using ApeFree.ApeForms.Core.Properties;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    partial class NavigationTitleBar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationTitleBar));
            this.RightButton = new ApeFree.ApeForms.Core.Controls.ImageButton();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.LeftButton = new ApeFree.ApeForms.Core.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // RightButton
            // 
            this.RightButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.RightButton.BackgroundImage = Resources.NavigationTitleBarRightButtonImage;
            this.RightButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.RightButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RightButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightButton.ForeColor = System.Drawing.Color.White;
            this.RightButton.Location = new System.Drawing.Point(188, 0);
            this.RightButton.Margin = new System.Windows.Forms.Padding(2);
            this.RightButton.Name = "RightButton";
            this.RightButton.UseAlphaChannel = true;
            this.RightButton.Size = new System.Drawing.Size(112, 28);
            this.RightButton.TabIndex = 2;
            this.RightButton.UsePureColor = true;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.TitleLabel.Location = new System.Drawing.Point(112, 0);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(76, 28);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LeftButton
            // 
            this.LeftButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.LeftButton.BackgroundImage = Resources.NavigationTitleBarLeftButtonImage;
            this.LeftButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LeftButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LeftButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftButton.ForeColor = System.Drawing.Color.White;
            this.LeftButton.Location = new System.Drawing.Point(0, 0);
            this.LeftButton.Margin = new System.Windows.Forms.Padding(2);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.UseAlphaChannel = true;
            this.LeftButton.Size = new System.Drawing.Size(112, 28);
            this.LeftButton.TabIndex = 1;
            this.LeftButton.UsePureColor = true;
            // 
            // NavigationTitleBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.RightButton);
            this.Controls.Add(this.LeftButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NavigationTitleBar";
            this.Size = new System.Drawing.Size(300, 28);
            this.ResumeLayout(false);

        }

        #endregion
        protected ImageButton LeftButton;
        protected ImageButton RightButton;
        public Label TitleLabel;
    }
}
