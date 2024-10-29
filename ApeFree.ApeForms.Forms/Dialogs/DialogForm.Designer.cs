namespace ApeFree.ApeForms.Forms.Dialogs;

partial class DialogForm
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
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.panelView = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.labContent = new ApeFree.ApeForms.Core.Controls.TallerLabel();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpOptions
            // 
            this.flpOptions.AutoSize = true;
            this.flpOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpOptions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpOptions.Location = new System.Drawing.Point(12, 206);
            this.flpOptions.Margin = new System.Windows.Forms.Padding(4);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(476, 0);
            this.flpOptions.TabIndex = 0;
            // 
            // panelView
            // 
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(12, 52);
            this.panelView.Margin = new System.Windows.Forms.Padding(4);
            this.panelView.Name = "panelView";
            this.panelView.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.panelView.Size = new System.Drawing.Size(476, 154);
            this.panelView.TabIndex = 3;
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.AutoSize = true;
            this.panelContent.Controls.Add(this.labContent);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContent.Location = new System.Drawing.Point(12, 40);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(476, 12);
            this.panelContent.TabIndex = 4;
            // 
            // labContent
            // 
            this.labContent.AutoEllipsis = true;
            this.labContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.labContent.Location = new System.Drawing.Point(0, 0);
            this.labContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(476, 12);
            this.labContent.TabIndex = 3;
            this.labContent.Text = "Content";
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(500, 220);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.flpOptions);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogForm";
            this.Padding = new System.Windows.Forms.Padding(12, 40, 12, 14);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToastForm";
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.FlowLayoutPanel flpOptions;
    private System.Windows.Forms.Panel panelView;
    private System.Windows.Forms.Panel panelContent;
    private Core.Controls.TallerLabel labContent;
}