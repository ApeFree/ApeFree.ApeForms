namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class GridViewDemoPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridViewDemoPanel));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridView = new ApeFree.ApeForms.Controls.Grids.GridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tbDisplayRow = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectedColor = new System.Windows.Forms.ToolStripButton();
            this.btnHoveredColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectionMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSelectionModeSingleCell = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectionModeEntireRow = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectionModeEntireColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSelectionModeBothRowAndColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 455);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GridView";
            // 
            // gridView
            // 
            this.gridView.AllowEdit = true;
            this.gridView.BackColor = System.Drawing.Color.White;
            this.gridView.Columns = new ApeFree.ApeForms.Controls.Grids.GridView.ColumnSettings[0];
            this.gridView.DataSource = ((System.Collections.Generic.List<object[]>)(resources.GetObject("gridView.DataSource")));
            this.gridView.DisplayColumn = 6;
            this.gridView.DisplayRow = 10;
            this.gridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridView.HeaderFont = null;
            this.gridView.HeaderHeight = 25;
            this.gridView.HoveredBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.gridView.Location = new System.Drawing.Point(3, 17);
            this.gridView.Name = "gridView";
            this.gridView.SelectedBackColor = System.Drawing.SystemColors.Highlight;
            this.gridView.SelectedForeColor = System.Drawing.Color.White;
            this.gridView.SelectionMode = ApeFree.ApeForms.Controls.Grids.GridView.CellSelectionMode.EntireRow;
            this.gridView.Size = new System.Drawing.Size(494, 435);
            this.gridView.TabIndex = 0;
            this.gridView.TopRowIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd,
            this.btnRemove,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tbDisplayRow,
            this.toolStripSeparator2,
            this.btnSelectedColor,
            this.btnHoveredColor,
            this.toolStripSeparator3,
            this.btnSelectionMode});
            this.toolStrip1.Location = new System.Drawing.Point(3, 17);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(494, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAdd.ForeColor = System.Drawing.Color.Green;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(28, 22);
            this.btnAdd.Text = "➕";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRemove.ForeColor = System.Drawing.Color.Red;
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(28, 22);
            this.btnRemove.Text = "➖︎";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Rows";
            // 
            // tbDisplayRow
            // 
            this.tbDisplayRow.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.tbDisplayRow.Name = "tbDisplayRow";
            this.tbDisplayRow.Size = new System.Drawing.Size(50, 25);
            this.tbDisplayRow.Text = "10";
            this.tbDisplayRow.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbDisplayRow.TextChanged += new System.EventHandler(this.tbDisplayRow_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSelectedColor
            // 
            this.btnSelectedColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectedColor.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.btnSelectedColor.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectedColor.Image")));
            this.btnSelectedColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectedColor.Name = "btnSelectedColor";
            this.btnSelectedColor.Size = new System.Drawing.Size(81, 22);
            this.btnSelectedColor.Text = "🎨 Selected";
            this.btnSelectedColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectedColor.Click += new System.EventHandler(this.btnSelectedColor_Click);
            // 
            // btnHoveredColor
            // 
            this.btnHoveredColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHoveredColor.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnHoveredColor.Image = ((System.Drawing.Image)(resources.GetObject("btnHoveredColor.Image")));
            this.btnHoveredColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHoveredColor.Name = "btnHoveredColor";
            this.btnHoveredColor.Size = new System.Drawing.Size(82, 22);
            this.btnHoveredColor.Text = "🎨 Hovered";
            this.btnHoveredColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnHoveredColor.Click += new System.EventHandler(this.btnHoveredColor_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSelectionMode
            // 
            this.btnSelectionMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectionMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectionModeSingleCell,
            this.btnSelectionModeEntireRow,
            this.btnSelectionModeEntireColumn,
            this.btnSelectionModeBothRowAndColumn});
            this.btnSelectionMode.ForeColor = System.Drawing.Color.Black;
            this.btnSelectionMode.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectionMode.Image")));
            this.btnSelectionMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectionMode.Name = "btnSelectionMode";
            this.btnSelectionMode.Size = new System.Drawing.Size(126, 22);
            this.btnSelectionMode.Text = "⛶ Selection Mode";
            this.btnSelectionMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSelectionModeSingleCell
            // 
            this.btnSelectionModeSingleCell.Name = "btnSelectionModeSingleCell";
            this.btnSelectionModeSingleCell.Size = new System.Drawing.Size(195, 22);
            this.btnSelectionModeSingleCell.Text = "SingleCell";
            this.btnSelectionModeSingleCell.Click += new System.EventHandler(this.btnSelectionMode_Click);
            // 
            // btnSelectionModeEntireRow
            // 
            this.btnSelectionModeEntireRow.Name = "btnSelectionModeEntireRow";
            this.btnSelectionModeEntireRow.Size = new System.Drawing.Size(195, 22);
            this.btnSelectionModeEntireRow.Text = "EntireRow";
            this.btnSelectionModeEntireRow.Click += new System.EventHandler(this.btnSelectionMode_Click);
            // 
            // btnSelectionModeEntireColumn
            // 
            this.btnSelectionModeEntireColumn.Name = "btnSelectionModeEntireColumn";
            this.btnSelectionModeEntireColumn.Size = new System.Drawing.Size(195, 22);
            this.btnSelectionModeEntireColumn.Text = "EntireColumn";
            this.btnSelectionModeEntireColumn.Click += new System.EventHandler(this.btnSelectionMode_Click);
            // 
            // btnSelectionModeBothRowAndColumn
            // 
            this.btnSelectionModeBothRowAndColumn.Name = "btnSelectionModeBothRowAndColumn";
            this.btnSelectionModeBothRowAndColumn.Size = new System.Drawing.Size(195, 22);
            this.btnSelectionModeBothRowAndColumn.Text = "BothRowAndColumn";
            this.btnSelectionModeBothRowAndColumn.Click += new System.EventHandler(this.btnSelectionMode_Click);
            // 
            // GridViewDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "GridViewDemoPanel";
            this.Size = new System.Drawing.Size(500, 500);
            this.Load += new System.EventHandler(this.GridViewDemoPanel_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ApeFree.ApeForms.Controls.Grids.GridView gridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAdd;
        private System.Windows.Forms.ToolStripButton btnRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tbDisplayRow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnSelectedColor;
        private System.Windows.Forms.ToolStripButton btnHoveredColor;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton btnSelectionMode;
        private System.Windows.Forms.ToolStripMenuItem btnSelectionModeSingleCell;
        private System.Windows.Forms.ToolStripMenuItem btnSelectionModeEntireRow;
        private System.Windows.Forms.ToolStripMenuItem btnSelectionModeEntireColumn;
        private System.Windows.Forms.ToolStripMenuItem btnSelectionModeBothRowAndColumn;
    }
}
