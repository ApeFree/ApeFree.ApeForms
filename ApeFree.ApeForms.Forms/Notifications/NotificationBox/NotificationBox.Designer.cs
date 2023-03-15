namespace ApeFree.ApeForms.Forms.Notifications
{
    partial class NotificationBox
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
            this.timerDisappear = new System.Windows.Forms.Timer(this.components);
            this.labTitle = new System.Windows.Forms.Label();
            this.btnClose = new ApeFree.ApeForms.Core.Controls.ShapeButton();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSpareControl = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // timerDisappear
            // 
            this.timerDisappear.Interval = 5000;
            this.timerDisappear.Tick += new System.EventHandler(this.timerDisappear_Tick);
            // 
            // labTitle
            // 
            this.labTitle.AutoEllipsis = true;
            this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.Location = new System.Drawing.Point(66, 5);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(229, 20);
            this.labTitle.TabIndex = 1;
            this.labTitle.Text = "Notification";
            this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(196)))), ((int)(((byte)(196)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.GraphicScale = 0.5F;
            this.btnClose.Location = new System.Drawing.Point(275, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Shape = ApeFree.ApeForms.Core.Controls.ShapeButton.SimpleShape.Close;
            this.btnClose.Size = new System.Drawing.Size(20, 20);
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // flpOptions
            // 
            this.flpOptions.AutoSize = true;
            this.flpOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpOptions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpOptions.Location = new System.Drawing.Point(66, 95);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(229, 0);
            this.flpOptions.TabIndex = 2;
            // 
            // panelSpareControl
            // 
            this.panelSpareControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSpareControl.Location = new System.Drawing.Point(5, 5);
            this.panelSpareControl.Name = "panelSpareControl";
            this.panelSpareControl.Size = new System.Drawing.Size(61, 90);
            this.panelSpareControl.TabIndex = 3;
            this.panelSpareControl.Visible = false;
            this.panelSpareControl.SizeChanged += new System.EventHandler(this.PanelSpareControl_SizeChanged);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(66, 25);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(4);
            this.panelMain.Size = new System.Drawing.Size(229, 70);
            this.panelMain.TabIndex = 4;
            // 
            // NotificationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.flpOptions);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labTitle);
            this.Controls.Add(this.panelSpareControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NotificationBox";
            this.Opacity = 0.9D;
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.MouseEnter += new System.EventHandler(this.NotificationBox_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.NotificationBox_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerDisappear;
        private Core.Controls.ShapeButton btnClose;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
        private System.Windows.Forms.Panel panelSpareControl;
        private System.Windows.Forms.Panel panelMain;
    }
}