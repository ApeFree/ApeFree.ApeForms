using ApeFree.ApeForms.Core.Controls;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class ToastDemoPanel
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnShowToast = new SimpleButton();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarToastDelay = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.tbtnToastMode0 = new System.Windows.Forms.RadioButton();
            this.labToastCount = new System.Windows.Forms.Label();
            this.tbToastContent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbtnToastMode1 = new System.Windows.Forms.RadioButton();
            this.tbtnToastMode2 = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToastDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnShowToast);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.trackBarToastDelay);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.tbtnToastMode0);
            this.groupBox3.Controls.Add(this.labToastCount);
            this.groupBox3.Controls.Add(this.tbToastContent);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbtnToastMode1);
            this.groupBox3.Controls.Add(this.tbtnToastMode2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(651, 242);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Toast";
            // 
            // btnShowToast
            // 
            this.btnShowToast.AutoSize = true;
            this.btnShowToast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnShowToast.ForeColor = System.Drawing.Color.White;
            this.btnShowToast.Location = new System.Drawing.Point(5, 56);
            this.btnShowToast.Name = "btnShowToast";
            this.btnShowToast.Size = new System.Drawing.Size(91, 37);
            this.btnShowToast.TabIndex = 22;
            this.btnShowToast.Title = "Show";
            this.btnShowToast.Click += new System.EventHandler(this.btnShowToast_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(2, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "Delay(ms)";
            // 
            // trackBarToastDelay
            // 
            this.trackBarToastDelay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarToastDelay.Location = new System.Drawing.Point(2, 135);
            this.trackBarToastDelay.Minimum = 1;
            this.trackBarToastDelay.Name = "trackBarToastDelay";
            this.trackBarToastDelay.Size = new System.Drawing.Size(647, 45);
            this.trackBarToastDelay.TabIndex = 20;
            this.trackBarToastDelay.Value = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(2, 180);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "ToastMode（显示模式）";
            // 
            // tbtnToastMode0
            // 
            this.tbtnToastMode0.AutoSize = true;
            this.tbtnToastMode0.Checked = true;
            this.tbtnToastMode0.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbtnToastMode0.Location = new System.Drawing.Point(2, 192);
            this.tbtnToastMode0.Name = "tbtnToastMode0";
            this.tbtnToastMode0.Size = new System.Drawing.Size(647, 16);
            this.tbtnToastMode0.TabIndex = 16;
            this.tbtnToastMode0.TabStop = true;
            this.tbtnToastMode0.Text = "Queue - 队列模式：消息加入队列，顺序显示";
            this.tbtnToastMode0.UseVisualStyleBackColor = true;
            // 
            // labToastCount
            // 
            this.labToastCount.AutoSize = true;
            this.labToastCount.Location = new System.Drawing.Point(272, 56);
            this.labToastCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labToastCount.Name = "labToastCount";
            this.labToastCount.Size = new System.Drawing.Size(11, 12);
            this.labToastCount.TabIndex = 15;
            this.labToastCount.Text = "0";
            this.labToastCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbToastContent
            // 
            this.tbToastContent.Location = new System.Drawing.Point(4, 30);
            this.tbToastContent.Margin = new System.Windows.Forms.Padding(2);
            this.tbToastContent.Name = "tbToastContent";
            this.tbToastContent.Size = new System.Drawing.Size(279, 21);
            this.tbToastContent.TabIndex = 5;
            this.tbToastContent.Text = "Toast message";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(2, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Content:";
            // 
            // tbtnToastMode1
            // 
            this.tbtnToastMode1.AutoSize = true;
            this.tbtnToastMode1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbtnToastMode1.Location = new System.Drawing.Point(2, 208);
            this.tbtnToastMode1.Name = "tbtnToastMode1";
            this.tbtnToastMode1.Size = new System.Drawing.Size(647, 16);
            this.tbtnToastMode1.TabIndex = 17;
            this.tbtnToastMode1.Text = "Preemption - 抢占模式：清除队列，下一次弹出时显示";
            this.tbtnToastMode1.UseVisualStyleBackColor = true;
            // 
            // tbtnToastMode2
            // 
            this.tbtnToastMode2.AutoSize = true;
            this.tbtnToastMode2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbtnToastMode2.Location = new System.Drawing.Point(2, 224);
            this.tbtnToastMode2.Name = "tbtnToastMode2";
            this.tbtnToastMode2.Size = new System.Drawing.Size(647, 16);
            this.tbtnToastMode2.TabIndex = 18;
            this.tbtnToastMode2.Text = "Reuse - 复用模式：清除队列，使用当前正在显示的Toast";
            this.tbtnToastMode2.UseVisualStyleBackColor = true;
            // 
            // NotificationDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "NotificationDemoPanel";
            this.Size = new System.Drawing.Size(651, 436);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToastDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar trackBarToastDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton tbtnToastMode0;
        private System.Windows.Forms.Label labToastCount;
        private System.Windows.Forms.TextBox tbToastContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton tbtnToastMode1;
        private System.Windows.Forms.RadioButton tbtnToastMode2;
        private SimpleButton btnShowToast;
    }
}
