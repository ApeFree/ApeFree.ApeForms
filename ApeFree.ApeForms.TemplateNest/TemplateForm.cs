using ApeFree.ApeForms.Core.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo
{
    /// <summary>
    /// 模板窗体
    /// </summary>
    public partial class TemplateForm : Form
    {
        private List<NavBarGroup> sideNavData;
        private List<TopBarItem> topBarData;

        /// <summary>
        /// 侧边导航栏数据
        /// </summary>
        public List<NavBarGroup> SideNavData { get => sideNavData; set { sideNavData = value; LoadSideBar(clbSideBar); } }

        /// <summary>
        /// 顶部导航栏数据
        /// </summary>
        public List<TopBarItem> TopBarData { get => topBarData; set { topBarData = value; LoadTopBar(clbTopBar); } }

        /// <summary>
        /// Logo图像
        /// </summary>
        public Image LogoImage { get => picLogo.Image; set => picLogo.Image = value; }

        /// <summary>
        /// “关闭所有选项卡”选项的文本
        /// </summary>
        public string CloseAllPagesOptionText { get => slideTabControl.CloseAllPagesOptionText; set => slideTabControl.CloseAllPagesOptionText = value; }

        /// <summary>
        /// “关闭选项卡”选项的文本
        /// </summary>
        public string ClosePageOptionText { get => slideTabControl.ClosePageOptionText; set => slideTabControl.ClosePageOptionText = value; }

        public TemplateForm()
        {
            InitializeComponent();

            Text = $"{ProductName} - V{ProductVersion}";

            // 修改关闭选项名称
            CloseAllPagesOptionText = "全部关闭";
            ClosePageOptionText = "关闭";

            LoadBottomBar(clbBottomBar);
        }

        /// <summary>
        /// 加载导航栏
        /// </summary>
        protected virtual void LoadSideBar(ControlListBox sideBar)
        {
            // 清空导航栏
            foreach (Control item in sideBar.Items)
            {
                item.Dispose();
            }

            // 遍历导航栏一级菜单数据
            SideNavData.Reverse();
            foreach (NavBarGroup data in SideNavData)
            {
                // 创建下拉按钮组
                SimpleButtonShutter shutter = new SimpleButtonShutter();
                shutter.ButtonGroupId = byte.MaxValue;
                shutter.MainControl.Text = data.Name;
                shutter.MainControl.BackColor = Color.FromArgb(30, 20, 50);
                shutter.MainControl.ForeColor = Color.FromArgb(245, 245, 245);

                // 遍历导航栏二级菜单数据
                data.Reverse();
                foreach (var item in data)
                {
                    // 添加二级菜单按钮
                    var btn = shutter.AddChildButton(item.Name, (s, args) =>
                    {
                        // 设置单击事件
                        slideTabControl.AddPage(item.Name, item.Control, (data.Icon ?? this.Icon).ToBitmap());
                    });

                    // 设置单个按钮的前景色和背景色
                    btn.BackColor = Color.FromArgb(70, 55, 100);
                    btn.ForeColor = Color.FromArgb(245, 245, 245);

                    // 设置单个按钮选中时的边线颜色、边线宽度和背景色
                    btn.SidelineColor = Color.PaleVioletRed;
                    btn.SidelineWidth = 8;
                    btn.SelectedBackColor = btn.BackColor.Luminance(1.2f); // 选中状态下按钮增亮20%   
                }
                sideBar.AddItem(shutter);
            }
        }

        protected virtual void LoadTopBar(ControlListBox topBar)
        {
            // 清空导航栏
            foreach (Control item in topBar.Items)
            {
                item.Dispose();
            }

            foreach (var item in TopBarData)
            {
                var btn = new SimpleButton();
                btn.Text = item.Name;
                btn.AutoSize = true;
                btn.MinimumSize = new Size(80, 0);
                btn.BackColor = Color.FromArgb(40, 25, 65);
                btn.Click += item.Click;
                btn.Parent = topBar;
            }
        }

        protected virtual void LoadBottomBar(ControlListBox bottomBar)
        {
            // 清空控件
            foreach (Control item in bottomBar.Items)
            {
                item.Dispose();
            }
        }
    }

    /// <summary>
    /// 导航栏数据
    /// </summary>
    [Serializable]
    public class NavBarGroup : List<NavItem>
    {
        public string Name { get; set; }
        public Icon Icon { get; set; }

        private NavBarGroup() { }

        public NavBarGroup(string name, Icon icon = null)
        {
            Name = name;
            Icon = icon;
        }
    }

    /// <summary>
    /// 导航栏
    /// </summary>
    [Serializable]
    public class NavItem
    {
        private Control control;

        public string Name { get; set; }
        public Type ControlType { get; }
        public Control Control
        {
            get
            {
                if (control != null && control.IsDisposed)
                {
                    control = null;
                }

                if (control == null)
                {
                    control = (Control)Activator.CreateInstance(ControlType);
                }
                return control;
            }
        }

        private NavItem()
        {

        }

        public NavItem(string name, Control control)
        {
            Name = name;
            this.control = control;
            ControlType = control.GetType();
        }

        public NavItem(string name, Type controlType)
        {
            if (!controlType.IsSubclassOf(typeof(Control)))
            {
                throw new ArgumentException($"{controlType} 不是 Control 类及其子类。");
            }

            Name = name;
            ControlType = controlType;
        }
    }

    [Serializable]
    public class TopBarItem
    {
        public TopBarItem(string name, EventHandler click)
        {
            Name = name;
            Click = click;
        }

        private TopBarItem() { }

        public string Name { get; set; }
        public EventHandler Click { get; set; }
    }
}
