using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    /// <summary>
    /// 页面切换事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);

    [DefaultEvent("PageChanged")]
    public partial class SlideBox : UserControl
    {
        private int pageIndex = -1;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get { return pageIndex; } }

        /// <summary>
        /// 页面总数
        /// </summary>
        public int PageCount { get { return panel.Controls.Count; } }

        [Browsable(true)]
        [Description("翻页时移动的速率，当Rate = 1时，无翻页动画效果，Rate值越大翻页越慢")]
        public int Rate { get; set; } = 5;

        [Browsable(true)]
        [Description("翻页时最小的移动速度(像素单位)")]
        public int ReviseValue { get; set; } = 5;

        [Browsable(true)]
        [Description("当页面切换时触发")]
        public event PageChangedEventHandler PageChanged;

        public SlideBox()
        {
            InitializeComponent();

            panel.Size = Size;
            panel.Left = panel.Top = 0;
            panel.AutoScroll = false;
            panel.AutoSize = true;

            ControlAdded += (s, e) =>
            {
                AddPage(e.Control);
            };
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            panel.SuspendLayout();
            panel.Height = Height;

            foreach (Control page in panel.Controls)
            {
                page.Width = Width;
                page.Height = panel.Height;
            }

            panel.ResumeLayout();

            if (pageIndex != -1)
            {
                panel.Left = -panel.Controls[pageIndex].Left;
            }
        }

        public int AddPage(Control control)
        {
            PerformLayout();
            Panel p = new Panel();
            p.SuspendLayout();
            p.Size = Size;
            p.Visible = true;
            p.Margin = new Padding(0);
            p.Padding = new Padding(0);
            p.Controls.Add(control);
            p.ControlRemoved += (s, e) =>
            {
                // 当控件移除时，SlideBox和Page之间的Panel也需要移除
                if (p.Controls.Count == 0) panel.Controls.Remove(p);
            };
            control.Dock = DockStyle.Fill;
            panel.SuspendLayout();
            panel.Controls.Add(p);
            p.ResumeLayout();
            panel.ResumeLayout();

            if (pageIndex == -1) pageIndex = 0;

            // 返回当前页面的
            return PageCount - 1;
        }



        public void RemovePage(Control control)
        {
            int i = 0;
            foreach (Control c in panel.Controls)
            {
                if (c.Controls.Count > 0 && c.Controls[0] == control)
                {
                    RemovePageAt(i);
                    return;
                }
                i++;
            }
        }

        public void Clear()
        {
            panel.TraverseChildControls(ctrl => ctrl.Dispose());
            panel.Controls.Clear();
        }

        public Control GetPage(int pageNum)
        {
            // SlideBox和Page之间有一层普通Panel
            return panel.Controls[pageNum].Controls[0];
        }

        public void RemovePageAt(int pageNum)
        {
            panel.Controls.RemoveAt(pageNum);
            if (pageNum <= pageIndex)
            {
                pageIndex--;
                if (pageIndex >= 0)
                    panel.Left = -panel.Controls[pageIndex].Left;
            }
        }

        public void Jump(int pageNum)
        {
            if (pageNum < 0 || pageNum >= panel.Controls.Count) return;
            pageIndex = pageNum;

            var targetLeft = -panel.Controls[pageNum].Left;

            if (Rate <= 0)
            {
                Rate = 1;
            }

            panel.LocationGradualChange(new Point(targetLeft, panel.Top), (byte)Rate);
        }

        public void NextPage()
        {
            Jump(pageIndex + 1);
        }

        public void PreviousPage()
        {
            Jump(pageIndex - 1);
        }
    }

    //public class PageChangedEventArgs : EventArgs
    //{
    //    public int PageIndex { get; set; }

    //    public PageChangedEventArgs(int pageIndex)
    //    {
    //        PageIndex = pageIndex;
    //    }
    //}
}
