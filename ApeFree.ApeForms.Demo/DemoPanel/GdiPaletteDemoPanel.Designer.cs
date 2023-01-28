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
            this.panelCanvas = new System.Windows.Forms.Panel();
            this.gbCanvas.SuspendLayout();
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
            this.gbCanvas.Controls.Add(this.panelCanvas);
            this.gbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCanvas.Location = new System.Drawing.Point(0, 0);
            this.gbCanvas.Name = "gbCanvas";
            this.gbCanvas.Size = new System.Drawing.Size(560, 363);
            this.gbCanvas.TabIndex = 0;
            this.gbCanvas.TabStop = false;
            this.gbCanvas.Text = "Canvas";
            // 
            // panelCanvas
            // 
            this.panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCanvas.Location = new System.Drawing.Point(3, 17);
            this.panelCanvas.Name = "panelCanvas";
            this.panelCanvas.Size = new System.Drawing.Size(554, 343);
            this.panelCanvas.TabIndex = 0;
            // 
            // GdiPaletteDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbCanvas);
            this.Name = "GdiPaletteDemoPanel";
            this.Size = new System.Drawing.Size(560, 363);
            this.gbCanvas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.GroupBox gbCanvas;
        private System.Windows.Forms.Panel panelCanvas;
    }
}
