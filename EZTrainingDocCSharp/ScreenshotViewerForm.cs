using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class ScreenshotViewerForm : Form
{
    private List<string> _screenshotPaths;
    private int _currentIndex;
    private PictureBox _pictureBox;

    public ScreenshotViewerForm(List<string> screenshotPaths, int startIndex)
    {
        _screenshotPaths = screenshotPaths ?? throw new ArgumentNullException(nameof(screenshotPaths));
        _currentIndex = startIndex;
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

        _pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom
        };
        this.Controls.Add(_pictureBox);

        this.KeyDown += ScreenshotViewerForm_KeyDown;
    }

    private void LoadScreenshot()
    {
        if (_screenshotPaths.Count == 0 || _currentIndex < 0 || _currentIndex >= _screenshotPaths.Count)
        {
            _pictureBox.Image = null;
            this.Text = "Screenshot Viewer";
            return;
        }

        string path = _screenshotPaths[_currentIndex];
        if (File.Exists(path))
        {
            using (var img = Image.FromFile(path))
            {
                _pictureBox.Image?.Dispose();
                _pictureBox.Image = new Bitmap(img);
            }
            this.Text = $"Screenshot Viewer ({_currentIndex + 1}/{_screenshotPaths.Count})";
        }
        else
        {
            _pictureBox.Image = null;
            this.Text = "Screenshot not found";
        }
    }

    private void ScreenshotViewerForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Right)
        {
            if (_currentIndex < _screenshotPaths.Count - 1)
            {
                _currentIndex++;
                LoadScreenshot();
            }
        }
        else if (e.KeyCode == Keys.Left)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                LoadScreenshot();
            }
        }
        else if (e.KeyCode == Keys.Delete)
        {
            if (_screenshotPaths.Count > 0)
            {
                string toDelete = _screenshotPaths[_currentIndex];
                _screenshotPaths.RemoveAt(_currentIndex);
                try
                {
                    if (File.Exists(toDelete))
                        File.Delete(toDelete);
                }
                catch { /* Handle exceptions as needed */ }

                if (_currentIndex >= _screenshotPaths.Count)
                    _currentIndex = _screenshotPaths.Count - 1;

                LoadScreenshot();
            }
        }
    }
}