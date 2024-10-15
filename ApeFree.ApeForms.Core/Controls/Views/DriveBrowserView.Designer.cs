using ApeFree.ApeForms.Core.Controls;

namespace ApeFree.ApeForms.Core.Controls.Views
{
    partial class DriveBrowserView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriveBrowserView));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbPath = new ApeFree.ApeForms.Core.Controls.RoundTextPanel();
            this.btnDrive = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnPrevious = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnGoTo = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnBack = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.cmsDrives = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsFileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCteationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Folder");
            this.imageList.Images.SetKeyName(1, "File");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbPath);
            this.panel1.Controls.Add(this.btnDrive);
            this.panel1.Controls.Add(this.btnPrevious);
            this.panel1.Controls.Add(this.btnGoTo);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel1.Size = new System.Drawing.Size(600, 35);
            this.panel1.TabIndex = 3;
            // 
            // tbPath
            // 
            this.tbPath.BackColor = System.Drawing.SystemColors.Window;
            this.tbPath.BorderColor = System.Drawing.SystemColors.Highlight;
            this.tbPath.BorderWidth = ((ushort)(2));
            this.tbPath.CornerRadius = 20;
            this.tbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPath.Hint = null;
            this.tbPath.HintColor = System.Drawing.Color.Gray;
            this.tbPath.Location = new System.Drawing.Point(105, 0);
            this.tbPath.MinimumSize = new System.Drawing.Size(30, 22);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = false;
            this.tbPath.Size = new System.Drawing.Size(460, 32);
            this.tbPath.TabIndex = 1;
            this.tbPath.Text = "C:\\";
            this.tbPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tbPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPath_KeyDown);
            // 
            // btnDrive
            // 
            this.btnDrive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnDrive.BorderColor = System.Drawing.Color.Empty;
            this.btnDrive.BorderSize = 1;
            this.btnDrive.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDrive.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(135)))), ((int)(((byte)(225)))));
            this.btnDrive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(164)))));
            this.btnDrive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(147)))), ((int)(((byte)(246)))));
            this.btnDrive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrive.Font = new System.Drawing.Font("等线", 15F);
            this.btnDrive.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnDrive.Icon = null;
            this.btnDrive.IconScaling = 0.6F;
            this.btnDrive.Location = new System.Drawing.Point(70, 0);
            this.btnDrive.Name = "btnDrive";
            this.btnDrive.Size = new System.Drawing.Size(35, 32);
            this.btnDrive.TabIndex = 3;
            this.btnDrive.Text = "💽";
            this.btnDrive.UsePureColorIcon = true;
            this.btnDrive.UseVisualStyleBackColor = true;
            this.btnDrive.Click += new System.EventHandler(this.OnDriveButtonClicked);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnPrevious.BorderColor = System.Drawing.Color.Empty;
            this.btnPrevious.BorderSize = 1;
            this.btnPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrevious.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(135)))), ((int)(((byte)(225)))));
            this.btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(164)))));
            this.btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(147)))), ((int)(((byte)(246)))));
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("等线", 15F);
            this.btnPrevious.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrevious.Icon = null;
            this.btnPrevious.IconScaling = 0.6F;
            this.btnPrevious.Location = new System.Drawing.Point(35, 0);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(35, 32);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "⏫";
            this.btnPrevious.UsePureColorIcon = true;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.OnPreviousButtonClicked);
            // 
            // btnGoTo
            // 
            this.btnGoTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnGoTo.BorderColor = System.Drawing.Color.Empty;
            this.btnGoTo.BorderSize = 1;
            this.btnGoTo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnGoTo.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(135)))), ((int)(((byte)(225)))));
            this.btnGoTo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(164)))));
            this.btnGoTo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(147)))), ((int)(((byte)(246)))));
            this.btnGoTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoTo.Font = new System.Drawing.Font("等线", 15F);
            this.btnGoTo.ForeColor = System.Drawing.Color.White;
            this.btnGoTo.Icon = null;
            this.btnGoTo.IconScaling = 0.6F;
            this.btnGoTo.Location = new System.Drawing.Point(565, 0);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(35, 32);
            this.btnGoTo.TabIndex = 2;
            this.btnGoTo.Text = "➡︎";
            this.btnGoTo.UsePureColorIcon = true;
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.OnGoToButtonClicked);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnBack.BorderColor = System.Drawing.Color.Empty;
            this.btnBack.BorderSize = 1;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBack.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(135)))), ((int)(((byte)(225)))));
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(99)))), ((int)(((byte)(164)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(147)))), ((int)(((byte)(246)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("等线", 15F);
            this.btnBack.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnBack.Icon = null;
            this.btnBack.IconScaling = 0.6F;
            this.btnBack.Location = new System.Drawing.Point(0, 0);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(35, 32);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "🔙";
            this.btnBack.UsePureColorIcon = true;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.OnBackButtonClicked);
            // 
            // cmsDrives
            // 
            this.cmsDrives.Name = "cmsDrives";
            this.cmsDrives.Size = new System.Drawing.Size(61, 4);
            // 
            // cmsFileContextMenu
            // 
            this.cmsFileContextMenu.Name = "cmsFileContextMenu";
            this.cmsFileContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 206;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 148;
            // 
            // colCteationTime
            // 
            this.colCteationTime.Text = "Time";
            this.colCteationTime.Width = 131;
            // 
            // listView
            // 
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSize,
            this.colCteationTime});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.HideSelection = false;
            this.listView.LargeImageList = this.imageList;
            this.listView.Location = new System.Drawing.Point(0, 35);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(600, 365);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OnFileItemMouseClicked);
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnListViewMouseDoubleClicked);
            // 
            // DriveBrowserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.panel1);
            this.Name = "DriveBrowserView";
            this.Size = new System.Drawing.Size(600, 400);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        protected SimpleButton btnBack;
        protected ApeForms.Core.Controls.RoundTextPanel tbPath;
        protected SimpleButton btnGoTo;
        protected SimpleButton btnDrive;
        protected System.Windows.Forms.ImageList imageList;
        protected System.Windows.Forms.ContextMenuStrip cmsDrives;
        protected System.Windows.Forms.ContextMenuStrip cmsFileContextMenu;
        protected System.Windows.Forms.ColumnHeader colName;
        protected System.Windows.Forms.ColumnHeader colSize;
        protected System.Windows.Forms.ColumnHeader colCteationTime;
        protected System.Windows.Forms.ListView listView;
        protected SimpleButton btnPrevious;
    }
}
