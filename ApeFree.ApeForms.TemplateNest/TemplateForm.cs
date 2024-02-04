using ApeFree.ApeForms.Core.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.TemplateNest
{
    /// <summary>
    /// 模板窗体
    /// </summary>
    public partial class TemplateForm : Form
    {
        private List<NavBarGroup> sideBarData;
        private List<TopBarItem> topBarData;
        private Color mainMenuBackColor = Color.FromArgb(30, 20, 50);
        private Color mainMenuForeColor = Color.FromArgb(245, 245, 245);
        private Color subMenuBackColor = Color.FromArgb(70, 55, 100);
        private Color subMenuForeColor = Color.FromArgb(245, 245, 245);
        private Color topBarItemBackColor = Color.FromArgb(40, 25, 65);
        private Color topBarItemForeColor = Color.White;
        private Color subMenuSidelineColor = Color.PaleVioletRed;

        /// <summary>
        /// 侧边导航栏数据
        /// </summary>
        [Browsable(false)]
        public List<NavBarGroup> SideBarData { get => sideBarData; set { sideBarData = value; LoadSideBar(clbSideBar); } }

        /// <summary>
        /// 顶部导航栏数据
        /// </summary>
        [Browsable(false)]
        public List<TopBarItem> TopBarData { get => topBarData; set { topBarData = value; LoadTopBar(clbTopBar); } }

        /// <summary>
        /// 滑动选项卡
        /// </summary>
        protected SlideTabControl SlideTabBox => slideTabControl;

        /// <summary>
        /// Logo图像
        /// </summary>
        [Description("Logo图像")]
        public Image LogoImage { get => picLogo.Image; set => picLogo.Image = value; }

        /// <summary>
        /// “关闭所有选项卡”选项的文本
        /// </summary>
        [Description("“关闭所有选项卡”选项的文本")]
        public string CloseAllPagesOptionText { get => slideTabControl.CloseAllPagesOptionText; set => slideTabControl.CloseAllPagesOptionText = value; }

        /// <summary>
        /// “关闭选项卡”选项的文本
        /// </summary>
        [Description("“关闭选项卡”选项的文本")]
        public string ClosePageOptionText { get => slideTabControl.ClosePageOptionText; set => slideTabControl.ClosePageOptionText = value; }

        [Description("顶部导航栏背景色")]
        public Color TopBarBackColor { get => panelHead.BackColor; set => panelHead.BackColor = value; }

        [Description("侧边导航栏背景色")]
        public Color SideBarBackColor { get => clbSideBar.BackColor; set => clbSideBar.BackColor = value; }

        [Description("底部导航栏背景色")]
        public Color BottomBarBackColor { get => clbBottomBar.BackColor; set => clbBottomBar.BackColor = value; }

        [Description("一级菜单背景色")]
        public Color MainMenuBackColor { get => mainMenuBackColor; set { mainMenuBackColor = value; RefreahNavColor(); } }

        [Description("一级菜单前景色")]
        public Color MainMenuForeColor { get => mainMenuForeColor; set { mainMenuForeColor = value; RefreahNavColor(); } }

        [Description("二级菜单背景色")]
        public Color SubMenuBackColor { get => subMenuBackColor; set { subMenuBackColor = value; RefreahNavColor(); } }

        [Description("二级菜前景色")]
        public Color SubMenuForeColor { get => subMenuForeColor; set { subMenuForeColor = value; RefreahNavColor(); } }

        [Description("二级菜选中时的边线颜色")]
        public Color SubMenuSidelineColor { get => subMenuSidelineColor; set { subMenuSidelineColor = value; RefreahNavColor(); } }

        [Description("顶部导航栏选项背景色")]
        public Color TopBarItemBackColor { get => topBarItemBackColor; set { topBarItemBackColor = value; RefreahNavColor(); } }

        [Description("顶部导航栏选项前景色")]
        public Color TopBarItemForeColor { get => topBarItemForeColor; set { topBarItemForeColor = value; RefreahNavColor(); } }


        public TemplateForm()
        {
            InitializeComponent();

            LoadBottomBar(clbBottomBar);
        }

        /// <summary>
        /// 刷新导航栏选项的颜色
        /// </summary>
        private void RefreahNavColor()
        {
            // 左侧导航栏
            foreach (SimpleButtonShutter item in clbSideBar.Items)
            {
                item.MainControl.BackColor = MainMenuBackColor;
                item.MainControl.ForeColor = MainMenuForeColor;

                foreach (TabButton btn in item.HiddenControl.Controls)
                {
                    btn.BackColor = SubMenuBackColor;
                    btn.ForeColor = SubMenuForeColor;
                    btn.SidelineColor = SubMenuSidelineColor;
                }
            }

            // 顶部导航栏
            foreach (Control btn in clbTopBar.Items)
            {
                btn.BackColor = TopBarItemBackColor;
                btn.ForeColor = TopBarItemForeColor;
            }

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
            SideBarData.Reverse();
            foreach (NavBarGroup data in SideBarData)
            {
                var shutter = AddChildShutter(data);
                sideBar.AddItem(shutter);
            }
        }

        private SimpleButtonShutter AddChildShutter(NavBarGroup data)
        {
            // 创建下拉按钮组
            SimpleButtonShutter shutter = new SimpleButtonShutter();
            shutter.ButtonGroupId = byte.MaxValue;
            shutter.MainControl.Text = data.Name;
            shutter.MainControl.BackColor = MainMenuBackColor;
            shutter.MainControl.ForeColor = MainMenuForeColor;
            shutter.MainControl.Icon = data.Icon;
            shutter.MainControl.IconScaling = 0.5f;


            // 遍历导航栏二级菜单数据
            data.Reverse();
            foreach (var barItem in data)
            {
                if (barItem is NavItem item)
                {
                    // 添加二级菜单按钮
                    var btn = shutter.AddChildButton(item.Name, (s, args) =>
                    {
                        // 如果页面是窗体，则改为控件
                        if (item.Control is Form form)
                        {
                            form.TopLevel = false;
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.Show();
                        }

                        // 设置单击事件
                        slideTabControl.AddPage(item.Name, item.Control, item.Icon ?? Icon.ToBitmap());
                    });

                    // 设置单个按钮的前景色和背景色
                    btn.BackColor = SubMenuBackColor;
                    btn.ForeColor = SubMenuForeColor;

                    // 设置单个按钮选中时的边线颜色、边线宽度和背景色
                    btn.SidelineColor = SubMenuSidelineColor;
                    btn.SidelineWidth = 8;
                    btn.SelectedBackColor = btn.BackColor.Luminance(1.2f); // 选中状态下按钮增亮20%   

                    // 设置按钮的图标
                    btn.Icon = item.Icon;
                    btn.IconScaling = 0.5f;
                }
                else if (barItem is NavBarGroup barGroup)
                {
                    var group = AddChildShutter(barGroup);
                    shutter.AddChildShutter(group);
                    group.ButtonGroupId = byte.MaxValue;
                    group.MainControl.BackColor = SubMenuBackColor.Luminance(0.9f);
                    group.MainControl.ForeColor = SubMenuForeColor;
                    group.MainControl.Icon = data.Icon;
                    group.MainControl.IconScaling = 0.5f;
                }
            }

            return shutter;
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
                btn.BackColor = topBarItemBackColor;
                btn.ForeColor = topBarItemForeColor;
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

    public interface INavBarItem
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        Bitmap Icon { get; set; }
    }

    /// <summary>
    /// 导航栏数据
    /// </summary>
    [Serializable]
    public class NavBarGroup : List<INavBarItem>, INavBarItem
    {
        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public Bitmap Icon { get; set; }

        private NavBarGroup() { }

        public NavBarGroup(string name, Bitmap icon = null)
        {
            Name = name;
            Icon = icon;
        }
    }

    /// <summary>
    /// 导航栏
    /// </summary>
    [Serializable]
    public class NavItem : INavBarItem
    {
        private Control control;

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public Bitmap Icon { get; set; }

        /// <summary>
        /// 关联控件类型
        /// </summary>
        public Type ControlType { get; }

        /// <summary>
        /// 关联控件
        /// </summary>
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

        private NavItem() { }

        public NavItem(string name, Control control, Bitmap icon = null)
        {
            Name = name;
            this.control = control;
            ControlType = control.GetType();
            Icon = icon;
        }

        public NavItem(string name, Type controlType, Bitmap icon = null)
        {
            if (!controlType.IsSubclassOf(typeof(Control)))
            {
                throw new ArgumentException($"{controlType} 不是 Control 类及其子类。");
            }

            Name = name;
            ControlType = controlType;
            Icon = icon;
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
