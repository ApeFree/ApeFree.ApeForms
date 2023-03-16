using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    public interface ITabBox<T>
    {
        public event EventHandler<PageChangedEventArgs> PageChanged;
        public event EventHandler<PageRemovedEventArgs> PageRemoved;

        /// <summary>
        /// 添加新的页面
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容控件</param>
        /// <param name="icon">图标</param>
        T AddPage(string title, Control content, Image icon = null);
        Control RemovePage(string title);
        Control RemovePage(int index);
        void RemovePage(Control content);

        void Jump(string title);
        void Jump(Control content);
        void Jump(int index);

        int CurrentIndex { get; }

        void NextPage();
        void PreviousPage();


    }

    public class PageChangedEventArgs : EventArgs
    {
        public int PageIndex { get; set; }

        public PageChangedEventArgs(int pageIndex)
        {
            PageIndex = pageIndex;
        }
    }

    public class PageRemovedEventArgs : EventArgs
    {
        public PageRemovedEventArgs(Control page)
        {
            PageControl = page;
        }

        public Control PageControl { get; }
    }
}
