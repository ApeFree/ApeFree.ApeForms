using ApeFree.ApeForms.Core.Controls;

namespace ApeFree.ApeForms.TemplateNest
{
    partial class TemplateForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Core.Utils.StateColorSet stateColorSet1 = new Core.Utils.StateColorSet();
            panelHead = new System.Windows.Forms.Panel();
            clbTopBar = new ControlListBox();
            picLogo = new System.Windows.Forms.PictureBox();
            slideTabControl = new SlideTabControl();
            clbSideBar = new ControlListBox();
            clbBottomBar = new ControlListBox();
            splitContainer = new System.Windows.Forms.SplitContainer();
            rtpSearch = new RoundTextPanel();
            cmsPageItem = new System.Windows.Forms.ContextMenuStrip(components);
            panelHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // panelHead
            // 
            panelHead.BackColor = System.Drawing.Color.FromArgb(30, 30, 50);
            panelHead.Controls.Add(clbTopBar);
            panelHead.Controls.Add(picLogo);
            panelHead.Dock = System.Windows.Forms.DockStyle.Top;
            panelHead.Location = new System.Drawing.Point(0, 0);
            panelHead.Margin = new System.Windows.Forms.Padding(4);
            panelHead.Name = "panelHead";
            panelHead.Size = new System.Drawing.Size(780, 71);
            panelHead.TabIndex = 1;
            // 
            // clbTopBar
            // 
            clbTopBar.AutoScroll = true;
            clbTopBar.BackColor = System.Drawing.Color.Transparent;
            clbTopBar.Direction = System.Windows.Forms.FlowDirection.RightToLeft;
            clbTopBar.Dock = System.Windows.Forms.DockStyle.Fill;
            clbTopBar.Location = new System.Drawing.Point(175, 0);
            clbTopBar.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            clbTopBar.Name = "clbTopBar";
            clbTopBar.Size = new System.Drawing.Size(605, 71);
            clbTopBar.TabIndex = 1;
            // 
            // picLogo
            // 
            picLogo.Dock = System.Windows.Forms.DockStyle.Left;
            picLogo.Location = new System.Drawing.Point(0, 0);
            picLogo.Margin = new System.Windows.Forms.Padding(4);
            picLogo.Name = "picLogo";
            picLogo.Size = new System.Drawing.Size(175, 71);
            picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            // 
            // slideTabControl
            // 
            slideTabControl.CloseAllPagesOptionText = "Close all";
            slideTabControl.ClosePageOptionText = "Close";
            slideTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            slideTabControl.Location = new System.Drawing.Point(0, 0);
            slideTabControl.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            slideTabControl.Name = "slideTabControl";
            slideTabControl.PageItemContextMenu = cmsPageItem;
            slideTabControl.Rate = 2;
            slideTabControl.ShowPageCloseButton = false;
            slideTabControl.Size = new System.Drawing.Size(564, 397);
            stateColorSet1.GotFocusBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            stateColorSet1.GotFocusForeColor = System.Drawing.Color.White;
            stateColorSet1.LostFocusBackColor = System.Drawing.Color.FromArgb(251, 251, 251);
            stateColorSet1.LostFocusForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            stateColorSet1.MouseDownBackColor = System.Drawing.Color.FromArgb(14, 97, 152);
            stateColorSet1.MouseDownForeColor = System.Drawing.Color.White;
            stateColorSet1.MouseLeaveBackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            stateColorSet1.MouseLeaveForeColor = System.Drawing.Color.White;
            stateColorSet1.MouseMoveBackColor = System.Drawing.Color.FromArgb(82, 176, 239);
            stateColorSet1.MouseMoveForeColor = System.Drawing.Color.White;
            slideTabControl.StateColorSet = stateColorSet1;
            slideTabControl.TabIndex = 0;
            slideTabControl.TitleDock = System.Windows.Forms.DockStyle.Top;
            slideTabControl.TitleLayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            // 
            // clbSideBar
            // 
            clbSideBar.AutoScroll = true;
            clbSideBar.BackColor = System.Drawing.Color.Transparent;
            clbSideBar.Direction = System.Windows.Forms.FlowDirection.TopDown;
            clbSideBar.Dock = System.Windows.Forms.DockStyle.Fill;
            clbSideBar.Location = new System.Drawing.Point(0, 27);
            clbSideBar.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            clbSideBar.Name = "clbSideBar";
            clbSideBar.Size = new System.Drawing.Size(212, 370);
            clbSideBar.TabIndex = 3;
            // 
            // clbBottomBar
            // 
            clbBottomBar.AutoScroll = true;
            clbBottomBar.BackColor = System.Drawing.Color.WhiteSmoke;
            clbBottomBar.Direction = System.Windows.Forms.FlowDirection.BottomUp;
            clbBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            clbBottomBar.Location = new System.Drawing.Point(0, 468);
            clbBottomBar.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            clbBottomBar.Name = "clbBottomBar";
            clbBottomBar.Size = new System.Drawing.Size(780, 71);
            clbBottomBar.TabIndex = 4;
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer.Location = new System.Drawing.Point(0, 71);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(40, 40, 60);
            splitContainer.Panel1.Controls.Add(clbSideBar);
            splitContainer.Panel1.Controls.Add(rtpSearch);
            splitContainer.Panel1MinSize = 175;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(slideTabControl);
            splitContainer.Size = new System.Drawing.Size(780, 397);
            splitContainer.SplitterDistance = 212;
            splitContainer.TabIndex = 5;
            // 
            // rtpSearch
            // 
            rtpSearch.BackColor = System.Drawing.SystemColors.Window;
            rtpSearch.BorderColor = System.Drawing.SystemColors.Highlight;
            rtpSearch.BorderWidth = (ushort)2;
            rtpSearch.CornerRadius = 20;
            rtpSearch.Dock = System.Windows.Forms.DockStyle.Top;
            rtpSearch.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            rtpSearch.Hint = null;
            rtpSearch.HintColor = System.Drawing.Color.Gray;
            rtpSearch.Location = new System.Drawing.Point(0, 0);
            rtpSearch.MinimumSize = new System.Drawing.Size(30, 24);
            rtpSearch.Name = "rtpSearch";
            rtpSearch.ReadOnly = false;
            rtpSearch.Size = new System.Drawing.Size(212, 27);
            rtpSearch.TabIndex = 4;
            rtpSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            rtpSearch.TextChanged += rtpSearch_TextChanged;
            // 
            // cmsPageItem
            // 
            cmsPageItem.Name = "cmsPageItem";
            cmsPageItem.Size = new System.Drawing.Size(61, 4);
            // 
            // TemplateForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(780, 539);
            Controls.Add(splitContainer);
            Controls.Add(clbBottomBar);
            Controls.Add(panelHead);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "TemplateForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "ApeForms Demo";
            panelHead.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelHead;
        private ControlListBox clbSideBar;
        private System.Windows.Forms.PictureBox picLogo;
        private ControlListBox clbTopBar;
        private ControlListBox clbBottomBar;
        protected SlideTabControl slideTabControl;
        private System.Windows.Forms.SplitContainer splitContainer;
        private RoundTextPanel rtpSearch;
        private System.Windows.Forms.ContextMenuStrip cmsPageItem;
    }
}

