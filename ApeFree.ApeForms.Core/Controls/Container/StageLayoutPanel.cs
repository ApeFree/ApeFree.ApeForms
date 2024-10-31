using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls.Container
{
    /// <summary>
    /// 舞台布局面板
    /// </summary>
    public partial class StageLayoutPanel : UserControl
    {
        /// <summary>
        /// 网格区面板
        /// </summary>
        public Panel GridPanel { get; }

        /// <summary>
        /// 主控件
        /// </summary>
        public Control MainControl { get; private set; }

        /// <summary>
        /// 网格区视图尺寸
        /// </summary>
        public Size GridItemSize { get; set; } = new Size(100, 100);

        /// <summary>
        /// 网格区视图之间的间距
        /// </summary>
        public ushort SeparationDistance { get; set; } = 20;

        /// <summary>
        /// 网格区控件列表的排序依据<br/>
        /// 例如：x=>x.Text; 表示将使用控件的Text属性进行排序
        /// </summary>
        public Func<Control, object> GridItemSortingHandler { get; set; }

        public StageLayoutPanel()
        {
            InitializeComponent();

            GridPanel = panelGrid;

            GridPanel.HorizontalScroll.Maximum = 0;
            GridPanel.AutoScroll = true;
            GridPanel.VerticalScroll.Visible = true;
            GridPanel.HorizontalScroll.Visible = false;
            GridPanel.PerformLayout();

            // 监听网格区控件增减事件
            ControlEventHandler controlEventHandler = (s, e) => RefreshGridControlLocation();
            GridPanel.ControlAdded += controlEventHandler;
            GridPanel.ControlRemoved += controlEventHandler;
            GridPanel.SizeChanged += (s, e) => RefreshGridControlLocation();
        }

        /// <summary>
        /// 向网格区添加控件
        /// </summary>
        /// <param name="control"></param>
        public void AddControl(Control control)
        {
            control.Size = GridItemSize;
            GridPanel.Controls.Add(control);
        }

        /// <summary>
        /// 选择主要控件
        /// </summary>
        /// <param name="control"></param>
        public void SelectMainControl(Control control)
        {
            if (control == MainControl)
            {
                return;
            }

            if (!GridPanel.Controls.Contains(control))
            {
                throw new InvalidOperationException("设置的主控件不是已加入网格中的控件。");
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                var ctrl = Controls[i];
                if (ctrl != GridPanel)
                {
                    ctrl.Dock = DockStyle.None;
                    AddControl(ctrl);
                }
            }

            control.Parent = this;
            control.BringToFront();

            var newSize = new Size(Width - GridPanel.Width, Height);
            var newLocation = new Point(GridPanel.Width, 0);

            ControlSizeChangeHandler(control, newSize);
            ControlLocationChangeHandler(control, newLocation);

            MainControl = control;
        }

        /// <summary>
        /// 刷新网格区控件位置
        /// </summary>
        private void RefreshGridControlLocation()
        {
            GridPanel.SuspendLayout();

            // 总列数
            var columnNum = (GridPanel.Width + SeparationDistance) / (GridItemSize.Width + SeparationDistance);

            // 所有控件
            List<Control> controls = GridPanel.Controls.ToList();

            // 排序依据
            if (GridItemSortingHandler != null)
            {
                controls = controls.OrderBy(GridItemSortingHandler).ToList();
            }

            // 修改每个控件的位置
            for (int i = 0; i < controls.Count; i++)
            {
                var view = controls[i];

                // 尺寸调整
                ControlSizeChangeHandler(view, GridItemSize);

                var col = i % columnNum;
                var row = i / columnNum;
                var newLocation = new Point(col * (GridItemSize.Width + SeparationDistance), row * (GridItemSize.Height + SeparationDistance) - Math.Abs(panelGrid.AutoScrollPosition.Y));

                // 位置调整
                ControlLocationChangeHandler(view, newLocation);
            }

            GridPanel.ResumeLayout();
        }

        /// <summary>
        /// 控件位置运动的实现
        /// </summary>
        /// <param name="control"></param>
        /// <param name="point"></param>
        protected virtual void ControlLocationChangeHandler(Control control, Point point)
        {
            control.Location = point;
        }

        /// <summary>
        /// 控件尺寸变化的实现
        /// </summary>
        /// <param name="control"></param>
        /// <param name="size"></param>
        protected virtual void ControlSizeChangeHandler(Control control, Size size)
        {
            control.Size = size;
        }

        /// <inheritdoc/>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (MainControl != null)
            {
                var newSize = new Size(Width - GridPanel.Width, Height);
                var newLocation = new Point(GridPanel.Width, 0);

                MainControl.Size = newSize;
                MainControl.Location = newLocation;
            }
        }
    }
}
