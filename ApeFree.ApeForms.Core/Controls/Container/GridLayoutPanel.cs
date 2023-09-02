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

        /// <summary>
        /// 显示行数
        /// </summary>
        public int DisplayRow
        {
            get => displayRow;
            set
            {
                if (value > 0 && displayRow != value)
                {
                    displayRow = value;
                    Rearrange();
                }
            }
        }

        /// <summary>
        /// 显示列数
        /// </summary>
        public int DisplayColumn
        {
            get => displayColumn;
            set
            {
                if (value > 0 && displayColumn != value)
                {
                    displayColumn = value;
                    Rearrange();
                }
            }
        }
        /// <summary>
        /// 重新排列内部控件
        /// </summary>
        public void Rearrange()
        {
            var itemCount = Controls.Count;
            if (itemCount == 0)
            {
                return;
            }

            var itemHeight = this.ClientRectangle.Height / DisplayRow;
            var itemWidth = this.ClientRectangle.Width / DisplayColumn;

            AutoScrollPosition = new Point(0, 0);

            SuspendLayout();

            for (int i = 0; i < itemCount; i++)
            {
                var row = i / DisplayColumn;
                var col = i % DisplayColumn;

                var item = Controls[i];
                item.SuspendLayout();
                item.Size = new Size(itemWidth, itemHeight);
                item.Location = new Point(col * itemWidth, row * itemHeight);
                item.ResumeLayout(false);
            }

            ResumeLayout(false);
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
            Rearrange();
        }
    }
}
