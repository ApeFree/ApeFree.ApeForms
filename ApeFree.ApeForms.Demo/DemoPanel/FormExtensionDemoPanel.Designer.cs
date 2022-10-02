namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class FormExtensionDemoPanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGraduallyShowDefault = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnShakeFormDefault = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnGraduallyShow1 = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnGraduallyShow2 = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnShakeForm1 = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnShakeForm2 = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GraduallyShow";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 65);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shake";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnGraduallyShowDefault);
            this.flowLayoutPanel1.Controls.Add(this.btnGraduallyShow1);
            this.flowLayoutPanel1.Controls.Add(this.btnGraduallyShow2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(584, 45);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnShakeFormDefault);
            this.flowLayoutPanel2.Controls.Add(this.btnShakeForm1);
            this.flowLayoutPanel2.Controls.Add(this.btnShakeForm2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(584, 45);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // btnGraduallyShowDefault
            // 
            this.btnGraduallyShowDefault.AutoSize = true;
            this.btnGraduallyShowDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGraduallyShowDefault.ForeColor = System.Drawing.Color.White;
            this.btnGraduallyShowDefault.Location = new System.Drawing.Point(3, 3);
            this.btnGraduallyShowDefault.Name = "btnGraduallyShowDefault";
            this.btnGraduallyShowDefault.Size = new System.Drawing.Size(91, 37);
            this.btnGraduallyShowDefault.TabIndex = 0;
            this.btnGraduallyShowDefault.Title = "Default";
            this.btnGraduallyShowDefault.Click += new System.EventHandler(this.btnGraduallyShowDefault_Click);
            // 
            // btnShakeFormDefault
            // 
            this.btnShakeFormDefault.AutoSize = true;
            this.btnShakeFormDefault.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnShakeFormDefault.ForeColor = System.Drawing.Color.White;
            this.btnShakeFormDefault.Location = new System.Drawing.Point(3, 3);
            this.btnShakeFormDefault.Name = "btnShakeFormDefault";
            this.btnShakeFormDefault.Size = new System.Drawing.Size(91, 37);
            this.btnShakeFormDefault.TabIndex = 1;
            this.btnShakeFormDefault.Title = "Default";
            this.btnShakeFormDefault.Click += new System.EventHandler(this.btnShakeFormDefault_Click);
            // 
            // btnGraduallyShow1
            // 
            this.btnGraduallyShow1.AutoSize = true;
            this.btnGraduallyShow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGraduallyShow1.ForeColor = System.Drawing.Color.White;
            this.btnGraduallyShow1.Location = new System.Drawing.Point(100, 3);
            this.btnGraduallyShow1.Name = "btnGraduallyShow1";
            this.btnGraduallyShow1.Size = new System.Drawing.Size(91, 37);
            this.btnGraduallyShow1.TabIndex = 1;
            this.btnGraduallyShow1.Title = "gradientValue 0.02";
            this.btnGraduallyShow1.Click += new System.EventHandler(this.btnGraduallyShow1_Click);
            // 
            // btnGraduallyShow2
            // 
            this.btnGraduallyShow2.AutoSize = true;
            this.btnGraduallyShow2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGraduallyShow2.ForeColor = System.Drawing.Color.White;
            this.btnGraduallyShow2.Location = new System.Drawing.Point(197, 3);
            this.btnGraduallyShow2.Name = "btnGraduallyShow2";
            this.btnGraduallyShow2.Size = new System.Drawing.Size(91, 37);
            this.btnGraduallyShow2.TabIndex = 2;
            this.btnGraduallyShow2.Title = "gradientValue 0.1";
            this.btnGraduallyShow2.Click += new System.EventHandler(this.btnGraduallyShow2_Click);
            // 
            // btnShakeForm1
            // 
            this.btnShakeForm1.AutoSize = true;
            this.btnShakeForm1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnShakeForm1.ForeColor = System.Drawing.Color.White;
            this.btnShakeForm1.Location = new System.Drawing.Point(100, 3);
            this.btnShakeForm1.Name = "btnShakeForm1";
            this.btnShakeForm1.Size = new System.Drawing.Size(91, 37);
            this.btnShakeForm1.TabIndex = 2;
            this.btnShakeForm1.Title = "16 times";
            this.btnShakeForm1.Click += new System.EventHandler(this.btnShakeForm1_Click);
            // 
            // btnShakeForm2
            // 
            this.btnShakeForm2.AutoSize = true;
            this.btnShakeForm2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnShakeForm2.ForeColor = System.Drawing.Color.White;
            this.btnShakeForm2.Location = new System.Drawing.Point(197, 3);
            this.btnShakeForm2.Name = "btnShakeForm2";
            this.btnShakeForm2.Size = new System.Drawing.Size(91, 37);
            this.btnShakeForm2.TabIndex = 3;
            this.btnShakeForm2.Title = "amplitude 40";
            this.btnShakeForm2.Click += new System.EventHandler(this.btnShakeForm2_Click);
            // 
            // FormExtensionDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormExtensionDemoPanel";
            this.Size = new System.Drawing.Size(590, 457);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Core.Controls.SimpleButton btnGraduallyShowDefault;
        private Core.Controls.SimpleButton btnShakeFormDefault;
        private Core.Controls.SimpleButton btnGraduallyShow1;
        private Core.Controls.SimpleButton btnGraduallyShow2;
        private Core.Controls.SimpleButton btnShakeForm1;
        private Core.Controls.SimpleButton btnShakeForm2;
    }
}
