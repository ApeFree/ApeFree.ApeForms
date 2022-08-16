namespace ApeFree.ApeForms.Core.Controls
{
    partial class DatePicker
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
            this.slide = new SlideBox();
            this.yearPicker = new YearPicker();
            this.daysPicker = new DaysPicker();
            this.monthPicker = new MonthPicker();
            this.SuspendLayout();
            // 
            // slide
            // 
            this.slide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.slide.Location = new System.Drawing.Point(0, 0);
            this.slide.Margin = new System.Windows.Forms.Padding(0);
            this.slide.Name = "slide";
            this.slide.Rate = 5;
            this.slide.ReviseValue = 5;
            this.slide.Size = new System.Drawing.Size(200, 160);
            this.slide.TabIndex = 0;
            // 
            // yearPicker
            // 
            this.yearPicker.Location = new System.Drawing.Point(526, 17);
            this.yearPicker.Name = "yearPicker";
            this.yearPicker.Size = new System.Drawing.Size(188, 118);
            this.yearPicker.TabIndex = 1;
            // 
            // daysPicker
            // 
            this.daysPicker.Location = new System.Drawing.Point(527, 259);
            this.daysPicker.Month = 6;
            this.daysPicker.Name = "daysPicker";
            this.daysPicker.Size = new System.Drawing.Size(187, 140);
            this.daysPicker.TabIndex = 3;
            // 
            // monthPicker
            // 
            this.monthPicker.Location = new System.Drawing.Point(526, 141);
            this.monthPicker.Name = "monthPicker";
            this.monthPicker.Size = new System.Drawing.Size(188, 112);
            this.monthPicker.TabIndex = 4;
            // 
            // DatePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.monthPicker);
            this.Controls.Add(this.daysPicker);
            this.Controls.Add(this.yearPicker);
            this.Controls.Add(this.slide);
            this.Name = "DatePicker";
            this.Size = new System.Drawing.Size(200, 160);
            this.ResumeLayout(false);

        }

        #endregion

        private SlideBox slide;
        private YearPicker yearPicker;
        private DaysPicker daysPicker;
        private MonthPicker monthPicker;
    }
}
