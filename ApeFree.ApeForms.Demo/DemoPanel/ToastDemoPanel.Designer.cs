using ApeFree.ApeForms.Controls.Buttons;
using ApeFree.ApeForms.Controls.Cards;
using ApeFree.ApeForms.Controls.Containers;
using ApeFree.ApeForms.Controls.DatePickers;
using ApeFree.ApeForms.Controls.Grids;
using ApeFree.ApeForms.Controls.Labels;
using ApeFree.ApeForms.Controls.ListBoxes;
using ApeFree.ApeForms.Controls.TabBoxes;
using ApeFree.ApeForms.Controls.TextBoxes;
using ApeFree.ApeForms.Controls.TitleBars;
using ApeFree.ApeForms.Controls.Views;
using ApeFree.ApeForms.Forms;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notifications;
using ApeFree.ApeForms.Forms.Templates;
using ApeFree.ApeForms.Extensions;
using ApeFree.ApeForms.Utils;

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
            this.comboBoxLocation = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnToastForeColor = new ApeFree.ApeForms.Controls.Buttons.SimpleButton();
            this.btnToastFont = new ApeFree.ApeForms.Controls.Buttons.SimpleButton();
            this.btnToastBackColor = new ApeFree.ApeForms.Controls.Buttons.SimpleButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.trackBarToastDelay = new System.Windows.Forms.TrackBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tbtnToastMode2 = new System.Windows.Forms.RadioButton();
            this.tbtnToastMode1 = new System.Windows.Forms.RadioButton();
            this.tbtnToastMode0 = new System.Windows.Forms.RadioButton();
            this.btnShowToast = new ApeFree.ApeForms.Controls.Buttons.SimpleButton();
            this.labToastCount = new System.Windows.Forms.Label();
            this.tbToastContent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToastDelay)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxLocation);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.btnShowToast);
            this.groupBox3.Controls.Add(this.labToastCount);
            this.groupBox3.Controls.Add(this.tbToastContent);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(651, 317);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Toast";
            // 
            // comboBoxLocation
            // 
            this.comboBoxLocation.FormattingEnabled = true;
            this.comboBoxLocation.Location = new System.Drawing.Point(288, 31);
            this.comboBoxLocation.Name = "comboBoxLocation";
            this.comboBoxLocation.Size = new System.Drawing.Size(121, 20);
            this.comboBoxLocation.TabIndex = 26;
            this.comboBoxLocation.Visible = false;
            this.comboBoxLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxLocation_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnToastForeColor);
            this.panel3.Controls.Add(this.btnToastFont);
            this.panel3.Controls.Add(this.btnToastBackColor);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(2, 118);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(647, 52);
            this.panel3.TabIndex = 25;
            // 
            // btnToastForeColor
            // 
            this.btnToastForeColor.BackColor = System.Drawing.Color.White;
            this.btnToastForeColor.BorderColor = System.Drawing.Color.Empty;
            this.btnToastForeColor.BorderSize = 1;
            this.btnToastForeColor.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnToastForeColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnToastForeColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnToastForeColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToastForeColor.ForeColor = System.Drawing.Color.Black;
            this.btnToastForeColor.Location = new System.Drawing.Point(109, 3);
            this.btnToastForeColor.Name = "btnToastForeColor";
            this.btnToastForeColor.Size = new System.Drawing.Size(100, 35);
            this.btnToastForeColor.TabIndex = 2;
            this.btnToastForeColor.Text = "Toast ForeColor";
            this.btnToastForeColor.UseVisualStyleBackColor = false;
            this.btnToastForeColor.Click += new System.EventHandler(this.btnToastForeColor_Click);
            // 
            // btnToastFont
            // 
            this.btnToastFont.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnToastFont.BorderColor = System.Drawing.Color.Empty;
            this.btnToastFont.BorderSize = 0;
            this.btnToastFont.FlatAppearance.BorderSize = 0;
            this.btnToastFont.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(132)))), ((int)(((byte)(236)))));
            this.btnToastFont.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.btnToastFont.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.btnToastFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToastFont.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnToastFont.Location = new System.Drawing.Point(215, 3);
            this.btnToastFont.Name = "btnToastFont";
            this.btnToastFont.Size = new System.Drawing.Size(100, 35);
            this.btnToastFont.TabIndex = 1;
            this.btnToastFont.Text = "Toast Font";
            this.btnToastFont.UseVisualStyleBackColor = false;
            this.btnToastFont.Click += new System.EventHandler(this.btnToastFont_Click);
            // 
            // btnToastBackColor
            // 
            this.btnToastBackColor.BackColor = System.Drawing.Color.Gray;
            this.btnToastBackColor.BorderColor = System.Drawing.Color.Empty;
            this.btnToastBackColor.BorderSize = 0;
            this.btnToastBackColor.FlatAppearance.BorderSize = 0;
            this.btnToastBackColor.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.btnToastBackColor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.btnToastBackColor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.btnToastBackColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToastBackColor.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnToastBackColor.Location = new System.Drawing.Point(3, 3);
            this.btnToastBackColor.Name = "btnToastBackColor";
            this.btnToastBackColor.Size = new System.Drawing.Size(100, 35);
            this.btnToastBackColor.TabIndex = 0;
            this.btnToastBackColor.Text = "Toast BackColor";
            this.btnToastBackColor.UseVisualStyleBackColor = false;
            this.btnToastBackColor.Click += new System.EventHandler(this.btnToastBackColor_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.trackBarToastDelay);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(2, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(647, 69);
            this.panel2.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(0, 12);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "Delay(ms)";
            // 
            // trackBarToastDelay
            // 
            this.trackBarToastDelay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBarToastDelay.Location = new System.Drawing.Point(0, 24);
            this.trackBarToastDelay.Minimum = 1;
            this.trackBarToastDelay.Name = "trackBarToastDelay";
            this.trackBarToastDelay.Size = new System.Drawing.Size(647, 45);
            this.trackBarToastDelay.TabIndex = 20;
            this.trackBarToastDelay.Value = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbtnToastMode2);
            this.panel1.Controls.Add(this.tbtnToastMode1);
            this.panel1.Controls.Add(this.tbtnToastMode0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(2, 239);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 76);
            this.panel1.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(0, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 19;
            this.label4.Text = "ToastMode（显示模式）";
            // 
            // tbtnToastMode2
            // 
            this.tbtnToastMode2.AutoSize = true;
            this.tbtnToastMode2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbtnToastMode2.Location = new System.Drawing.Point(0, 28);
            this.tbtnToastMode2.Name = "tbtnToastMode2";
            this.tbtnToastMode2.Size = new System.Drawing.Size(647, 16);
            this.tbtnToastMode2.TabIndex = 18;
            this.tbtnToastMode2.Text = "Reuse - 复用模式：清除队列，使用当前正在显示的Toast";
            this.tbtnToastMode2.UseVisualStyleBackColor = true;
            // 
            // tbtnToastMode1
            // 
            this.tbtnToastMode1.AutoSize = true;
            this.tbtnToastMode1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbtnToastMode1.Location = new System.Drawing.Point(0, 44);
            this.tbtnToastMode1.Name = "tbtnToastMode1";
            this.tbtnToastMode1.Size = new System.Drawing.Size(647, 16);
            this.tbtnToastMode1.TabIndex = 17;
            this.tbtnToastMode1.Text = "Preemption - 抢占模式：清除队列，下一次弹出时显示";
            this.tbtnToastMode1.UseVisualStyleBackColor = true;
            // 
            // tbtnToastMode0
            // 
            this.tbtnToastMode0.AutoSize = true;
            this.tbtnToastMode0.Checked = true;
            this.tbtnToastMode0.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbtnToastMode0.Location = new System.Drawing.Point(0, 60);
            this.tbtnToastMode0.Name = "tbtnToastMode0";
            this.tbtnToastMode0.Size = new System.Drawing.Size(647, 16);
            this.tbtnToastMode0.TabIndex = 16;
            this.tbtnToastMode0.TabStop = true;
            this.tbtnToastMode0.Text = "Queue - 队列模式：消息加入队列，顺序显示";
            this.tbtnToastMode0.UseVisualStyleBackColor = true;
            // 
            // btnShowToast
            // 
            this.btnShowToast.AutoSize = true;
            this.btnShowToast.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnShowToast.BorderColor = System.Drawing.Color.Empty;
            this.btnShowToast.BorderSize = 1;
            this.btnShowToast.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(224)))));
            this.btnShowToast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(97)))), ((int)(((byte)(163)))));
            this.btnShowToast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(146)))), ((int)(((byte)(244)))));
            this.btnShowToast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowToast.ForeColor = System.Drawing.Color.White;
            this.btnShowToast.Location = new System.Drawing.Point(5, 56);
            this.btnShowToast.Name = "btnShowToast";
            this.btnShowToast.Size = new System.Drawing.Size(100, 35);
            this.btnShowToast.TabIndex = 22;
            this.btnShowToast.Text = "Show";
            this.btnShowToast.UseVisualStyleBackColor = false;
            this.btnShowToast.Click += new System.EventHandler(this.btnShowToast_Click);
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
            // ToastDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "ToastDemoPanel";
            this.Size = new System.Drawing.Size(651, 436);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToastDelay)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private SimpleButton btnToastBackColor;
        private System.Windows.Forms.ComboBox comboBoxLocation;
        private SimpleButton btnToastFont;
        private SimpleButton btnToastForeColor;
    }
}
