using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApeFree.ApeForms.Core.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Drawing2D;

namespace ApeFree.ApeForms.Core.Controls
{
    public partial class SlideTabControl : UserControl, ITabBox<ToolStripItem>
    {
        public StateColorSet StateColorSet { get; set; } = new StateColorSet();

        public SlideTabControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面切换时触发此事件
        /// </summary>
        public event EventHandler<PageChangedEventArgs> PageChanged;

        /// <summary>
        /// 页面移除时触发此事件
        /// </summary>
        public event EventHandler<PageRemovedEventArgs> PageRemoved;

        /// <summary>
        /// 所有页面对象
        /// </summary>
        public Dictionary<ToolStripItem, Control> Pages = new Dictionary<ToolStripItem, Control>();

        /// <summary>
        /// 页面标题栏停靠的位置
        /// </summary>
        [Browsable(true)]
        [Description("页面标题栏停靠的位置")]
        public DockStyle TitleDock { get => tsTitle.Dock; set => tsTitle.Dock = value; }

        /// <summary>
        /// 页面标题栏项目的对齐方式
        /// </summary>
        [Browsable(true)]
        [Description("页面标题栏项目的对齐方式")]
        public ToolStripLayoutStyle TitleLayoutStyle { get => tsTitle.LayoutStyle; set => tsTitle.LayoutStyle = value; }

        /// <summary>
        /// 翻页时移动的速率
        /// </summary>
        [Browsable(true)]
        [Description("翻页时移动的速率，当Rate = 1时，无翻页动画效果，Rate值越大翻页越慢")]
        public int Rate { get => slideBox.Rate; set => slideBox.Rate = value; }

        /// <summary>
        /// 关闭页面选项的文本
        /// </summary>
        public string ClosePageOptionText { get => tsmiClose.Text; set => tsmiClose.Text = value; }

        /// <summary>
        /// 关闭所有页面选项的文本
        /// </summary>
        public string CloseAllPagesOptionText { get => tsmiCloseAll.Text; set => tsmiCloseAll.Text = value; }

        /// <summary>
        /// 当前页面序号
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        /// 标题栏文本格式
        /// </summary>
        // public Font PageTitleFont { get; set; }

        [Browsable(true)]
        [Description("显示或隐藏单个页面的标签是否提供关闭按钮")]
        public bool ShowPageCloseButton
        {
            get
            {
                return showPageCloseButton;
            }
            set
            {
                showPageCloseButton = value;
                Pages.Keys.ForEach(x => ((TabStripButton)x).ShowCloseButton = showPageCloseButton);
            }
        }
        private bool showPageCloseButton = true;

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public ToolStripItem AddPage(string title, Control content, Image icon = null)
        {
            // 检查加入的控件是否已存在，若存在则跳转
            if (Pages.Values.Contains(content))
            {
                Jump(content);
                foreach (var kv in Pages)
                {
                    if (kv.Value == content)
                    {
                        return kv.Key;
                    }
                }
            }

            // 当控件销毁时移除页面
            content.Disposed += (s, e) =>
            {
                RemovePage(content);
            };

            // 创建新标签
            var item = new TabStripButton(title, icon, null);
            tsTitle.Items.Add(item);
            Pages.Add(item, content);
            slideBox.AddPage(content);
            item.CloseButtonClickHandler = (s, e) => RemovePage(title);

            if (tsTitle.Items.Count == 1)
            {
                Jump(0);
            }
            else
            {
                Jump(content);
            }

            return item;
        }

        /// <summary>
        /// 跳转到指定标题的页面
        /// </summary>
        /// <param name="title"></param>
        public void Jump(string title)
        {
            foreach (ToolStripItem item in tsTitle.Items)
            {
                PerformItemClicked(item);
            }
        }

        /// <summary>
        /// 跳转到指定控件的页面
        /// </summary>
        /// <param name="content"></param>
        public void Jump(Control content)
        {
            foreach (var kv in Pages)
            {
                if (content == kv.Value)
                {
                    PerformItemClicked(kv.Key);
                    return;
                }
            }
        }

