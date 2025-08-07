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
            this.btnWebSave = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.selectionControlPanel = new System.Windows.Forms.Panel();
            this.lblPreviewExplanation = new System.Windows.Forms.Label();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTipScreenshotPrev = new System.Windows.Forms.ToolTip(this.components);
            this.btnLeft = new System.Windows.Forms.Button();
            this.btbRight = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.actionButtonsPanel.SuspendLayout();
            this.selectionControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionButtonsPanel
            // 
            this.actionButtonsPanel.Controls.Add(this.btnLast);
            this.actionButtonsPanel.Controls.Add(this.btnFirst);
            this.actionButtonsPanel.Controls.Add(this.btbRight);
            this.actionButtonsPanel.Controls.Add(this.btnLeft);
            this.actionButtonsPanel.Controls.Add(this.btnWebSave);
            this.actionButtonsPanel.Controls.Add(this.chkSelectAll);
            this.actionButtonsPanel.Controls.Add(this.btnDelete);
            this.actionButtonsPanel.Controls.Add(this.btnSave);
            this.actionButtonsPanel.Controls.Add(this.btnChange);
            this.actionButtonsPanel.Controls.Add(this.btnClear);
            this.actionButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.actionButtonsPanel.Name = "actionButtonsPanel";
            this.actionButtonsPanel.Size = new System.Drawing.Size(800, 75);
            this.actionButtonsPanel.TabIndex = 0;
            // 
            // btnWebSave
            // 
            this.btnWebSave.Image = ((System.Drawing.Image)(resources.GetObject("btnWebSave.Image")));
            this.btnWebSave.Location = new System.Drawing.Point(114, 15);
            this.btnWebSave.Name = "btnWebSave";
            this.btnWebSave.Size = new System.Drawing.Size(45, 45);
            this.btnWebSave.TabIndex = 3;
            this.toolTipScreenshotPrev.SetToolTip(this.btnWebSave, "Create HTML document.");
            this.btnWebSave.UseVisualStyleBackColor = true;
            this.btnWebSave.Click += new System.EventHandler(this.btnWebSave_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(235, 28);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(146, 20);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "Select / deselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAll_CheckedChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(387, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(45, 45);
            this.btnDelete.TabIndex = 2;
            this.toolTipScreenshotPrev.SetToolTip(this.btnDelete, "Delete selected screenshots.");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(66, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(45, 45);
            this.btnSave.TabIndex = 1;
            this.toolTipScreenshotPrev.SetToolTip(this.btnSave, "Create Word document.");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChange
            // 
            this.btnChange.Image = ((System.Drawing.Image)(resources.GetObject("btnChange.Image")));
            this.btnChange.Location = new System.Drawing.Point(15, 15);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(45, 45);
            this.btnChange.TabIndex = 0;
            this.toolTipScreenshotPrev.SetToolTip(this.btnChange, "Select folder to save.");
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClear
            // 
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(184, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(45, 45);
            this.btnClear.TabIndex = 0;
            this.toolTipScreenshotPrev.SetToolTip(this.btnClear, "Delete all screenshots.");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // selectionControlPanel
            // 
            this.selectionControlPanel.Controls.Add(this.lblPreviewExplanation);
            this.selectionControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectionControlPanel.Location = new System.Drawing.Point(0, 75);
            this.selectionControlPanel.Name = "selectionControlPanel";
            this.selectionControlPanel.Size = new System.Drawing.Size(800, 24);
            this.selectionControlPanel.TabIndex = 1;
            // 
            // lblPreviewExplanation
            // 
            this.lblPreviewExplanation.AutoSize = true;
            this.lblPreviewExplanation.Location = new System.Drawing.Point(12, 3);
            this.lblPreviewExplanation.Name = "lblPreviewExplanation";
            this.lblPreviewExplanation.Size = new System.Drawing.Size(621, 16);
            this.lblPreviewExplanation.TabIndex = 0;
            this.lblPreviewExplanation.Text = "Double click on thumbnail to open detailed view. Keyboard arrows andd delete butt" +
    "on to move or delete.";
            this.lblPreviewExplanation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPreviewExplanation.Click += new System.EventHandler(this.lblPreviewExplanation_Click);
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 99);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(800, 351);
            this.flowPanel.TabIndex = 2;
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(510, 15);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(45, 45);
            this.btnLeft.TabIndex = 4;
            this.btnLeft.Text = "left";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btbRight
            // 
            this.btbRight.Location = new System.Drawing.Point(561, 15);
            this.btbRight.Name = "btbRight";
            this.btbRight.Size = new System.Drawing.Size(45, 45);
            this.btbRight.TabIndex = 5;
            this.btbRight.Text = "right";
            this.btbRight.UseVisualStyleBackColor = true;
            this.btbRight.Click += new System.EventHandler(this.btbRight_Click);
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(612, 15);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 45);
            this.btnLast.TabIndex = 7;
            this.btnLast.Text = "last";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(459, 15);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(45, 45);
            this.btnFirst.TabIndex = 6;
            this.btnFirst.Text = "first";
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // ScreenshotPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.selectionControlPanel);
            this.Controls.Add(this.actionButtonsPanel);
            this.Icon = global::EZTrainingDocCSharp.Properties.Resources.video_camera_64px;
            this.Name = "ScreenshotPreviewForm";
            this.Text = "ScreenshotPreviewForm";
            this.Activated += new System.EventHandler(this.ScreenshotPreviewForm_Activated);
            this.Load += new System.EventHandler(this.ScreenshotPreviewForm_Load);
            this.actionButtonsPanel.ResumeLayout(false);
            this.actionButtonsPanel.PerformLayout();
            this.selectionControlPanel.ResumeLayout(false);
            this.selectionControlPanel.PerformLayout();
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
        private System.Windows.Forms.Button btnWebSave;
        private System.Windows.Forms.Label lblPreviewExplanation;
        private System.Windows.Forms.Button btbRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.Button btnFirst;
    }
}
