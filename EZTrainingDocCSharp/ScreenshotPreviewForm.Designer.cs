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
            this.actionButtonsPanel = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.selectionControlPanel = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.flowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.actionButtonsPanel.SuspendLayout();
            this.selectionControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionButtonsPanel
            // 
            this.actionButtonsPanel.Controls.Add(this.btnSave);
            this.actionButtonsPanel.Controls.Add(this.btnChange);
            this.actionButtonsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionButtonsPanel.Location = new System.Drawing.Point(0, 0);
            this.actionButtonsPanel.Name = "actionButtonsPanel";
            this.actionButtonsPanel.Size = new System.Drawing.Size(800, 40);
            this.actionButtonsPanel.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(139, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save to Word";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(3, 3);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(130, 30);
            this.btnChange.TabIndex = 0;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // selectionControlPanel
            // 
            this.selectionControlPanel.Controls.Add(this.btnDelete);
            this.selectionControlPanel.Controls.Add(this.chkSelectAll);
            this.selectionControlPanel.Controls.Add(this.btnClear);
            this.selectionControlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.selectionControlPanel.Location = new System.Drawing.Point(0, 40);
            this.selectionControlPanel.Name = "selectionControlPanel";
            this.selectionControlPanel.Size = new System.Drawing.Size(800, 40);
            this.selectionControlPanel.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(316, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete selection";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(164, 9);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(146, 20);
            this.chkSelectAll.TabIndex = 1;
            this.chkSelectAll.Text = "Select / deselect all";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.ChkSelectAll_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(3, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(130, 30);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear all";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // flowPanel
            // 
            this.flowPanel.AutoScroll = true;
            this.flowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPanel.Location = new System.Drawing.Point(0, 80);
            this.flowPanel.Name = "flowPanel";
            this.flowPanel.Size = new System.Drawing.Size(800, 370);
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

    }
}