using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public static class User32Extensions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

        /// <summary>
        /// 调用user32.dll操作窗体以完成更复杂的功能
        /// </summary>
        /// <param name="form"></param>
        /// <param name="mode">操作模式</param>
        /// <returns></returns>
        public static bool ShowWindow(this Form form, ShowWindowMode mode)
        {
            return ShowWindow(new HandleRef(form, form.Handle), (int)mode);
        }
    }

    /// <summary>
    /// 显示窗体的操作模式
    /// </summary>
    public enum ShowWindowMode : byte
    {
        /// <summary>
        /// 隐藏窗口并激活其他窗口
        /// </summary>
        Hide = 0,

        /// <summary>
        /// 激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志
        /// </summary>
        ShowNormal = 1,

        /// <summary>
        /// 激活窗口并将其最小化
        /// </summary>
        ShowMinimized = 2,

        /// <summary>
        /// 激活窗口并将其最大化
        /// </summary>
        ShowMaximized = 3,

        /// <summary>
        /// 以窗口最近一次的大小和状态显示窗口。此值与ShowNormal相似，只是窗口没有被激活
        /// </summary>
        ShowNoactivate = 4,

        /// <summary>
        /// 在窗口原来的位置以原来的尺寸激活和显示窗口
        /// </summary>
        Show = 5,

        /// <summary>
        /// 最小化指定的窗口并且激活在z序中的下一个顶层窗口
        /// </summary>
        Minimize = 6,

        /// <summary>
        /// 最小化的方式显示窗口，此值与ShowMinimized相似，只是窗口没有被激活
        /// </summary>
        ShowMinnoactive = 7,

        /// <summary>
        /// 以窗口原来的状态显示窗口。此值与Show相似，只是窗口没有被激活
        /// </summary>
        Showna = 8,

        /// <summary>
        /// 激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志
        /// </summary>
        Restore = 9,

        /// <summary>
        /// 依据在startupinfo结构中指定的flag标志设定显示状态，startupinfo 结构是由启动应用程序的程序传递给createprocess函数的
        /// </summary>
        ShowDefault = 10,

        /// <summary>
        /// 最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数
        /// </summary>
        ForceMinimize = 11,
    }
}
