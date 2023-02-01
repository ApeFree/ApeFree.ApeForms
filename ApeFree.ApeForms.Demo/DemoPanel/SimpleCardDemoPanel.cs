using ApeFree.ApeForms.Core.Controls;
using ApeFree.ApeForms.Demo.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class SimpleCardDemoPanel : UserControl
    {
        public Dictionary<string, Bitmap> items = new Dictionary<string, Bitmap>
        {
            {"Item_1",Resources.Magent_01 },
            {"Item_2",Resources.Magent_02 },
            {"Item_3",Resources.Magent_03 },
            {"Item_4",Resources.Magent_04 },
            {"Item_5",Resources.Magent_05 },
            {"Item_6",Resources.Magent_06 },
            {"Item_7",Resources.Magent_07 },
            {"Item_8",Resources.Magent_08 },
            {"Item_9",Resources.Magent_09 },
            {"Item_10",Resources.Magent_10 },
            {"Item_11",Resources.ImageButton_1 },
            {"Item_12",Resources.ImageButton_11 },
            {"Item_13",Resources.ImageButton_12 },
            {"Item_14",Resources.ImageButton_13 },
        };

        public SimpleCardDemoPanel()
        {
            InitializeComponent();

            foreach (var kvp in items)
            {
                var item = new SimpleCard();
                item.BackColor = Color.White;
                item.Text = kvp.Key;
                item.Image = kvp.Value.ToPureColor(Color.Black, false);
                item.BorderSize = 5;                        // 设置边框的粗细
                item.BorderColor = Color.LightBlue;         // 设置边框的颜色
                item.ContextMenuStrip = contextMenuStrip1;

                item.MouseClick += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        item.ShowToast($"当前单击项：{kvp.Key}", ToastMode.Reuse);
                    }
                    else
                    {
                        // contextMenuStrip1.Show(Control.MousePosition);
                    }
                };

                flp.Controls.Add(item);
            }
        }
    }
}
