using ApeFree.ApeForms.Core.Controls;

namespace ApeFree.ApeForms.Core.Controls
{
    partial class SlideTabControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tsTitle = new System.Windows.Forms.ToolStrip();
            this.cmsTitleItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPreviousPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNextPage = new System.Windows.Forms.ToolStripMenuItem();
            this.slideBox = new ApeFree.ApeForms.Core.Controls.SlideBox();
            this.cmsTitleItem.SuspendLayout();
            this.cmsTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsTitle
            // 
            this.tsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tsTitle.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTitle.Location = new System.Drawing.Point(0, 0);
            this.tsTitle.Name = "tsTitle";
            this.tsTitle.Size = new System.Drawing.Size(456, 25);
            this.tsTitle.TabIndex = 0;
            this.tsTitle.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.TsTitle_ItemAdded);
            this.tsTitle.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.TsTitle_ItemClicked);
            this.tsTitle.ItemRemoved += new System.Windows.Forms.ToolStripItemEventHandler(this.TsTitle_ItemRemoved);
            // 
            // cmsTitleItem
            // 
            this.cmsTitleItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClose,
            this.tsmiCloseAll});
            this.cmsTitleItem.Name = "cmsTitleItem";
            this.cmsTitleItem.Size = new System.Drawing.Size(126, 48);
            // 
            // tsmiClose
            // 
            this.tsmiClose.Name = "tsmiClose";
            this.tsmiClose.Size = new System.Drawing.Size(125, 22);
            this.tsmiClose.Text = "Close";
            this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
            // 
            // tsmiCloseAll
            // 
            this.tsmiCloseAll.Name = "tsmiCloseAll";
            this.tsmiCloseAll.Size = new System.Drawing.Size(125, 22);
            this.tsmiCloseAll.Text = "Close all";
            this.tsmiCloseAll.Click += new System.EventHandler(this.tsmiCloseAll_Click);
            // 
            // cmsTitle
            // 
            this.cmsTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPreviousPage,
            this.tsmiNextPage});
            this.cmsTitle.Name = "cmsTitle";
            this.cmsTitle.Size = new System.Drawing.Size(113, 48);
            // 
            // tsmiPreviousPage
            // 
            this.tsmiPreviousPage.Name = "tsmiPreviousPage";
            this.tsmiPreviousPage.Size = new System.Drawing.Size(112, 22);
            this.tsmiPreviousPage.Text = "上一页";
            this.tsmiPreviousPage.Click += new System.EventHandler(this.tsmiPreviousPage_Click);
            // 
            // tsmiNextPage
            // 
            this.tsmiNextPage.Name = "tsmiNextPage";
            this.tsmiNextPage.Size = new System.Drawing.Size(112, 22);
            this.tsmiNextPage.Text = "下一页";
            this.tsmiNextPage.Click += new System.EventHandler(this.tsmiNextPage_Click);
            // 
            // slideBox
            // 
            this.slideBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slideBox.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.slideBox.Location = new System.Drawing.Point(0, 25);
            this.slideBox.Margin = new System.Windows.Forms.Padding(0);
            this.slideBox.Name = "slideBox";
            this.slideBox.Rate = 1;
            this.slideBox.ReviseValue = 5;
            this.slideBox.Size = new System.Drawing.Size(456, 247);
            this.slideBox.TabIndex = 1;
            // 
            // SlideTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.slideBox);
            this.Controls.Add(this.tsTitle);
            this.Name = "SlideTabControl";
            this.Size = new System.Drawing.Size(456, 272);
            this.cmsTitleItem.ResumeLayout(false);
            this.cmsTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTitle;
        private SlideBox slideBox;
        private System.Windows.Forms.ContextMenuStrip cmsTitleItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseAll;
        private System.Windows.Forms.ContextMenuStrip cmsTitle;
        private System.Windows.Forms.ToolStripMenuItem tsmiPreviousPage;
        private System.Windows.Forms.ToolStripMenuItem tsmiNextPage;
    }
}
