using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public static class ControlExtensions
    {
        /// <summary>
        /// 在UI线程中修改界面
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void ModifyInUI(this Control control, Action action)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control?.Invoke(action);
                }
                else
                {
                    action();
                }
            }
            catch (InvalidOperationException) { }
            catch (NullReferenceException) { }
        }

        /// <summary>
        /// 获取指定控件下的所有子控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="isRecursive"></param>
        /// <returns></returns>
        public static List<Control> GetChildControls(this Control control, bool isRecursive = false)
        {
            List<Control> list = new List<Control>();
            foreach (Control cc in control.Controls)
            {
                list.Add(cc);
                if (isRecursive && cc.Controls.Count > 0)
                {
                    list.AddRange(GetChildControls(cc, isRecursive));
                }
            }
            return list;
        }

        /// <summary>
        /// 遍历子控件，传入委托代码操作每一个控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="handler"></param>
        /// <param name="isRecursive"></param>
        public static void TraverseChildControls(this Control control, Action<Control> handler, bool isRecursive = false)
        {
            List<Control> controls = GetChildControls(control, isRecursive);
            if (handler != null)
            {
                foreach (Control c in controls)
                {
                    handler.Invoke(c);
                }
            }
        }

        /// <summary>
        /// 当前正在闪烁的控件列表（控件背景色闪烁）
        /// </summary>
        private static List<Control> CurrentBlinkingControlList = new List<Control>();
        /// <summary>
        /// 控件背景色闪烁
        /// </summary>
        /// <param name="control"></param>
        /// <param name="color"></param>
        /// <param name="frequency"></param>
        /// <param name="completedCallback"></param>
        public static void Blink(this Control control, Color color, ushort frequency = 2, Action completedCallback = null)
        {
            // 如果当前控件正在闪烁 则无效
            if (CurrentBlinkingControlList.Contains(control)) return;

            // 将控件加入闪烁列表
            CurrentBlinkingControlList.Add(control);

            // 获取所有子控件列表
            List<Control> controls = GetChildControls(control, true);

            // 去除背景色为透明的子控件
            controls.RemoveAll(item => { return item.BackColor == Color.Transparent; });

            // 将当前容器控件加入列表
            controls.Add(control);

            // 记录原始颜色
            Dictionary<Control, Color> colorTable = new Dictionary<Control, Color>();
            foreach (Control c in controls)
            {
                colorTable.Add(c, c.BackColor);
            }

            // 闪烁任务
            Task task = new Task(() =>
            {
                try
                {
                    for (int i = 0; i < frequency * 2; ++i)
                    {

                        // 挂起UI布局
                        control.SuspendLayout();
                        // 颜色反转方向
                        bool cr = i % 2 == 1;

                        try
                        {
                            // 颜色反转
                            foreach (Control c in controls)
                            {
                                c.BackColor = cr ? colorTable[c] : color;
                            }
                        }
                        catch (Exception) { }

                        // 更新UI布局
                        control.PerformLayout();
                        // 闪烁延时
                        Thread.Sleep(100);
                    }

                }
                catch (Exception) { }

                // 闪烁完成后，将控件移除闪烁列表
                if (CurrentBlinkingControlList.Contains(control)) CurrentBlinkingControlList.Remove(control);
            });
            // 如果有回调事件则设置完成回调
            if (completedCallback != null) task.GetAwaiter().OnCompleted(completedCallback);
            // 启动任务
            task.Start();
        }

        /// <summary>
        /// 向子控件共享单击事件委托
        /// </summary>
        /// <param name="control"></param>
        /// <param name="types"></param>
        public static void ShareClickEvent(this Control control, Type[] types = null)
        {
            // 方法一：为子控件添加回调事件，通过反射调用父容器的回调方法。此方法不易拓展，已弃用。
            TraverseChildControls(control, (childControl) =>
            {
                // 如果指定了共享的控件类型，则依照类型数组过滤；若未指定类型，则所有控件都通过；
                // 如果子控件未绑定 OnClick 事件，则共享Click事件
                if ((types == null || types.Contains(childControl.GetType())) && FindDelegateFromControl(childControl, "Click") == null)
                {
                    childControl.Click += (s, e) => { control.CallControlEvent("Click", e); };
                }
            }, true);

            // 方法二：获取委托，将委托加入子控件的委托列表中
            // ShareEventDelegate(control, "Click");
            //#if NETFRAMEWORK

            //#else
            //            // .NETCore & .NET5中无法通过Type.GetField获取到Event字段，所以无法使用FindDelegateFromControl方法.
            //            // 此方法在.NETCore & .NET5环境下无效
            //#endif
        }

        /// <summary>
        /// 向子控件共享指定事件名的事件委托
        /// </summary>
        /// <param name="control">容器控件</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="types">只允许类型数组内包含的类型共享事件委托</param>
        public static void ShareEventDelegate(this Control control, string eventName, Type[] types = null)
        {
            // 获取当前容器中指定事件委托（通过事件名称）
            Delegate handler = FindDelegateFromControl(control, eventName);
            // 如果未找到事件委托（不存在或未绑定委托）则退出
            if (handler == null) return;
            // 递归遍历出所有的子控件，为子控件配置委托
            TraverseChildControls(control, (childControl) =>
            {
                // 如果指定了共享的控件类型，则依照类型数组过滤；若未指定类型，则所有控件都通过；
                // 如果子控件未绑定 eventName 事件，则共享 eventName 事件
                if ((types == null || types.Contains(childControl.GetType())) && FindDelegateFromControl(childControl, eventName) == null)
                {
                    AddDelegateToControlEventHandlerList(childControl, eventName, handler);
                }
            }, true);
            //#if NETFRAMEWORK

            //#else
            //            // .NETCore & .NET5中无法通过Type.GetField获取到Event字段，所以无法使用FindDelegateFromControl方法.
            //            // 此方法在.NETCore & .NET5环境下无效
            //#endif
        }


        /// <summary>
        /// 反射调用事件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static bool CallControlEvent(this Control control, string eventName, EventArgs e = null)
        {
            try
            {
                // 获取对象的类型      
                Type t = control.GetType();
                // 从类型中查找出符合条件的方法  
                MethodInfo m = t.GetMethod("On" + eventName, BindingFlags.NonPublic | BindingFlags.Instance);
                // 如果没有对应方法则返回false
                if (m == null) return false;
                //获得参数信息
                ParameterInfo[] para = m.GetParameters();
                //参数对象      
                object[] p = new object[1];
                if (e == null)
                    p[0] = Type.GetType(para[0].ParameterType.FullName).GetProperty("Empty");
                else
                    p[0] = e;
                //调用  
                m.Invoke(control, parameters: p);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 向控件事件列表中添加委托
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="handler"></param>
        public static void AddDelegateToControlEventHandlerList(this Control control, string eventName, Delegate handler)
        {
            EventInfo ei = control.GetType().GetEvent(eventName);
            ei.AddEventHandler(control, handler);
        }

        /// <summary>
        /// 从控件中获取指定名称的事件委托
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public static Delegate FindDelegateFromControl(this Control control, string eventName)
        {
            EventHandlerList events = control.GetType().GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(control) as EventHandlerList;
            FieldInfo key = control.GetType().GetField("Event" + eventName, BindingFlags.Static | BindingFlags.NonPublic);
            if (key == null) return null;
            return events[key.GetValue(control)];
            //#if NETFRAMEWORK

            //#else
            //            return null;
            //#endif
        }

        /// <summary>
        /// 从控件的多级父容器中寻找指定类型的容器控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        public static T FindParentContainerByType<T>(this Control control) where T : Control
        {
            if (control.GetType() == typeof(T))
            {
                return (T)control;
            }
            else
            {
                if (control.Parent == control.FindForm() && typeof(T) != control.FindForm().GetType())
                {
                    throw new Exception($"The parent container of the specified type was not found.[{typeof(T).Name}]");
                }
                return FindParentContainerByType<T>(control.Parent);
            }
        }

        /// <summary>
        /// 获取控件中文本的尺寸
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static SizeF GetTextSize(this Control control)
        {
            return control.CreateGraphics().MeasureString(control.Text, control.Font);
        }
    }
}
