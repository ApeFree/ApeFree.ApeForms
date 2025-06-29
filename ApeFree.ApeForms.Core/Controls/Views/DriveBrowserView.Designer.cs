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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriveBrowserView));
            imageList = new System.Windows.Forms.ImageList(components);
            panelTop = new System.Windows.Forms.Panel();
            tbPath = new RoundTextPanel();
            btnDrive = new SimpleButton();
            btnPrevious = new SimpleButton();
            btnGoTo = new SimpleButton();
            btnBack = new SimpleButton();
            cmsDrives = new System.Windows.Forms.ContextMenuStrip(components);
            cmsFileContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            colName = new System.Windows.Forms.ColumnHeader();
            colSize = new System.Windows.Forms.ColumnHeader();
            colCteationTime = new System.Windows.Forms.ColumnHeader();
            listView = new System.Windows.Forms.ListView();
            labStatus = new System.Windows.Forms.Label();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // imageList
            // 
            imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = System.Drawing.Color.Transparent;
            imageList.Images.SetKeyName(0, "Folder");
            imageList.Images.SetKeyName(1, "File");
            // 
            // panelTop
            // 
            panelTop.Controls.Add(tbPath);
            panelTop.Controls.Add(btnDrive);
            panelTop.Controls.Add(btnPrevious);
            panelTop.Controls.Add(btnGoTo);
            panelTop.Controls.Add(btnBack);
            panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            panelTop.Location = new System.Drawing.Point(0, 0);
            panelTop.Margin = new System.Windows.Forms.Padding(4);
            panelTop.MinimumSize = new System.Drawing.Size(0, 35);
            panelTop.Name = "panelTop";
            panelTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            panelTop.Size = new System.Drawing.Size(473, 35);
            panelTop.TabIndex = 3;
            // 
            // tbPath
            // 
            tbPath.BackColor = System.Drawing.SystemColors.Window;
            tbPath.BorderColor = System.Drawing.SystemColors.Highlight;
            tbPath.BorderWidth = (ushort)2;
            tbPath.CornerRadius = 20;
            tbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            tbPath.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            tbPath.Hint = null;
            tbPath.HintColor = System.Drawing.Color.Gray;
            tbPath.Location = new System.Drawing.Point(93, 0);
            tbPath.Margin = new System.Windows.Forms.Padding(4);
            tbPath.MinimumSize = new System.Drawing.Size(30, 22);
            tbPath.Name = "tbPath";
            tbPath.ReadOnly = false;
            tbPath.Size = new System.Drawing.Size(349, 31);
            tbPath.TabIndex = 1;
            tbPath.Text = "C:\\";
            tbPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            tbPath.KeyDown += tbPath_KeyDown;
            // 
            // btnDrive
            // 
            btnDrive.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnDrive.BorderColor = System.Drawing.Color.Empty;
            btnDrive.BorderSize = 1;
            btnDrive.Dock = System.Windows.Forms.DockStyle.Left;
            btnDrive.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(1, 135, 225);
            btnDrive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(1, 99, 164);
            btnDrive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(1, 147, 246);
            btnDrive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDrive.Font = new System.Drawing.Font("等线", 15F);
            btnDrive.ForeColor = System.Drawing.Color.WhiteSmoke;
            btnDrive.Icon = null;
            btnDrive.IconScaling = 0.6F;
            btnDrive.Location = new System.Drawing.Point(62, 0);
            btnDrive.Margin = new System.Windows.Forms.Padding(4);
            btnDrive.Name = "btnDrive";
            btnDrive.Size = new System.Drawing.Size(31, 31);
            btnDrive.TabIndex = 3;
            btnDrive.Text = "💽";
            btnDrive.UsePureColorIcon = true;
            btnDrive.UseVisualStyleBackColor = true;
            btnDrive.Click += OnDriveButtonClicked;
            // 
            // btnPrevious
            // 
            btnPrevious.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnPrevious.BorderColor = System.Drawing.Color.Empty;
            btnPrevious.BorderSize = 1;
            btnPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            btnPrevious.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(1, 135, 225);
            btnPrevious.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(1, 99, 164);
            btnPrevious.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(1, 147, 246);
            btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnPrevious.Font = new System.Drawing.Font("等线", 15F);
            btnPrevious.ForeColor = System.Drawing.Color.WhiteSmoke;
            btnPrevious.Icon = null;
            btnPrevious.IconScaling = 0.6F;
            btnPrevious.Location = new System.Drawing.Point(31, 0);
            btnPrevious.Margin = new System.Windows.Forms.Padding(4);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new System.Drawing.Size(31, 31);
            btnPrevious.TabIndex = 4;
            btnPrevious.Text = "⏫";
            btnPrevious.UsePureColorIcon = true;
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += OnPreviousButtonClicked;
            // 
            // btnGoTo
            // 
            btnGoTo.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnGoTo.BorderColor = System.Drawing.Color.Empty;
            btnGoTo.BorderSize = 1;
            btnGoTo.Dock = System.Windows.Forms.DockStyle.Right;
            btnGoTo.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(1, 135, 225);
            btnGoTo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(1, 99, 164);
            btnGoTo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(1, 147, 246);
            btnGoTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGoTo.Font = new System.Drawing.Font("等线", 15F);
            btnGoTo.ForeColor = System.Drawing.Color.White;
            btnGoTo.Icon = null;
            btnGoTo.IconScaling = 0.6F;
            btnGoTo.Location = new System.Drawing.Point(442, 0);
            btnGoTo.Margin = new System.Windows.Forms.Padding(4);
            btnGoTo.Name = "btnGoTo";
            btnGoTo.Size = new System.Drawing.Size(31, 31);
            btnGoTo.TabIndex = 2;
            btnGoTo.Text = "➡︎";
            btnGoTo.UsePureColorIcon = true;
            btnGoTo.UseVisualStyleBackColor = true;
            btnGoTo.Click += OnGoToButtonClicked;
            // 
            // btnBack
            // 
            btnBack.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            btnBack.BorderColor = System.Drawing.Color.Empty;
            btnBack.BorderSize = 1;
            btnBack.Dock = System.Windows.Forms.DockStyle.Left;
            btnBack.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(1, 135, 225);
            btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(1, 99, 164);
            btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(1, 147, 246);
            btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnBack.Font = new System.Drawing.Font("等线", 15F);
            btnBack.ForeColor = System.Drawing.Color.WhiteSmoke;
            btnBack.Icon = null;
            btnBack.IconScaling = 0.6F;
            btnBack.Location = new System.Drawing.Point(0, 0);
            btnBack.Margin = new System.Windows.Forms.Padding(4);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(31, 31);
            btnBack.TabIndex = 0;
            btnBack.Text = "🔙";
            btnBack.UsePureColorIcon = true;
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += OnBackButtonClicked;
            // 
            // cmsDrives
            // 
            cmsDrives.Name = "cmsDrives";
            cmsDrives.Size = new System.Drawing.Size(61, 4);
            // 
            // cmsFileContextMenu
            // 
            cmsFileContextMenu.Name = "cmsFileContextMenu";
            cmsFileContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // colName
            // 
            colName.Text = "Name";
            colName.Width = 206;
            // 
            // colSize
            // 
            colSize.Text = "Size";
            colSize.Width = 148;
            // 
            // colCteationTime
            // 
            colCteationTime.Text = "Time";
            colCteationTime.Width = 131;
            // 
            // listView
            // 
            listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { colName, colSize, colCteationTime });
            listView.Dock = System.Windows.Forms.DockStyle.Fill;
            listView.LargeImageList = imageList;
            listView.Location = new System.Drawing.Point(0, 35);
            listView.Margin = new System.Windows.Forms.Padding(4);
            listView.Name = "listView";
            listView.Size = new System.Drawing.Size(473, 391);
            listView.SmallImageList = imageList;
            listView.TabIndex = 2;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = System.Windows.Forms.View.Details;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            listView.MouseClick += OnFileItemMouseClicked;
            listView.MouseDoubleClick += OnListViewMouseDoubleClicked;
            // 
            // labStatus
            // 
            labStatus.AutoEllipsis = true;
            labStatus.BackColor = System.Drawing.Color.White;
            labStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            labStatus.ForeColor = System.Drawing.SystemColors.ControlDark;
            labStatus.Location = new System.Drawing.Point(0, 426);
            labStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labStatus.Name = "labStatus";
            labStatus.Size = new System.Drawing.Size(473, 17);
            labStatus.TabIndex = 4;
            // 
            // DriveBrowserView
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.White;
            Controls.Add(listView);
            Controls.Add(labStatus);
            Controls.Add(panelTop);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "DriveBrowserView";
            Size = new System.Drawing.Size(473, 443);
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
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
        private System.Windows.Forms.Label labStatus;
    }
}
