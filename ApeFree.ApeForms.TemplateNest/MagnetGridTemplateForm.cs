using ApeFree.ApeForms.Core.Controls;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.TemplateNest
{
    public partial class MagnetGridTemplateForm : Form
    {
        public MagnetGridTemplateForm()
        {
            InitializeComponent();
            SearchInputPanel.TextChanged += SearchInputPanel_TextChanged;
        }

        private void SearchInputPanel_TextChanged(object sender, EventArgs e)
        {
            var magnets = MagnetTable.Controls.Cast<Magnet>();
            foreach (var item in magnets)
            {
                item.Visible = item.Title.Contains(SearchInputPanel.Text) || item.Title.MatchByWildcard(SearchInputPanel.Text);
            };
        }

        /// <summary>
        /// 跳转界面
        /// </summary>
        protected void JumpTo(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;

            Hide();
            form.ShowDialog();
            form.Dispose();
            Show();
        }


        /// <summary>
        /// 添加子程序启动项
        /// </summary>
        /// <param name="item"></param>
        protected void AddItem(MagnetGirdItem item)
        {
            var magent = new Magnet();
            magent.Title = item.Title;
            magent.Image = item.Image;
            magent.Content = item.Description;
            magent.BackColor = item.BackColor;
            magent.ForeColor = item.ForeColor;
            magent.ImageSizeMode = item.ImageSizeMode;
            magent.Dock = DockStyle.Fill;
            magent.Parent = MagnetTable;
            magent.Click += (s, e) => item.ClickCallback?.Invoke();
            magent.MouseMove += (s, e) => StatusLabel.Text = $"{item.Title} - {item.Description}";
            MagnetTable.SetRowSpan(magent, item.Size.Height);
            MagnetTable.SetColumnSpan(magent, item.Size.Width);

            OnMagnetAdded(magent);
        }

        /// <summary>
        /// 磁力贴添加到界面时
        /// </summary>
        /// <param name="magnet"></param>
        protected virtual void OnMagnetAdded(Magnet magnet) { }
    }

    /// <summary>
    /// 磁力贴表格项
    /// </summary>
    public class MagnetGirdItem
    {
        /// <summary>
        /// 图标
        /// </summary>
        public Image Image { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 单击回调
        /// </summary>
        public Action ClickCallback { get; set; }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; } = Color.White;

        /// <summary>
        /// 尺寸（X,Y表示跨越单元格的数量）
        /// </summary>
        public Size Size { get; set; } = new Size(1, 1);

        /// <summary>
        /// 图形尺寸模式
        /// </summary>
        public PictureBoxSizeMode ImageSizeMode { get; set; } = PictureBoxSizeMode.Zoom;
    }
}
