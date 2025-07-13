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

    public ScreenshotViewerForm(List<ScreenshotInfo> screenshots, int startIndex)
    {
        _screenshots = screenshots ?? throw new ArgumentNullException(nameof(screenshots));
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

    private void ScreenshotViewerForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Right)
        {
            if (_currentIndex < _screenshots.Count - 1)
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