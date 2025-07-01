using System.Drawing;

namespace EZTrainingDocCSharp
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.btnStartPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.toolTipMainWindow = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnStartPause
            // 
            this.btnStartPause.Image = ((System.Drawing.Image)(resources.GetObject("btnStartPause.Image")));
            this.btnStartPause.Location = new System.Drawing.Point(12, 12);
            this.btnStartPause.Name = "btnStartPause";
            this.btnStartPause.Size = new System.Drawing.Size(45, 45);
            this.btnStartPause.TabIndex = 2;
            this.toolTipMainWindow.SetToolTip(this.btnStartPause, "Start or pause the recording of screenshots.");
            this.btnStartPause.UseVisualStyleBackColor = true;
            this.btnStartPause.Click += new System.EventHandler(this.btnStartPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(73, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(45, 45);
            this.btnStop.TabIndex = 10;
            this.toolTipMainWindow.SetToolTip(this.btnStop, "Stop recording and preview the captured screenshots.");
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 69);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStartPause);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Ez training Doc BETA";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStartPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolTip toolTipMainWindow;
    }
}