namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class DatePickerDemoPanel
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labDate = new System.Windows.Forms.Label();
            this.alignCenterBox3 = new ApeFree.ApeForms.Core.Controls.AlignCenterBox();
            this.yearPicker1 = new ApeFree.ApeForms.Core.Controls.YearPicker();
            this.alignCenterBox2 = new ApeFree.ApeForms.Core.Controls.AlignCenterBox();
            this.monthPicker1 = new ApeFree.ApeForms.Core.Controls.MonthPicker();
            this.alignCenterBox1 = new ApeFree.ApeForms.Core.Controls.AlignCenterBox();
            this.daysPicker1 = new ApeFree.ApeForms.Core.Controls.DaysPicker();
            this.alignCenterBox4 = new ApeFree.ApeForms.Core.Controls.AlignCenterBox();
            this.datePicker1 = new ApeFree.ApeForms.Core.Controls.DatePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.alignCenterBox3.SuspendLayout();
            this.alignCenterBox2.SuspendLayout();
            this.alignCenterBox1.SuspendLayout();
            this.alignCenterBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.alignCenterBox1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 250);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DaysPicker";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.alignCenterBox2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 550);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(629, 250);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MonthPicker";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.alignCenterBox3);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 800);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(629, 250);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "YearPicker";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.alignCenterBox4);
            this.groupBox4.Controls.Add(this.labDate);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(629, 300);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "DatePicker";
            // 
            // labDate
            // 
            this.labDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labDate.Location = new System.Drawing.Point(3, 254);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(623, 43);
            this.labDate.TabIndex = 1;
            this.labDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // alignCenterBox3
            // 
            this.alignCenterBox3.Controls.Add(this.yearPicker1);
            this.alignCenterBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alignCenterBox3.Location = new System.Drawing.Point(3, 17);
            this.alignCenterBox3.MainControl = this.yearPicker1;
            this.alignCenterBox3.Name = "alignCenterBox3";
            this.alignCenterBox3.Size = new System.Drawing.Size(623, 230);
            this.alignCenterBox3.TabIndex = 0;
            // 
            // yearPicker1
            // 
            this.yearPicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yearPicker1.Location = new System.Drawing.Point(161, 15);
            this.yearPicker1.MiddleYear = 2022;
            this.yearPicker1.Name = "yearPicker1";
            this.yearPicker1.Size = new System.Drawing.Size(300, 200);
            this.yearPicker1.StartYear = 2018;
            this.yearPicker1.TabIndex = 0;
            // 
            // alignCenterBox2
            // 
            this.alignCenterBox2.Controls.Add(this.monthPicker1);
            this.alignCenterBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alignCenterBox2.Location = new System.Drawing.Point(3, 17);
            this.alignCenterBox2.MainControl = this.monthPicker1;
            this.alignCenterBox2.Name = "alignCenterBox2";
            this.alignCenterBox2.Size = new System.Drawing.Size(623, 230);
            this.alignCenterBox2.TabIndex = 0;
            // 
            // monthPicker1
            // 
            this.monthPicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.monthPicker1.Location = new System.Drawing.Point(161, 15);
            this.monthPicker1.Name = "monthPicker1";
            this.monthPicker1.Size = new System.Drawing.Size(300, 200);
            this.monthPicker1.TabIndex = 0;
            this.monthPicker1.Year = 2022;
            // 
            // alignCenterBox1
            // 
            this.alignCenterBox1.Controls.Add(this.daysPicker1);
            this.alignCenterBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alignCenterBox1.Location = new System.Drawing.Point(3, 17);
            this.alignCenterBox1.MainControl = this.daysPicker1;
            this.alignCenterBox1.Name = "alignCenterBox1";
            this.alignCenterBox1.Size = new System.Drawing.Size(623, 230);
            this.alignCenterBox1.TabIndex = 0;
            // 
            // daysPicker1
            // 
            this.daysPicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.daysPicker1.Location = new System.Drawing.Point(161, 15);
            this.daysPicker1.Month = 5;
            this.daysPicker1.Name = "daysPicker1";
            this.daysPicker1.Size = new System.Drawing.Size(300, 200);
            this.daysPicker1.TabIndex = 0;
            this.daysPicker1.Year = 2022;
            // 
            // alignCenterBox4
            // 
            this.alignCenterBox4.Controls.Add(this.datePicker1);
            this.alignCenterBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alignCenterBox4.Location = new System.Drawing.Point(3, 17);
            this.alignCenterBox4.MainControl = this.datePicker1;
            this.alignCenterBox4.Name = "alignCenterBox4";
            this.alignCenterBox4.Size = new System.Drawing.Size(623, 237);
            this.alignCenterBox4.TabIndex = 0;
            // 
            // datePicker1
            // 
            this.datePicker1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.datePicker1.Location = new System.Drawing.Point(161, 18);
            this.datePicker1.Month = 1;
            this.datePicker1.Name = "datePicker1";
            this.datePicker1.Size = new System.Drawing.Size(300, 200);
            this.datePicker1.TabIndex = 0;
            this.datePicker1.Year = 0;
            // 
            // DatePickerDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Name = "DatePickerDemoPanel";
            this.Size = new System.Drawing.Size(629, 464);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.alignCenterBox3.ResumeLayout(false);
            this.alignCenterBox2.ResumeLayout(false);
            this.alignCenterBox1.ResumeLayout(false);
            this.alignCenterBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Core.Controls.AlignCenterBox alignCenterBox1;
        private Core.Controls.DaysPicker daysPicker1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Core.Controls.AlignCenterBox alignCenterBox2;
        private Core.Controls.MonthPicker monthPicker1;
        private System.Windows.Forms.GroupBox groupBox3;
        private Core.Controls.AlignCenterBox alignCenterBox3;
        private Core.Controls.YearPicker yearPicker1;
        private System.Windows.Forms.GroupBox groupBox4;
        private Core.Controls.AlignCenterBox alignCenterBox4;
        private Core.Controls.DatePicker datePicker1;
        private System.Windows.Forms.Label labDate;
    }
}
