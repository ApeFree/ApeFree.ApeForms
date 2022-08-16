namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class ShutterDemoPanel
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
            this.simpleButtonShutter1 = new ApeFree.ApeForms.Core.Controls.SimpleButtonShutter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButtonShutter1
            // 
            this.simpleButtonShutter1.Location = new System.Drawing.Point(6, 20);
            this.simpleButtonShutter1.Name = "simpleButtonShutter1";
            this.simpleButtonShutter1.OpenState = false;
            this.simpleButtonShutter1.Size = new System.Drawing.Size(131, 37);
            this.simpleButtonShutter1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.simpleButtonShutter1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 377);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SimpleButtonShutter";
            // 
            // ShutterDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ShutterDemoPanel";
            this.Size = new System.Drawing.Size(481, 377);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Core.Controls.SimpleButtonShutter simpleButtonShutter1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
