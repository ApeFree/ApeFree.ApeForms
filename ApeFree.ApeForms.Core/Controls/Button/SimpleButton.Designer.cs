namespace ApeFree.ApeForms.Core.Controls
{
    partial class SimpleButton
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
            this.labButtonText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labButtonText
            // 
            this.labButtonText.AutoEllipsis = true;
            this.labButtonText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.labButtonText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labButtonText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labButtonText.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labButtonText.ForeColor = System.Drawing.Color.White;
            this.labButtonText.Location = new System.Drawing.Point(0, 0);
            this.labButtonText.Name = "labButtonText";
            this.labButtonText.Size = new System.Drawing.Size(91, 37);
            this.labButtonText.TabIndex = 0;
            this.labButtonText.Text = this.Text;
            this.labButtonText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labButtonText.Click += new System.EventHandler(this.labButtonText_Click);
            this.labButtonText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labButtonText_MouseDown);
            this.labButtonText.MouseLeave += new System.EventHandler(this.labButtonText_MouseLeave);
            this.labButtonText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labButtonText_MouseMove);
            this.labButtonText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labButtonText_MouseUp);
            // 
            // SimpleButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.Controls.Add(this.labButtonText);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "SimpleButton";
            this.Size = new System.Drawing.Size(91, 37);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labButtonText;
    }
}
