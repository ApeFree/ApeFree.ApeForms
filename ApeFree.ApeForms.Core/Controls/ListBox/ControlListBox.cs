using ApeFree.CodePlus.Algorithm.DataStructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    [ToolboxItem(true)]
    public partial class ControlListBox : UserControl, IListBox<Control>
    {
        /// <summary>
        /// 列表项
        /// </summary>
        public EventableList<Control> Items { get; } = new EventableList<Control>();

        [Browsable(true)]
        [Description("指定列表中控件的排列方向")]
        public FlowDirection Direction { get => direction; set { direction = value; ListBoxOrientationChanged(); } }
        private FlowDirection direction = FlowDirection.TopDown;

        public override bool AutoScroll
        {
            get => base.AutoScroll; 
            set
            {
                if(base.AutoScroll != value)
                {
                    base.AutoScroll = value;
                    RefreshChildControlsSize();
                }
            }
        }

        public ControlListBox()
        {
            InitializeComponent();

            Items.ItemAdded += ListItemAdded;
            Items.ItemInserted += ListItemInserted;
            Items.ItemRemoved += ListItemRemoved;
            Items.ItemsCleared += ListItemsCleared;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            AddItem((Control)e.Control);
            RefreshChildControlsSize();
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            try
            {
                // 如果控件通过其他方式移除(非Items中移除)，需要在Items中将其移除
                Items.Remove((Control)e.Control);
            }
            catch (Exception) { }
            RefreshChildControlsSize();
        }

        private void ListItemsCleared(object sender, EventArgs e) => Controls.Clear();
        private void ListItemRemoved(object sender, ListItemsChangedEventArgs<Control> e) => Controls.Remove(e.Item);
        private void ListItemInserted(object sender, ListItemsChangedEventArgs<Control> e) => Controls.Add(e.Item);// 不能做插入操作，当添加处理
        private void ListItemAdded(object sender, ListItemsChangedEventArgs<Control> e) =>
            Controls.Add(e.Item);

        private void ListBoxOrientationChanged()
        {
            SuspendLayout();
            // 修改布局的Dock方向
            foreach (Control item in Controls)
            {
                item.Dock = GetDockStyle();
            }
            ResumeLayout();

            RefreshChildControlsSize();
        }

        /// <summary>
        /// 刷新子控件的尺寸
        /// </summary>
        public void RefreshChildControlsSize()
        {
            SuspendLayout();

            // 判断是否启用滚动条
            // 如果未开启滚动条则将控件等宽平铺
            if (!AutoScroll && Controls.Count > 0)
            {
                foreach (Control item in Controls)
                {
                    item.SuspendLayout();
                }

                // 如果是水平方向需要平均宽度
                // 如果是垂直方向需要平均高度
                if (Direction == FlowDirection.LeftToRight || Direction == FlowDirection.RightToLeft)
                {
                    var width = Width / Controls.Count;
                    foreach (Control item in Controls)
                    {
                        item.Width = width;
                    }
                }
                else
                {
                    var height = Height / Controls.Count;
                    foreach (Control item in Controls)
                    {
                        item.Height = height;
                    }
                }

                foreach (Control item in Controls)
                {
                    item.ResumeLayout();
                }
            }

            ResumeLayout();
        }


        public void AddItem(Control item)
        {
            item.Dock = GetDockStyle();
            if (!Items.Contains(item)) Items.Add(item);
        }

        public void AddItems(IEnumerable<Control> items)
        {
            foreach (var item in items) item.Dock = GetDockStyle();
            Items.AddRange(items);
        }

        private DockStyle GetDockStyle()
        {
            switch (Direction)
            {
                case FlowDirection.LeftToRight:
                    return DockStyle.Left;
                case FlowDirection.TopDown:
                    return DockStyle.Top;
                case FlowDirection.RightToLeft:
                    return DockStyle.Right;
                case FlowDirection.BottomUp:
                    return DockStyle.Bottom;
            }
            return DockStyle.Top;
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            RefreshChildControlsSize();
        }
    }
}
