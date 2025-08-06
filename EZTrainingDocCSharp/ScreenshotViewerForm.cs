using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using EZTrainingDocCSharp.ScreenCapture;

public class ScreenshotViewerForm : Form
{
    private List<ScreenshotInfo> _screenshots;
    private int _currentIndex;
    private PictureBox _pictureBox;
    private Button _saveButton;
    private Button _clearAllButton;


    private bool isDrawing = false;
    private Point startPoint;
    private Rectangle currentRect;
    private Bitmap _originalScreenshot;

    public ScreenshotViewerForm(List<ScreenshotInfo> screenshots, int startIndex)
    {
        _screenshots = screenshots ?? throw new ArgumentNullException(nameof(screenshots));
        _currentIndex = startIndex;
        _originalScreenshot = new Bitmap(_screenshots[_currentIndex].Image);
        InitializeComponents();
        LoadScreenshot();
    }

    private void InitializeComponents()
    {
        this.Text = "Screenshot Viewer";
        this.Width = 1600;
        this.Height = 1200;
        this.KeyPreview = true;
        this.StartPosition = FormStartPosition.CenterScreen;

        // Create a top panel for buttons
        var topPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 44,
            Padding = new Padding(4),
            BackColor = Color.LightGray
        };
        this.Controls.Add(topPanel);

        // Save button
        _saveButton = new Button
        {
            Text = "Save",
            Width = 140,
            Height = 36,
            Margin = new Padding(4, 4, 4, 4)
        };
        _saveButton.Click += SaveButton_Click;
        topPanel.Controls.Add(_saveButton);

      
        // Clear All Changes button
        _clearAllButton = new Button
        {
            Text = "Clear all",
            Width = 140,
            Height = 36,
            Margin = new Padding(4, 4, 4, 4)
        };
        _clearAllButton.Click += ClearAllButton_Click;
        topPanel.Controls.Add(_clearAllButton);

        // Arrange buttons left to right
        foreach (Control btn in topPanel.Controls)
        {
            btn.Dock = DockStyle.Left;
        }

