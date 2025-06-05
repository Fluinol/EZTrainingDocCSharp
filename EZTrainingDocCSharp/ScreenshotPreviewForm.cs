using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EZTrainingDocCSharp
{
    public partial class ScreenshotPreviewForm : Form
    {
        private List<Bitmap> screenshots;
        private List<CheckBox> checkBoxes = new List<CheckBox>();         
      

        public ScreenshotPreviewForm(List<Bitmap> screenshots)
        {
            InitializeComponent(); // Make sure this is called if you have a designer part
            this.screenshots = screenshots;
            InitializeUI();
        }

        

        private void InitializeUI()
        {
            this.Text = "Screenshot Previews";
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;           

            PopulateThumbnails();
        }

        private void PopulateThumbnails()
        {
            flowPanel.Controls.Clear();
            checkBoxes.Clear();

            for (int i = 0; i < screenshots.Count; i++)
            {
                var panel = new Panel { Width = 150, Height = 150, Margin = new Padding(5), BorderStyle = BorderStyle.FixedSingle };

                var picBox = new PictureBox
                {
                    Image = new Bitmap(screenshots[i], new Size(120, 90)),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 120,
                    Height = 90,
                    Top = 10, // Added some top margin
                    Left = (panel.Width - 120) / 2 // Center horizontally
                };
                panel.Controls.Add(picBox);

                var checkBox = new CheckBox
                {
                    Text = $"Screenshot {i + 1}",
                    Top = picBox.Bottom + 10, // Position below picture box
                    Left = 10,
                    Width = panel.Width - 20, // Adjust width
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel.Controls.Add(checkBox);
                checkBoxes.Add(checkBox);

                flowPanel.Controls.Add(panel);
            }
        }

        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool check = chkSelectAll.Checked;
            foreach (var cb in checkBoxes)
                cb.Checked = check;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var indicesToDelete = new List<int>();
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].Checked)
                    indicesToDelete.Add(i);
            }

            if (indicesToDelete.Count == 0)
            {
                MessageBox.Show("No screenshots selected for deletion.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete {indicesToDelete.Count} selected screenshot(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            indicesToDelete.Sort((a, b) => b.CompareTo(a)); // Sort descending to avoid index issues

            foreach (int idx in indicesToDelete)
            {
                if (idx < screenshots.Count) // Ensure index is valid
                {
                    screenshots[idx].Dispose(); // Dispose the bitmap
                    screenshots.RemoveAt(idx);
                }
            }

            PopulateThumbnails(); // Refresh the display
            chkSelectAll.Checked = false; // Uncheck "Select All"
        }

        public List<int> GetSelectedIndices()
        {
            var selected = new List<int>();
            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].Checked)
                    selected.Add(i);
            }
            return selected;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
            "Are you sure you want to clear all screenshots?",
            "Confirm Clear",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            // Dispose all bitmaps
            foreach (var bmp in screenshots)
            {
                bmp.Dispose();
            }
            screenshots.Clear();
            PopulateThumbnails();
            chkSelectAll.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder";
                dialog.ShowNewFolderButton = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // You can use dialog.SelectedPath as needed
                    MessageBox.Show($"Selected folder: {dialog.SelectedPath}", "Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // TODO: Handle the selected folder path as needed in your application
                }
            }
        }
    }
}