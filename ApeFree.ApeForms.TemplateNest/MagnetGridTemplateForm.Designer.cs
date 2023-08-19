namespace ApeFree.ApeForms.TemplateNest
{
    partial class MagnetGridTemplateForm
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
            panelHead = new System.Windows.Forms.Panel();
            LogoPictureBox = new System.Windows.Forms.PictureBox();
            panelSearchInput = new System.Windows.Forms.Panel();
            SearchInputPanel = new Core.Controls.RoundTextPanel();
            StatusStrip = new System.Windows.Forms.StatusStrip();
            StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            MagnetTable = new System.Windows.Forms.TableLayoutPanel();
            panelTable = new System.Windows.Forms.Panel();
            panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
            panelSearchInput.SuspendLayout();
            StatusStrip.SuspendLayout();
            panelTable.SuspendLayout();
            SuspendLayout();
            // 
            // panelHead
            // 
            panelHead.BackColor = System.Drawing.Color.FromArgb(30, 30, 50);
            panelHead.Controls.Add(LogoPictureBox);
            panelHead.Controls.Add(panelSearchInput);
            panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            panelHead.Location = new System.Drawing.Point(0, 0);
            panelHead.Margin = new System.Windows.Forms.Padding(4);
            panelHead.Name = "panelHead";
            panelHead.Size = new System.Drawing.Size(775, 87);
            panelHead.TabIndex = 2;
            // 
            // LogoPictureBox
            // 
            LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            LogoPictureBox.Location = new System.Drawing.Point(0, 0);
            LogoPictureBox.Margin = new System.Windows.Forms.Padding(4);
            LogoPictureBox.Name = "LogoPictureBox";
            LogoPictureBox.Size = new System.Drawing.Size(175, 87);
            LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            LogoPictureBox.TabIndex = 0;
            LogoPictureBox.TabStop = false;
            // 
            // panelSearchInput
            // 
            panelSearchInput.Controls.Add(SearchInputPanel);
            panelSearchInput.Dock = System.Windows.Forms.DockStyle.Right;
            panelSearchInput.Location = new System.Drawing.Point(391, 0);
            panelSearchInput.Name = "panelSearchInput";
            panelSearchInput.Padding = new System.Windows.Forms.Padding(20, 25, 20, 25);
            panelSearchInput.Size = new System.Drawing.Size(384, 87);
            panelSearchInput.TabIndex = 1;
            // 
            // SearchInputPanel
            // 
            SearchInputPanel.BackColor = System.Drawing.SystemColors.Window;
            SearchInputPanel.BorderColor = System.Drawing.Color.Purple;
            SearchInputPanel.BorderWidth = (ushort)2;
            SearchInputPanel.CornerRadius = 20;
            SearchInputPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            SearchInputPanel.Hint = "Search...";
            SearchInputPanel.HintColor = System.Drawing.Color.Gray;
            SearchInputPanel.Location = new System.Drawing.Point(20, 25);
            SearchInputPanel.Margin = new System.Windows.Forms.Padding(10);
            SearchInputPanel.MinimumSize = new System.Drawing.Size(30, 22);
            SearchInputPanel.Name = "SearchInputPanel";
            SearchInputPanel.Padding = new System.Windows.Forms.Padding(10);
            SearchInputPanel.Size = new System.Drawing.Size(344, 37);
            SearchInputPanel.TabIndex = 4;
            SearchInputPanel.TabStop = false;
            // 
            // StatusStrip
            // 
            StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { StatusLabel });
            StatusStrip.Location = new System.Drawing.Point(0, 538);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new System.Drawing.Size(775, 22);
            StatusStrip.TabIndex = 3;
            StatusStrip.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            StatusLabel.BackColor = System.Drawing.SystemColors.Control;
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new System.Drawing.Size(44, 17);
            StatusLabel.Text = "Ready";
            // 
            // MagnetTable
            // 
            MagnetTable.AutoSize = true;
            MagnetTable.ColumnCount = 4;
            MagnetTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            MagnetTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            MagnetTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            MagnetTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            MagnetTable.Dock = System.Windows.Forms.DockStyle.Top;
            MagnetTable.Location = new System.Drawing.Point(0, 0);
            MagnetTable.MinimumSize = new System.Drawing.Size(10, 10);
            MagnetTable.Name = "MagnetTable";
            MagnetTable.RowCount = 1;
            MagnetTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            MagnetTable.Size = new System.Drawing.Size(775, 10);
            MagnetTable.TabIndex = 4;
            // 
            // panelTable
            // 
            panelTable.AutoScroll = true;
            panelTable.Controls.Add(MagnetTable);
            panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            panelTable.Location = new System.Drawing.Point(0, 87);
            panelTable.Name = "panelTable";
            panelTable.Size = new System.Drawing.Size(775, 451);
            panelTable.TabIndex = 5;
            // 
            // MagnetGridTemplateForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(775, 560);
            Controls.Add(panelTable);
            Controls.Add(StatusStrip);
            Controls.Add(panelHead);
            Name = "MagnetGridTemplateForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "MagnetGridTemplateForm";
            panelHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
            panelSearchInput.ResumeLayout(false);
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            panelTable.ResumeLayout(false);
            panelTable.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelHead;
        private System.Windows.Forms.Panel panelSearchInput;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        protected Core.Controls.RoundTextPanel SearchInputPanel;
        protected System.Windows.Forms.PictureBox LogoPictureBox;
        protected System.Windows.Forms.StatusStrip StatusStrip;
        protected System.Windows.Forms.TableLayoutPanel MagnetTable;
        private System.Windows.Forms.Panel panelTable;
    }
}