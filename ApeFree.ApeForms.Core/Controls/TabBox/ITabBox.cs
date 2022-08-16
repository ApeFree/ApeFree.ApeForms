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
        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs e);
        public event PageChangedEventHandler PageChanged;

        /// <summary>
        /// 添加新的页面
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容控件</param>
        /// <param name="icon">图标</param>
        T AddPage(string title, Control content, Image icon = null);
        Control RemovePage(string title);
        void RemovePage(Control content);
        void RemovePage(int index);

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
}