        /// <summary>
        /// 跳转到指定序号的页面
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="ArgumentException"></exception>
        public void Jump(int index)
        {
            // 检查数据的合法性
            if (index < 0) throw new ArgumentException("无效的页面索引值", "index");

            // 如果当前无页面则不做跳转
            if (Pages.Count == 0) return;

            // 正常页码区间
            if (index < Pages.Count)
            {
                PerformItemClicked(tsTitle.Items[index]);
            }
            // 越界页码区间
            else
            {
                // 跳转至最后一页
                PerformItemClicked(tsTitle.Items[tsTitle.Items.Count - 1]);
            }
        }

        /// <summary>
        /// 跳转到下一个页面
        /// </summary>
        public void NextPage()
        {
            Jump(CurrentIndex + 1);
        }

        /// <summary>
        /// 跳转到前一个页面
        /// </summary>
        public void PreviousPage()
        {
            if (CurrentIndex > 0) Jump(CurrentIndex - 1);
        }

        /// <summary>
        /// 通过标题名称移除一个页面
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Control RemovePage(string title)
        {
            foreach (ToolStripItem item in tsTitle.Items)
            {
                if (item.Text == title)
                {
                    Control control = Pages[item];
                    tsTitle.Items.Remove(item);

                    PageRemoved?.Invoke(this, new PageRemovedEventArgs(control));

                    return control;
                }
            }
            return null;
        }

        /// <summary>
        /// 移除指定控件的页面
        /// </summary>
        /// <param name="content"></param>
        public void RemovePage(Control content)
        {
            foreach (var kv in Pages)
            {
                if (content == kv.Value)
                {
                    tsTitle.Items.Remove(kv.Key);
                    Jump(CurrentIndex);

                    if (content != null)
                    {
                        PageRemoved?.Invoke(this, new PageRemovedEventArgs(content));
                    }

                    return;
                }
            }


        }

        /// <summary>
        /// 移除指定序号的页面
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Control RemovePage(int index)
        {
            Control content = null;
            if (index < Pages.Count)
            {
                var tsi = tsTitle.Items.Cast<ToolStripItem>().ElementAt(index);
                content = Pages[tsi];
                tsTitle.Items.Remove(tsi);
                // tsTitle.Items.RemoveAt(index);
                Jump(CurrentIndex);
            }

            if (content != null)
            {
                PageRemoved?.Invoke(this, new PageRemovedEventArgs(content));
            }

            return content;
        }

        private void TsTitle_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            e.Item.Click += Item_Click;
            e.Item.MouseDown += Item_MouseDown;
            string name = e.Item.Text;
            int c = 1;
            while (HasCheckSameName(e.Item))
            {
                e.Item.Text = $"{name} {++c}";
            }
        }

        private bool HasCheckSameName(ToolStripItem newItem)
        {
            foreach (ToolStripItem item in tsTitle.Items)
            {
                if (newItem != item && newItem.Text == item.Text)
                {
                    return true;
                }
            }
            return false;
        }

        private ToolStripItem ActiveContextMenuTitleItem;



        private void Item_MouseDown(object sender, MouseEventArgs e)
        {
            // 当无页面的时候不做处理
            if (Pages.Count == 0) return;


            if (e.Button == MouseButtons.Right)
            {
                ActiveContextMenuTitleItem = (ToolStripItem)sender;

                tsmiClose.Image = ActiveContextMenuTitleItem.Image;

                Point mousePoint = Control.MousePosition;
                Point stripPoint = PointToScreen(ActiveContextMenuTitleItem.Owner.Location);

                if (TitleDock == DockStyle.Left || TitleDock == DockStyle.Right)
                {
                    int h = stripPoint.Y;
                    for (int i = 0; i < Pages.Count; ++i)
                    {
                        h += tsTitle.Items[i].Size.Height;
                        if (h > mousePoint.Y)
                        {
                            cmsTitleItem.Show(new Point(stripPoint.X, h));
                            return;
                        }
                    }
                    cmsTitleItem.Show(new Point(stripPoint.X, h));
                }
                else
                {
                    int w = stripPoint.X;
                    for (int i = 0; i < Pages.Count; ++i)
                    {
                        w += tsTitle.Items[i].Size.Width;
                        if (w > mousePoint.X)
                        {
                            cmsTitleItem.Show(new Point(w, stripPoint.Y));
                            return;
                        }
                    }
                    cmsTitleItem.Show(new Point(w, stripPoint.Y));
                }

                // cmsTitleItem.Show();
            }
        }

