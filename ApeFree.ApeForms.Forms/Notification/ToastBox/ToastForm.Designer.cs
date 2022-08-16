namespace ApeFree.ApeForms.Forms.Notification
{
    partial class ToastForm
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
            this.labContent = new System.Windows.Forms.Label();
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.timerWait = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labContent
            // 
            this.labContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.labContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labContent.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labContent.ForeColor = System.Drawing.Color.White;
            this.labContent.Location = new System.Drawing.Point(0, 0);
            this.labContent.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(414, 28);
            this.labContent.TabIndex = 2;
            this.labContent.Text = "content";
            this.labContent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labContent.MouseLeave += new System.EventHandler(this.LabContent_MouseLeave);
            this.labContent.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabContent_MouseMove);
            // 
            // timerHide
            // 
            this.timerHide.Interval = 25;
            this.timerHide.Tick += new System.EventHandler(this.TimerHide_Tick);
            // 
            // timerWait
            // 
            this.timerWait.Interval = 2000;
            this.timerWait.Tick += new System.EventHandler(this.TimerWait_Tick);
            // 
            // ToastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(414, 28);
            this.Controls.Add(this.labContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ToastForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ToastForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ToastForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labContent;
        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.Timer timerWait;
    }
}