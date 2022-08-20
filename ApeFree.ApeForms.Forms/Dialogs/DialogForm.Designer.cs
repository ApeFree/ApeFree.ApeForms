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
            this.labContent = new ApeFree.ApeForms.Core.Controls.TallerLabel();
            this.panelView = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // flpOptions
            // 
            this.flpOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpOptions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpOptions.Location = new System.Drawing.Point(10, 109);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(414, 42);
            this.flpOptions.TabIndex = 0;
            // 
            // labContent
            // 
            this.labContent.AutoEllipsis = true;
            this.labContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.labContent.Location = new System.Drawing.Point(10, 10);
            this.labContent.Name = "labContent";
            this.labContent.Size = new System.Drawing.Size(414, 12);
            this.labContent.TabIndex = 2;
            this.labContent.Text = "Content";
            // 
            // panelView
            // 
            this.panelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelView.Location = new System.Drawing.Point(10, 22);
            this.panelView.Name = "panelView";
            this.panelView.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panelView.Size = new System.Drawing.Size(414, 87);
            this.panelView.TabIndex = 3;
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(254)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(434, 161);
            this.Controls.Add(this.panelView);
            this.Controls.Add(this.labContent);
            this.Controls.Add(this.flpOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ToastForm";
            this.TopMost = true;
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.FlowLayoutPanel flpOptions;
    private Core.Controls.TallerLabel labContent;
    private System.Windows.Forms.Panel panelView;
}