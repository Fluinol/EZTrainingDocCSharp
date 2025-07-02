using System.Drawing;

namespace EZTrainingDocCSharp
{
    partial class ScreenshotPreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotPreviewForm));
            this.actionButtonsPanel = new System.Windows.Forms.Panel();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.selectionControlPanel = new System.Windows.Forms.Panel();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTipScreenshotPrev = new System.Windows.Forms.ToolTip(this.components);
            this.actionButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionButtonsPanel
            // 
            this.actionButtonsPanel.Controls.Add(this.chkSelectAll);
            this.actionButtonsPanel.Controls.Add(this.btnDelete);
            this.actionButtonsPanel.Controls.Add(this.btnSave);
            this.actionButtonsPanel.Controls.Add(this.btnChange);
            this.actionButtonsPanel.Controls.Add(this.btnClear);
            this.actionButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.actionButtonsPanel.Name = "actionButtonsPanel";
            this.actionButtonsPanel.Size = new System.Drawing.Size(800, 73);
            this.actionButtonsPanel.TabIndex = 0;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(165, 25);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(146, 20);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "Select / deselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAll_CheckedChanged);
            // 
            // btnDelete
            // 
            var deleteImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(317, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(45, 45);
            this.btnDelete.TabIndex = 2;
            this.toolTipScreenshotPrev.SetToolTip(this.btnDelete, "Delete selected screenshots.");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Image = new Bitmap(deleteImage, new Size(45, 45));
            this.btnDelete.ImageAlign = ContentAlignment.MiddleCenter;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(63, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 45);
            this.btnSave.TabIndex = 1;
            this.toolTipScreenshotPrev.SetToolTip(this.btnSave, "Create Word document.");
            this.btnSave.UseVisualStyleBackColor = true;
            var saveImage =global::EZTrainingDocCSharp.Properties.Resources.word48px.ToBitmap();
            this.btnSave.Image = new Bitmap(saveImage, new Size(30, 30));            
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChange
            // 
            this.btnChange.Image = ((System.Drawing.Image)(resources.GetObject("btnChange.Image")));
            this.btnChange.Location = new System.Drawing.Point(12, 12);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(45, 45);
            this.btnChange.TabIndex = 0;
            this.toolTipScreenshotPrev.SetToolTip(this.btnChange, "Select folder to save.");
            this.btnChange.UseVisualStyleBackColor = true;
            var changeImage = global::EZTrainingDocCSharp.Properties.Resources.folder48px.ToBitmap();
            btnChange.Image = new Bitmap(changeImage, new Size(30, 30));
            btnChange.ImageAlign = ContentAlignment.MiddleCenter;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(114, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(45, 45);
            this.btnClear.TabIndex = 0;
            this.toolTipScreenshotPrev.SetToolTip(this.btnClear, "Delete all screenshots.");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // selectionControlPanel
            // 
            this.selectionControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectionControlPanel.Location = new System.Drawing.Point(0, 73);
            this.selectionControlPanel.Name = "selectionControlPanel";
            this.selectionControlPanel.Size = new System.Drawing.Size(800, 24);
            this.selectionControlPanel.TabIndex = 1;
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 97);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(800, 353);
            this.flowPanel.TabIndex = 2;
            // 
            // ScreenshotPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.selectionControlPanel);
            this.Controls.Add(this.actionButtonsPanel);
            this.Name = "ScreenshotPreviewForm";
            this.Text = "ScreenshotPreviewForm";
            this.actionButtonsPanel.ResumeLayout(false);
            this.actionButtonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel actionButtonsPanel;
        private System.Windows.Forms.Panel selectionControlPanel;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolTip toolTipScreenshotPrev;
    }
}
