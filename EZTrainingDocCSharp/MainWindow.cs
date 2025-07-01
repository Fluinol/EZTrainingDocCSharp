// MainWindow.cs (UI Layer)
using EZTrainingDocCSharp.Mouse;
using EZTrainingDocCSharp.ScreenCapture;
using EZTrainingDocCSharp.WordEditing;
using System;
using System.Collections.Generic;
using System.Data;
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
        
        

        public void UpdateStatus(string message)
        {           
            if (this.Controls.ContainsKey("lblStatus"))
                this.Controls["lblStatus"].Text = message;
            else
                MessageBox.Show(message);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            //var screenshot = ScreenshotTaker.CaptureScreen();
            //if (screenshot != null)
            //{
            //    capturedScreenshotsList.Add(screenshot);
            //    UpdateStatus($"Screenshot #{capturedScreenshotsList.Count} captured and added to list.");
            //}
        }

        private void btnSaveToWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedFolderPath))
            {
                MessageBox.Show("Please select an output folder first.", "Folder Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var creator = new WordDocumentCreator();
            //creator.Create(selectedFolderPath, capturedScreenshotsList, UpdateStatus);
            creator.Create(selectedFolderPath, capturedScreenshotsList);
        }


        private void btnnClearMemory_Click(object sender, EventArgs e)
        {
            foreach (var info in capturedScreenshotsList)
                info.Image.Dispose();

            capturedScreenshotsList.Clear();
            
        }

        private void btnViewScreenhots_Click(object sender, EventArgs e)
        {
            var previewForm = new ScreenshotPreviewForm(capturedScreenshotsList);
            previewForm.ShowDialog(this);
        }   

        private void btnStop_Click(object sender, EventArgs e)
        {
            
            if (isRecording)
            {
                isRecording = !isRecording;
                btnStartPause.Text = "Resume recording";                
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
                btnStartPause.Text = "Pause";
                

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
                btnStartPause.Text = "Resume recording";                

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


    }
}

