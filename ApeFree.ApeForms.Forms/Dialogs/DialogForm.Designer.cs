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
        flpOptions = new System.Windows.Forms.FlowLayoutPanel();
        labContent = new Core.Controls.TallerLabel();
        panelView = new System.Windows.Forms.Panel();
        SuspendLayout();
        // 
        // flpOptions
        // 
        flpOptions.AutoSize = true;
        flpOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
        flpOptions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        flpOptions.Location = new System.Drawing.Point(12, 214);
        flpOptions.Margin = new System.Windows.Forms.Padding(4);
        flpOptions.Name = "flpOptions";
        flpOptions.Size = new System.Drawing.Size(482, 0);
        flpOptions.TabIndex = 0;
        // 
        // labContent
        // 
        labContent.AutoEllipsis = true;
        labContent.Dock = System.Windows.Forms.DockStyle.Top;
        labContent.Location = new System.Drawing.Point(12, 14);
        labContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        labContent.Name = "labContent";
        labContent.Size = new System.Drawing.Size(482, 17);
        labContent.TabIndex = 2;
        labContent.Text = "Content";
        // 
        // panelView
        // 
        panelView.Dock = System.Windows.Forms.DockStyle.Fill;
        panelView.Location = new System.Drawing.Point(12, 31);
        panelView.Margin = new System.Windows.Forms.Padding(4);
        panelView.Name = "panelView";
        panelView.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
        panelView.Size = new System.Drawing.Size(482, 183);
        panelView.TabIndex = 3;
        // 
        // DialogForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(255, 254, 253);
        ClientSize = new System.Drawing.Size(506, 228);
        Controls.Add(panelView);
        Controls.Add(labContent);
        Controls.Add(flpOptions);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "DialogForm";
        Padding = new System.Windows.Forms.Padding(12, 14, 12, 14);
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "ToastForm";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private System.Windows.Forms.FlowLayoutPanel flpOptions;
    private Core.Controls.TallerLabel labContent;
    private System.Windows.Forms.Panel panelView;
}