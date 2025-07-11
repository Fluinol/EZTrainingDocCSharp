// MIT License
// Copyright (c) 2025 [Fluinol]
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

// MainWindow.cs (UI Layer)
using EZTrainingDocCSharp.ScreenCapture;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace EZTrainingDocCSharp
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            if (this.Controls.ContainsKey("lblStatus"))
                this.Controls["lblStatus"].Text = "Ready.";
        }

        //private string selectedFolderPath = string.Empty;
        private string selectedFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private List<ScreenshotInfo> capturedScreenshotsList = new List<ScreenshotInfo>();
        private bool isRecording = false;
        private static Thread mouseListenerThread;



        private void btnStop_Click(object sender, EventArgs e)
        {

            if (isRecording)
            {
                isRecording = !isRecording;
                //btnStartPause.Text = "Resume recording";
                var recordImage = global::EZTrainingDocCSharp.Properties.Resources.rec48px.ToBitmap();
                btnStartPause.Image = new Bitmap(recordImage, new Size(45, 45));
                btnStartPause.ImageAlign = ContentAlignment.MiddleCenter;
                this.Invalidate(); // Force redraw to update border
            }


            // Stop MouseListener only if running
            Mouse.MouseListener.Stop();
            if (mouseListenerThread != null && mouseListenerThread.IsAlive)
            {
                mouseListenerThread.Join(500); // Optionally wait for thread to finish
                mouseListenerThread = null;
            }

            var previewForm = new ScreenshotPreviewForm(capturedScreenshotsList);
            previewForm.ShowDialog(this);

        }

        private void btnStartPause_Click(object sender, EventArgs e)
        {
            isRecording = !isRecording;

            if (isRecording)
            {
                //btnStartPause.Text = "Pause";
                var pauseImage = global::EZTrainingDocCSharp.Properties.Resources.pause48px.ToBitmap();
                btnStartPause.Image = new Bitmap(pauseImage, new Size(45, 45));
                btnStartPause.ImageAlign = ContentAlignment.MiddleCenter;




                // Start MouseListener only if not already running
                if (mouseListenerThread == null || !mouseListenerThread.IsAlive)
                {
                    //mouseListenerThread = new Thread(() => Mouse.MouseListener.Start(capturedScreenshotsList));
                    //mouseListenerThread.IsBackground = true;
                    Mouse.MouseListener.Start(capturedScreenshotsList);


                }
            }
            else
            {
                //btnStartPause.Text = "Resume recording";                
                var recordImage = global::EZTrainingDocCSharp.Properties.Resources.rec48px.ToBitmap();
                btnStartPause.Image = new Bitmap(recordImage, new Size(45, 45));
                btnStartPause.ImageAlign = ContentAlignment.MiddleCenter;

                // Stop MouseListener only if running
                Mouse.MouseListener.Stop();
                if (mouseListenerThread != null && mouseListenerThread.IsAlive)
                {
                    mouseListenerThread.Join(500); // Optionally wait for thread to finish
                    mouseListenerThread = null;
                }
            }
            this.Invalidate(); // Force redraw to update border
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (isRecording)
            {
                int borderWidth = 2;
                using (Pen pen = new Pen(Color.Red, borderWidth))
                {
                    e.Graphics.DrawRectangle(
                        pen,
                        borderWidth / 2,
                        borderWidth / 2,
                        this.ClientSize.Width - borderWidth,
                        this.ClientSize.Height - borderWidth
                    );
                }
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}

