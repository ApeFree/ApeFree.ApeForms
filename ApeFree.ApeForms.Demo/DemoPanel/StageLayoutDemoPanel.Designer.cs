namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class StageLayoutDemoPanel
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
            this.stageLayoutPanel = new ApeFree.ApeForms.Controls.Containers.StageLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.movableStageLayoutPanel = new ApeFree.ApeForms.Controls.Containers.MovableStageLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudRate = new System.Windows.Forms.NumericUpDown();
            this.cbSort = new System.Windows.Forms.CheckBox();
            this.btnAddTestControl = new ApeFree.ApeForms.Controls.Buttons.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.stageLayoutPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 380);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "StageLayoutPanel";
            // 
            // stageLayoutPanel
            // 
            this.stageLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stageLayoutPanel.GridItemSize = new System.Drawing.Size(100, 100);
            this.stageLayoutPanel.GridItemSortingHandler = null;
            this.stageLayoutPanel.Location = new System.Drawing.Point(3, 17);
            this.stageLayoutPanel.Name = "stageLayoutPanel";
            this.stageLayoutPanel.SeparationDistance = ((ushort)(20));
            this.stageLayoutPanel.Size = new System.Drawing.Size(556, 360);
            this.stageLayoutPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.movableStageLayoutPanel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(562, 380);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MovableStageLayoutPanel";
            // 
            // movableStageLayoutPanel
            // 
            this.movableStageLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.movableStageLayoutPanel.GridItemSize = new System.Drawing.Size(100, 100);
            this.movableStageLayoutPanel.GridItemSortingHandler = null;
            this.movableStageLayoutPanel.Location = new System.Drawing.Point(3, 17);
            this.movableStageLayoutPanel.MovingRateLevel = ((byte)(2));
            this.movableStageLayoutPanel.Name = "movableStageLayoutPanel";
            this.movableStageLayoutPanel.SeparationDistance = ((ushort)(20));
            this.movableStageLayoutPanel.Size = new System.Drawing.Size(556, 360);
            this.movableStageLayoutPanel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.nudRate);
            this.groupBox3.Controls.Add(this.cbSort);
            this.groupBox3.Controls.Add(this.btnAddTestControl);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(562, 60);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(123, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rate Level:";
            // 
            // nudRate
            // 
            this.nudRate.Location = new System.Drawing.Point(200, 25);
            this.nudRate.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRate.Name = "nudRate";
            this.nudRate.Size = new System.Drawing.Size(120, 21);
            this.nudRate.TabIndex = 2;
            this.nudRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRate.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudRate.ValueChanged += new System.EventHandler(this.nudRate_ValueChanged);
            // 
            // cbSort
            // 
            this.cbSort.AutoSize = true;
            this.cbSort.Location = new System.Drawing.Point(345, 26);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(48, 16);
            this.cbSort.TabIndex = 1;
            this.cbSort.Text = "Sort";
            this.cbSort.UseVisualStyleBackColor = true;
            this.cbSort.CheckedChanged += new System.EventHandler(this.cbSort_CheckedChanged);
            // 
            // btnAddTestControl
            // 
            this.btnAddTestControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAddTestControl.BorderColor = System.Drawing.Color.Empty;
            this.btnAddTestControl.BorderSize = 0;
            this.btnAddTestControl.FlatAppearance.BorderSize = 0;
            this.btnAddTestControl.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(135)))), ((int)(((byte)(225)))));
            this.btnAddTestControl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(164)))));
            this.btnAddTestControl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(147)))), ((int)(((byte)(246)))));
            this.btnAddTestControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTestControl.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnAddTestControl.Icon = null;
            this.btnAddTestControl.IconScaling = 0.6F;
            this.btnAddTestControl.Location = new System.Drawing.Point(6, 20);
            this.btnAddTestControl.Name = "btnAddTestControl";
            this.btnAddTestControl.Size = new System.Drawing.Size(98, 27);
            this.btnAddTestControl.TabIndex = 0;
            this.btnAddTestControl.Text = "Add Test Item";
            this.btnAddTestControl.UsePureColorIcon = true;
            this.btnAddTestControl.UseVisualStyleBackColor = false;
            this.btnAddTestControl.Click += new System.EventHandler(this.btnAddTestControl_Click);
            // 
            // StageLayoutDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "StageLayoutDemoPanel";
            this.Size = new System.Drawing.Size(562, 566);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private ApeFree.ApeForms.Controls.Containers.StageLayoutPanel stageLayoutPanel;
        private ApeFree.ApeForms.Controls.Containers.MovableStageLayoutPanel movableStageLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private ApeFree.ApeForms.Controls.Buttons.SimpleButton btnAddTestControl;
        private System.Windows.Forms.CheckBox cbSort;
        private System.Windows.Forms.NumericUpDown nudRate;
        private System.Windows.Forms.Label label1;
    }
}
