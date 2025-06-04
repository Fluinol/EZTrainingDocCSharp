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
        private FlowLayoutPanel flowPanel;
        private CheckBox chkSelectAll;
        private Button btnDelete;

        public ScreenshotPreviewForm(List<Bitmap> screenshots)
        {
            InitializeComponent();
            this.screenshots = screenshots;
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Screenshot Previews";
            this.Width = 800;
            this.Height = 600;

            // Top panel for controls
            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50
            };

            chkSelectAll = new CheckBox
            {
                Text = "Select/Deselect All",
                Left = 10,
                Top = 15,
                Width = 150
            };
            chkSelectAll.CheckedChanged += ChkSelectAll_CheckedChanged;
            topPanel.Controls.Add(chkSelectAll);

            btnDelete = new Button
            {
                Text = "Delete Selected",
                Left = 180,
                Top = 10,
                Width = 120,
                Height = 30
            };
            btnDelete.Click += BtnDelete_Click;
            topPanel.Controls.Add(btnDelete);

            this.Controls.Add(topPanel);

            // Flow panel for thumbnails
            flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true
            };
            this.Controls.Add(flowPanel);

            PopulateThumbnails();
        }

        private void PopulateThumbnails()
        {
            flowPanel.Controls.Clear();
            checkBoxes.Clear();

            for (int i = 0; i < screenshots.Count; i++)
            {
                var panel = new Panel { Width = 150, Height = 150, Margin = new Padding(5) };

                var picBox = new PictureBox
                {
                    Image = new Bitmap(screenshots[i], new Size(120, 90)),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 120,
                    Height = 90,
                    Top = 5,
                    Left = 10
                };
                panel.Controls.Add(picBox);

                var checkBox = new CheckBox
                {
                    Text = $"Screenshot {i + 1}",
                    Top = 100,
                    Left = 10,
                    Width = 120
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
            // Collect indices to delete (in reverse order to avoid shifting)
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

            // Confirm deletion
            var result = MessageBox.Show($"Delete {indicesToDelete.Count} selected screenshot(s)?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            // Delete from end to start to avoid index issues
            indicesToDelete.Reverse();
            foreach (int idx in indicesToDelete)
            {
                screenshots[idx].Dispose();
                screenshots.RemoveAt(idx);
            }

            PopulateThumbnails();
            chkSelectAll.Checked = false;
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
    }
}
