// MainWindow.cs (UI Layer)
using EZTrainingDocCSharp.ScreenCapture;
using EZTrainingDocCSharp.WordEditing;
using EZTrainingDocCSharp.MouseListener;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<Bitmap> capturedScreenshotsList = new List<Bitmap>();

 

        private void UpdateStatus(string message)
        {
            if (this.Controls.ContainsKey("lblStatus"))
                this.Controls["lblStatus"].Text = message;
            else
                MessageBox.Show(message);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            var screenshot = ScreenshotTaker.CaptureScreen();
            if (screenshot != null)
            {                
                capturedScreenshotsList.Add(screenshot);
                UpdateStatus($"Screenshot #{capturedScreenshotsList.Count} captured and added to list.");
            }
        }

        private void btnSaveToWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedFolderPath))
            {
                MessageBox.Show("Please select an output folder first.", "Folder Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var creator = new WordDocumentCreator();
            creator.Create(selectedFolderPath, capturedScreenshotsList, UpdateStatus);
        }

        private void btnChangeFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                selectedFolderPath = folderBrowserDialog1.SelectedPath;
                lblSelectedFolder.Text = "Selected Folder: " + selectedFolderPath;
            }
        }

        private void btnnClearMemory_Click(object sender, EventArgs e)
        {
            foreach (Bitmap bmp in capturedScreenshotsList)
                bmp.Dispose();

            capturedScreenshotsList.Clear();
            UpdateStatus("Screenshot list cleared.");
        }

        private void btnViewScreenhots_Click(object sender, EventArgs e)
        {

        }
    }
}

