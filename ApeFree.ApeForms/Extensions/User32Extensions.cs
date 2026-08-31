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
        private const int CS_DropShadow = 0x20000;
        private const int GCL_Style = -26;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

        /// <summary>
        /// 设置窗口Z轴位置
        /// 参考资料：https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-setwindowpos
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hPos, int x, int y, int cx, int cy, SetWindowPosFlags flags);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary> 
        /// 得到当前活动的窗口 
        /// </summary> 
        /// <returns></returns> 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();

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

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="form"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static bool ShowWindow(this Form form, int mode)
        {
            return ShowWindow(new HandleRef(form, form.Handle), mode);
        }

        /// <summary>
        /// 投射阴影
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static int DropShadow(this Form form)
        {
            return SetClassLong(form.Handle, GCL_Style, GetClassLong(form.Handle, GCL_Style) | CS_DropShadow);
        }

        /// <summary>
        /// 将窗口设置为顶部且不抢占焦点
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static bool SetWindowToTopWithoutFocus(this Form form)
        {
            return SetWindowPos(form.Handle, new IntPtr(-1), 0, 0, 0, 0, SetWindowPosFlags.NoActivate | SetWindowPosFlags.NoMove | SetWindowPosFlags.NoSize);
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

    /// <summary>
    /// SetWindowPos标志位
    /// 参数说明：https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-setwindowpos#parameters
    /// </summary>
    [Flags]
    public enum SetWindowPosFlags : uint
    {
        /// <summary>
        /// 如果调用线程和拥有窗口的线程附加到不同的输入队列，系统会将请求发布到拥有该窗口的线程。 这可以防止调用线程阻止其执行，而其他线程处理请求。
        /// </summary>
        AsyncWindowPos = 0x4000,

        /// <summary>
        /// 阻止生成 wm_syncpaint 消息。
        /// </summary>
        Defererase = 0x2000,

        /// <summary>
        /// 绘制在窗口的类说明中定义的框架 () 窗口周围。
        /// </summary>
        DrawFrame = 0x0020,

        /// <summary>
        /// 使用 setwindowlong 函数应用设置的新框架样式。 将 wm_nccalcsize 消息发送到窗口，即使窗口的大小未更改也是如此。 如果未指定此标志，则仅当窗口的大小发生更改时， 才会发送wm_nccalcsize 。
        /// </summary>
        FrameChanged = 0x0020,

        /// <summary>
        /// 隐藏窗口。
        /// </summary>
        HideWindow = 0x0080,

        /// <summary>
        /// 不激活窗口。 如果未设置此标志，则会激活窗口，并根据 hwndinsertafter 参数) 的设置 (将窗口移到最顶部或最顶层组的顶部。
        /// </summary>
        NoActivate = 0x0010,

        /// <summary>
        /// 丢弃工作区的整个内容。 如果未指定此标志，则会在调整或重新定位窗口后保存并复制回工作区的有效内容。
        /// </summary>
        NoCopyBits = 0x0100,

        /// <summary>
        /// 保留当前位置 (忽略 x 和 y 参数) 。
        /// </summary>
        NoMove = 0x0002,

        /// <summary>
        /// 不更改 z 顺序中的所有者窗口位置。
        /// </summary>
        NoOwnerZOrder = 0x0200,

        /// <summary>
        /// 不重绘更改。 如果设置了此标志，则不执行任何形式的重绘。 这适用于工作区、非工作区 (，包括标题栏和滚动条) ，以及由于移动窗口而发现的父窗口的任何部分。 设置此标志后，应用程序必须显式失效或重新绘制需要重绘的窗口和父窗口的任何部分。
        /// </summary>
        NoRedraw = 0x0008,

        /// <summary>
        /// 与 noownerzorder 标志相同。
        /// </summary>
        NoReposition = 0x0200,

        /// <summary>
        /// 阻止窗口接收 wm_windowposchanging 消息。
        /// </summary>
        NoSendChanging = 0x0400,

        /// <summary>
        /// 保留当前大小 (忽略 cx 和 cy 参数) 。
        /// </summary>
        NoSize = 0x0001,

        /// <summary>
        /// 保留当前 z 顺序 (忽略 hwndinsertafter 参数) 。
        /// </summary>
        NoZOrder = 0x0004,

        /// <summary>
        /// 显示“接收端口跟踪选项” 窗口。
        /// </summary>
        ShowWindow = 0x0040,


    }
}
