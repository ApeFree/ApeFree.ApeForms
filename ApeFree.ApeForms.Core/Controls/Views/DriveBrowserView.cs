using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApeFree.ApeForms.Core.Controls.Views
{
    /// <summary>
    /// 驱动浏览器控件
    /// </summary>
    public partial class DriveBrowserView : UserControl
    {
        /// <summary>
        /// 远程文件夹历史路径
        /// </summary>
        protected Stack<string> HistoryStack { get; } = new Stack<string>();

        /// <summary>
        /// 控件当前输入的远程文件夹路径
        /// </summary>
        public string CurrentPath => HistoryStack.Peek();

        /// <summary>
        /// 是否允许多选
        /// </summary>
        public bool MultiSelect { get => listView.MultiSelect; set => listView.MultiSelect = value; }

        /// <summary>
        /// 是否允许文件拖拽
        /// </summary>
        public override bool AllowDrop { get => listView.AllowDrop; set => listView.AllowDrop = value; }

        /// <summary>
        /// 显示项类型
        /// </summary>
        public DisplayItemType DisplayItemType { get; set; }

        /// <summary>
        /// 选中文件路径集合
        /// </summary>
        public string[] SelectedFiles { get; private set; } = new string[0];

        /// <summary>
        /// 选中文件夹路径集合
        /// </summary>
        public string[] SelectedFolders { get; private set; } = new string[0];

        /// <summary>
        /// 选中文件
        /// </summary>
        public string FilePath => SelectedFiles?.FirstOrDefault();

        /// <summary>
        /// 选中文件夹
        /// </summary>
        public string FolderPath => SelectedFolders?.FirstOrDefault();

        /// <summary>
        /// 与文件名匹配的搜索字符串
        /// </summary>
        public string SearchPattern { get; set; } = "*";

        /// <summary>
        /// 菜单项目被选中时出发此事件
        /// </summary>

        public event EventHandler OnSelectedItemsChanged;

        public DriveBrowserView()
        {
            InitializeComponent();
            AllowDrop = false;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = listView.SelectedItems.Cast<ListViewItem>();

            SelectedFiles = items.Where(x => x.ImageKey == "File").Select(x => Path.Combine(CurrentPath, x.Text)).ToArray();
            SelectedFolders = items.Where(x => x.ImageKey == "Folder").Select(x => Path.Combine(CurrentPath, x.Text)).ToArray();

            OnSelectedItemsChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            var path = CurrentPath;
            var folder = new DirectoryInfo(path);
            path = folder.Parent == null ? path : folder.Parent.FullName;

            OpenFolder(path);
        }

        protected virtual void OnListViewMouseDoubleClicked(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTest = listView.HitTest(e.X, e.Y);
            if (hitTest.Item == null)
            {
                return;
            }

            var dir = HistoryStack.Peek();

            if (hitTest.Item.Tag is DirectoryInfo di)
            {
                // 如果选中的时文件夹，就跳转进入该目录
                OpenFolder(di.FullName);
            }
            else if (hitTest.Item.Tag is FileInfo fi)
            {
                // 如果选中的时文件
                OnFileItemDoubleClicked(dir, fi, hitTest);
            }
        }

        protected virtual void OnGoToButtonClicked(object sender, EventArgs e)
        {
            var path = tbPath.Text;
            OpenFolder(path);
        }

        protected virtual void OnFileItemMouseClicked(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hitTest = listView.HitTest(e.X, e.Y);
            if (hitTest.Item == null)
            {
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                if (hitTest.Item.Tag is DirectoryInfo di)
                {
                    OpenFolderContextMenu(cmsFileContextMenu, di);
                }
                else if (hitTest.Item.Tag is FileInfo fi)
                {
                    OpenFileContextMenu(cmsFileContextMenu, fi);
                }
            }
        }

        protected virtual void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (HistoryStack.Count <= 1)
            {
                return;
            }

            HistoryStack.Pop();
            var path = HistoryStack.Peek();
            tbPath.Text = path;
            OpenFolder(path);
        }

        protected virtual void OnDriveButtonClicked(object sender, EventArgs e)
        {
            var dis = DriveInfo.GetDrives();
            cmsDrives.Items.Clear();

            var arr = dis.Select(x => new ToolStripMenuItem(x.Name, null, (_, arg) => OpenFolder(x.RootDirectory.FullName))).ToArray();
            cmsDrives.Items.AddRange(arr);

            cmsDrives.Show(btnDrive, new Point(0, btnDrive.Height));
        }

        #region 文件拖拽
        protected virtual void OnFileDragEnter(object sender, DragEventArgs e)
        {
            if (!AllowDrop)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        protected virtual void OnFileDragDrop(object sender, DragEventArgs e)
        {
            if (!AllowDrop)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var targetPath = Path.Combine(CurrentPath, fileName);
                    File.Move(file, targetPath);
                }
                OpenFolder(CurrentPath);
            }
        }
        #endregion

        /// <summary>
        /// 打开选中文件的快捷菜单
        /// </summary>
        /// <param name="contextItem"></param>
        protected virtual void OpenFileContextMenu(ContextMenuStrip cmsMenu, object contextItem) { }

        /// <summary>
        /// 打开选文件夹的快捷菜单
        /// </summary>
        /// <param name="cmsMenu"></param>
        /// <param name="di"></param>
        protected virtual void OpenFolderContextMenu(ContextMenuStrip cmsMenu, object contextItem) { }

        /// <summary>
        /// 当文件项被鼠标双击
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="item"></param>
        /// <param name="hitTest"></param>
        protected virtual void OnFileItemDoubleClicked(string dir, object contextItem, ListViewHitTestInfo hitTest) { }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="path"></param>
        public virtual bool OpenFolder(string path)
        {
            labStatus.Text = string.Empty;
            labStatus.Visible = false;

            DirectoryInfo directory = new DirectoryInfo(path);
            DirectoryInfo[] dirItems = null;
            FileInfo[] fileItems = null;

            try
            {
                dirItems = directory.GetDirectories();
                fileItems = DisplayItemType == DisplayItemType.FolderAndFile ? directory.GetFiles(SearchPattern) : [];
                SelectedFolders = [path];
            }
            catch (Exception ex)
            {
                labStatus.Text = ex.Message;
                labStatus.Visible = true;
                return false;
            }

            var lvis = new List<ListViewItem>();

            foreach (var item in dirItems)
            {
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.ImageKey = "Folder";
                lvi.Tag = item;
                lvis.Add(lvi);
            }

            foreach (var item in fileItems)
            {
                ListViewItem lvi = new ListViewItem(item.Name);
                lvi.SubItems.Add(item.Length.ToString());
                lvi.SubItems.Add(item.CreationTime.ToString());
                lvi.ImageKey = "File";
                lvi.Tag = item;
                lvis.Add(lvi);
            }

            listView.ModifyInUI(() =>
            {
                tbPath.Text = path;

                if (!HistoryStack.Any() || HistoryStack.Peek() != path)
                {
                    HistoryStack.Push(path);
                }

                listView.Items.Clear();
                if (lvis.Any())
                {
                    listView.Items.AddRange(lvis.ToArray());
                }
            });

            return true;
        }

        private void tbPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenFolder(tbPath.Text);
            }
        }
    }


    /// <summary>
    /// 显示项类型
    /// </summary>
    public enum DisplayItemType
    {
        /// <summary>
        /// 文件夹和文件
        /// </summary>
        FolderAndFile,

        /// <summary>
        /// 仅文件夹
        /// </summary>
        OnlyFolder,
    }
}
