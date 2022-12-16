namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class GdiPaletteDemoPanel
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
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.gbCanvas = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 500;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // gbCanvas
            // 
            this.gbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCanvas.Location = new System.Drawing.Point(0, 0);
            this.gbCanvas.Name = "gbCanvas";
            this.gbCanvas.Size = new System.Drawing.Size(560, 363);
            this.gbCanvas.TabIndex = 0;
            this.gbCanvas.TabStop = false;
            this.gbCanvas.Text = "Canvas";
            // 
            // GdiPaletteDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbCanvas);
            this.Name = "GdiPaletteDemoPanel";
            this.Size = new System.Drawing.Size(560, 363);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.GroupBox gbCanvas;
    }
}