        private void TsTitle_ItemRemoved(object sender, ToolStripItemEventArgs e)
        {
            e.Item.Click -= Item_Click;

            slideBox.RemovePage(Pages[e.Item]);
            Pages.Remove(e.Item);
            Jump(CurrentIndex);
        }

        private void TsTitle_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int index = tsTitle.Items.IndexOf(e.ClickedItem);
            slideBox.Jump(index);
            CurrentIndex = index;
            PageChanged?.Invoke(sender, new PageChangedEventArgs(CurrentIndex));

            foreach (ToolStripItem item in tsTitle.Items)
            {
                if (item == e.ClickedItem)
                {
                    item.BackColor = StateColorSet.GotFocusBackColor;
                    item.ForeColor = StateColorSet.GotFocusForeColor;
                }
                else
                {
                    item.BackColor = StateColorSet.LostFocusBackColor;
                    item.ForeColor = StateColorSet.LostFocusForeColor;
                }
            }
        }

        private void PerformItemClicked(ToolStripItem item)
        {
            TsTitle_ItemClicked(tsTitle, new ToolStripItemClickedEventArgs(item));
        }

        private void Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem item in tsTitle.Items)
            {
                if (item == sender)
                {
                    item.BackColor = StateColorSet.GotFocusBackColor;
                    item.ForeColor = StateColorSet.GotFocusForeColor;
                }
                else
                {
                    item.BackColor = StateColorSet.LostFocusBackColor;
                    item.ForeColor = StateColorSet.LostFocusForeColor;
                }
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            RemovePage(tsTitle.Items.IndexOf(ActiveContextMenuTitleItem));
        }

        private void tsmiCloseAll_Click(object sender, EventArgs e)
        {
            var controls = Pages.Values.Cast<Control>().ToArray();

            foreach (Control control in controls)
            {
                if (control != null)
                {
                    RemovePage(control);
                }
            }

            tsTitle.Items.Clear();
            Pages.Clear();
        }

        private void tsmiPreviousPage_Click(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void tsmiNextPage_Click(object sender, EventArgs e)
        {
            NextPage();
        }
    }

    public class TabStripButton : ToolStripButton
    {
        private bool inside;
        private Rectangle rect;
        public EventHandler CloseButtonClickHandler;
        private bool showCloseButton;

        [Browsable(true)]
        [Description("显示或隐藏关闭按钮")]
        public bool ShowCloseButton
        {
            get => showCloseButton; set
            {
                if (showCloseButton != value)
                {
                    showCloseButton = value;
                    Invalidate();
                }
            }
        }

        public TabStripButton(string name, Image image, EventHandler onClick, bool showCloseButton = true) : base(name, image, onClick)
        {
            AutoSize = false;
            TextAlign = ContentAlignment.MiddleLeft;
            ShowCloseButton = showCloseButton;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            inside = rect.Contains(e.Location);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            inside = false;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (ShowCloseButton && inside)
            {
                CloseButtonClickHandler?.Invoke(this, e);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            // 计算标签的宽度
            var size = TextRenderer.MeasureText(Text, Font);
            Width = size.Width + size.Height * 2 + 15;
            Height = size.Height + 15;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ShowCloseButton)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var x = Height / 4;
                rect = new Rectangle(Width - 3 * x, x, x * 2, x * 2);

                using (var brush = new SolidBrush(inside ? BackColor.Luminance(0.6f) : BackColor))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }

                var border = 2;
                using (var pen = new Pen(ForeColor))
                {
                    var sp = new Point(Width - 3 * x, x);
                    e.Graphics.DrawLine(pen, new Point(sp.X + border, sp.Y + border), new Point(sp.X + 2 * x - border - 1, sp.Y + 2 * x - border - 1));
                    e.Graphics.DrawLine(pen, new Point(sp.X + border, sp.Y + 2 * x - border - 1), new Point(sp.X + 2 * x - border - 1, sp.Y + border));
                }
            }
        }
    }
}