        _pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom
        };
        this.Controls.Add(_pictureBox);

        this.KeyDown += ScreenshotViewerForm_KeyDown;

        _pictureBox.MouseDown += pictureBoxPreview_MouseDown;
        _pictureBox.MouseMove += pictureBoxPreview_MouseMove;
        _pictureBox.MouseUp += pictureBoxPreview_MouseUp;
        _pictureBox.Paint += pictureBoxPreview_Paint;
    }

    private void LoadScreenshot()
    {
        if (_screenshots.Count == 0 || _currentIndex < 0 || _currentIndex >= _screenshots.Count)
        {
            _pictureBox.Image = null;
            this.Text = "Screenshot Viewer";
            return;
        }

        var info = _screenshots[_currentIndex];
        _pictureBox.Image?.Dispose();
        _pictureBox.Image = new Bitmap(info.Image);

        this.Text = $"Screenshot Viewer ({_currentIndex + 1}/{_screenshots.Count})";
    }

    private void SetCurrentScreenshot(int newIndex)
    {
        if (newIndex < 0 || newIndex >= _screenshots.Count)
            return;

        _currentIndex = newIndex;
        _originalScreenshot?.Dispose();
        _originalScreenshot = new Bitmap(_screenshots[_currentIndex].Image);
        LoadScreenshot();
    }

    private void ScreenshotViewerForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Right)
        {
            if (_currentIndex < _screenshots.Count - 1)
            {
                SetCurrentScreenshot(_currentIndex + 1);
            }
        }
        else if (e.KeyCode == Keys.Left)
        {
            if (_currentIndex > 0)
            {
                SetCurrentScreenshot(_currentIndex - 1);
            }
        }
        else if (e.KeyCode == Keys.Delete)
        {
            // Add confirmation dialog before deletion
            var result = MessageBox.Show("Are you sure you want to delete this screenshot?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (_screenshots.Count > 0)
                {
                    var toDelete = _screenshots[_currentIndex];
                    toDelete.Image.Dispose();
                    _screenshots.RemoveAt(_currentIndex);

                    if (_currentIndex >= _screenshots.Count)
                        _currentIndex = _screenshots.Count - 1;

                    LoadScreenshot();
                }
            }           
           
        }
    }

    private void pictureBoxPreview_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = true;
            startPoint = e.Location;
            currentRect = new Rectangle(e.Location, Size.Empty);
        }
    }

    private void pictureBoxPreview_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            int width = e.X - startPoint.X;
            int height = e.Y - startPoint.Y;
            currentRect = new Rectangle(
                Math.Min(startPoint.X, e.X),
                Math.Min(startPoint.Y, e.Y),
                Math.Abs(width),
                Math.Abs(height));
            _pictureBox.Invalidate(); // Triggers repaint
        }
    }

    private void pictureBoxPreview_MouseUp(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            isDrawing = false;
            _pictureBox.Invalidate();
        }
    }

    private void pictureBoxPreview_Paint(object sender, PaintEventArgs e)
    {
        if (currentRect != Rectangle.Empty)
        {
            using (var pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, currentRect);
            }
        }
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        if (_pictureBox.Image == null || _screenshots.Count == 0 || _currentIndex < 0)
            return;

        // Create a new bitmap based on the current image
        Bitmap original = _screenshots[_currentIndex].Image;
        Bitmap updated = new Bitmap(original.Width, original.Height);

        using (Graphics g = Graphics.FromImage(updated))
        {
            // Draw the original image
            g.DrawImage(original, 0, 0, original.Width, original.Height);

            // Calculate the rectangle in image coordinates
            Rectangle imageRect = GetImageRectangleInPictureBox(_pictureBox);
            if (currentRect != Rectangle.Empty && imageRect.Width > 0 && imageRect.Height > 0)
            {
                float scaleX = (float)original.Width / imageRect.Width;
                float scaleY = (float)original.Height / imageRect.Height;

                int x = (int)((currentRect.X - imageRect.X) * scaleX);
                int y = (int)((currentRect.Y - imageRect.Y) * scaleY);
                int w = (int)(currentRect.Width * scaleX);
                int h = (int)(currentRect.Height * scaleY);

                using (var pen = new Pen(Color.Red, 4))
                {
                    g.DrawRectangle(pen, x, y, w, h);
                }
            }
        }

        // Dispose the old image and update the list
        _screenshots[_currentIndex].Image.Dispose();
        _screenshots[_currentIndex].Image = updated;

        // Clear the drawn rectangle and reload the screenshot
        currentRect = Rectangle.Empty;
        LoadScreenshot();
    }

    private void ClearAllButton_Click(object sender, EventArgs e)
    {
        if (_screenshots.Count == 0 || _currentIndex < 0 || _originalScreenshot == null)
            return;


        _screenshots[_currentIndex].Image.Dispose();
        _screenshots[_currentIndex].Image = new Bitmap(_originalScreenshot); // This is safe if _originalScreenshot is valid

        currentRect = Rectangle.Empty;
        LoadScreenshot();
    }

    // Helper to get the image rectangle inside the PictureBox (for Zoom mode)
    private Rectangle GetImageRectangleInPictureBox(PictureBox pb)
    {
        if (pb.Image == null) return Rectangle.Empty;
        var img = pb.Image;
        var pbWidth = pb.ClientSize.Width;
        var pbHeight = pb.ClientSize.Height;
        var imgWidth = img.Width;
        var imgHeight = img.Height;

        float ratio = Math.Min((float)pbWidth / imgWidth, (float)pbHeight / imgHeight);
        int width = (int)(imgWidth * ratio);
        int height = (int)(imgHeight * ratio);
        int x = (pbWidth - width) / 2;
        int y = (pbHeight - height) / 2;
        return new Rectangle(x, y, width, height);
    }
    private void saveOriginalScreenshot(Bitmap screenshot)
    {
        _originalScreenshot?.Dispose();
        _originalScreenshot = new Bitmap(screenshot); // This clones the bitmap
    }
}
