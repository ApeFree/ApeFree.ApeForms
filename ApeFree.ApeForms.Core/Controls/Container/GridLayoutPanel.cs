using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Core.Controls.Container
{
    /// <summary>
    /// 网格布局面板
    /// </summary>
    public class GridLayoutPanel : Panel
    {
        private int displayRow = 1;
        private int displayColumn = 1;

        public GridLayoutPanel()
        {
            AutoSize = false;
            AutoScroll = true;
        }

        public int DisplayRow
        {
            get { return displayRow; }
            set
            {
                if (value > 0 && displayRow != value)
                {
                    displayRow = value;
                }
            }
        }

        public int DisplayColumn
        {
            get { return displayColumn; }
            set
            {
                if (value > 0 && displayColumn != value)
                {
                    displayColumn = value;
                }
            }
        }
        /// <summary>
        /// 重新排列内部控件
        /// </summary>
        public void Rearrange()
        {
            var displayItems = Controls.Cast<Control>().Where(c => c.Visible).ToArray();
            var displayCount = displayItems.Count();
            if (displayCount == 0)
            {
                return;
            }

            var itemHeight = ClientRectangle.Height / (float)DisplayRow;
            var itemWidth = ClientRectangle.Width / (float)DisplayColumn;

            // 滚动条置顶
            AutoScrollPosition = new Point(0, 0);

            SuspendLayout();

            // 重新计算每一个子控件的坐标
            for (int i = 0; i < displayCount; i++)
            {
                var row = i / DisplayColumn;
                var col = i % DisplayColumn;

                var item = displayItems[i];
                item.SuspendLayout();
                item.Size = new Size((int)itemWidth, (int)itemHeight);
                item.Location = new Point((int)(col * itemWidth), (int)(row * itemHeight));
                item.ResumeLayout(true);
            }

            ResumeLayout(true);

            var size = new Size((int)(displayItems.Select(i => i.Left).Max() + itemWidth), (int)(displayItems.Select(i => i.Top).Max() + itemHeight));
            AutoScrollMinSize = size;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Rearrange();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            e.Control.Dock = DockStyle.None;
            if (e.Control.Visible)
            {
                Rearrange();
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            Rearrange();
        }
    }
}
