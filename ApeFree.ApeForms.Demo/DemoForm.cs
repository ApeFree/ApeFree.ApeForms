using ApeFree.ApeForms.Controls.Buttons;
using ApeFree.ApeForms.Controls.Cards;
using ApeFree.ApeForms.Controls.Containers;
using ApeFree.ApeForms.Controls.DatePickers;
using ApeFree.ApeForms.Controls.Grids;
using ApeFree.ApeForms.Controls.Labels;
using ApeFree.ApeForms.Controls.ListBoxes;
using ApeFree.ApeForms.Controls.TabBoxes;
using ApeFree.ApeForms.Controls.TextBoxes;
using ApeFree.ApeForms.Controls.TitleBars;
using ApeFree.ApeForms.Controls.Views;
using ApeFree.ApeForms.Forms;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notifications;
using ApeFree.ApeForms.Forms.Templates;
using ApeFree.ApeForms.Extensions;
using ApeFree.ApeForms.Utils;
using ApeFree.ApeForms.Demo.DemoPanel;
using ApeFree.ApeForms.Demo.Properties;
using ApeFree.ApeForms.Forms.Templates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo
{
    public partial class DemoForm : TemplateForm
    {
        public DemoForm()
        {
            InitializeComponent();

            // 程序标题
            Text = $"{ProductName} - V{ProductVersion}";

            // 修改Logo
            LogoImage = Resources.ApeForms_Logo;

            // 设置侧边栏的菜单数据
            SideBarData = new List<NavBarGroup>
            {
                new NavBarGroup("Button",Resources.Icon_00)
                {
                    new NavItem ("Button",typeof(ButtonDemoPanel)){ Icon = Resources.Icon_01, ShowCloseButton = false },
                },
                new NavBarGroup("DatePicker",Resources.Icon_02)
                {
                    new NavItem("DatePicker", typeof(DatePickerDemoPanel)){ Icon = Resources.Icon_05},
                },
                new NavBarGroup("Card",Resources.Icon_03)
                {
                    new NavItem("Magnet", typeof(MagnetDemoPanel)){ Icon = Resources.Icon_06},
                    new NavItem("SimpleCard", typeof(SimpleCardDemoPanel)){ Icon = Resources.Icon_06},
                },
                new NavBarGroup("Grid",Resources.Icon_09)
                {
                    new NavItem("GridView", typeof(GridViewDemoPanel)){ Icon = Resources.Icon_09},
                },
                new NavBarGroup("Container",Resources.Icon_04)
                {
                    new NavItem("ControlListBox", typeof(ControlListBoxDemoPanel)){ Icon = Resources.Icon_07},
                    new NavItem("SlideBox", typeof(SlideBoxDemoPanel)){ Icon = Resources.Icon_07},
                    new NavItem("Shutter(preview)", typeof(ShutterDemoPanel)){ Icon = Resources.Icon_07},
                    new NavItem("StageLayout", typeof(StageLayoutDemoPanel)){ Icon = Resources.Icon_07},
                },
                new NavBarGroup("Notifications",Resources.Icon_08)
                {
                    new NavItem("Toast", typeof(ToastDemoPanel)),
                    new NavItem("Notification", typeof(NotificationBoxDemoPanel)),
                },
                new NavBarGroup("Dialogs",Resources.Icon_09)
                {
                    new NavItem("ApeDialogs", typeof(DialogDemoPanel)),
                },

                new NavBarGroup("Extensions")
                {
                    new NavItem("Form", typeof(FormExtensionDemoPanel)),
                    new NavItem("GradualChange", typeof(GradualChangeExtensionDemoPanel)),
                },
                new NavBarGroup("GdiPalette")
                {
                    new NavItem("Clock", typeof(GdiPaletteClockDemoPanel)),
                    new NavItem("RadarChart", typeof(GdiPaletteRadarChartDemoPanel)),
                },
            };

            // 设置顶部栏的选项数据
            TopBarData = new List<TopBarItem> {
                new TopBarItem("获取源码",(s,e)=>Process.Start("https://github.com/ApeFree/ApeFree.ApeForms.Demo")),
                new TopBarItem("更多文档",(s,e)=>Process.Start("https://blog.csdn.net/lgj123xj/category_11811822.html")),
                new TopBarItem("加入组织",(s,e)=>{
                    Process.Start("https://qm.qq.com/cgi-bin/qm/qr?k=pa-gDVKW6EHScuoeIxLgncshyQJgM3jP&jump_from=webapi&authKey=7yeoSaM8AVK+Svc/FBskmAu6n478PQn1BgNdtTZArMjc9YbSBRfSC+pufaKUKWJM");
                    Clipboard.SetDataObject("929371169");
                    this.ShowToast("QQ群：929371169 (已复制)", ToastMode.Preemption, 5000);
                }),
            };

            // 设置分页栏控件标题项的右键快捷菜单
            var tsmiCloseOne = PageItemContextMenu.Items.Add("Close", null, (s, e) => SlideTabBox.RemovePage(SlideTabBox.ActiveContextMenuTitleItem.Text));    // 添加关闭单个页面选项
            var tsmiCloseAll = PageItemContextMenu.Items.Add("Close all pages", null, (s, e) =>                                                                // 添加关闭全部页面选项（除不显示关闭按钮的选项外）
            {
                foreach (var item in SlideTabBox.PageTitleItems)
                {
                    if (item.ShowCloseButton) // 如果页面的标题项允许显示关闭按钮，则代表该页面允许关闭
                    {
                        SlideTabBox.RemovePage(item.Text);
                    }
                }
            });
            PageItemContextMenu.Opening += (s, e) => tsmiCloseOne.Enabled = SlideTabBox.ActiveContextMenuTitleItem.ShowCloseButton;

        }

        protected override void LoadBottomBar(ControlListBox bottomBar)
        {
            base.LoadBottomBar(bottomBar);

            var linklab = new LinkLabel() { Text = "博客主页", AutoSize = false, Size = new Size(0, 15), TextAlign = ContentAlignment.MiddleCenter };
            var labContactUs = new Label() { Text = "QQ交流群: 929371169", AutoSize = false, Size = new Size(0, 15), TextAlign = ContentAlignment.MiddleCenter };
            var labCopyright = new Label() { Text = "Copyright © 2022-2024 ApeFree", AutoSize = false, Size = new Size(0, 15), TextAlign = ContentAlignment.MiddleCenter };

            linklab.Click += (s, e) => Process.Start("https://blog.csdn.net/lgj123xj/category_11811822.html");

            bottomBar.AddItem(linklab);
            bottomBar.AddItem(labContactUs);
            bottomBar.AddItem(labCopyright);
        }
    }
}
