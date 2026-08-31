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
using static ApeFree.ApeForms.Controls.Grids.GridView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class GridViewDemoPanel : UserControl
    {
        public GridViewDemoPanel()
        {
            InitializeComponent();
        }

        private void GridViewDemoPanel_Load(object sender, EventArgs e)
        {
            // 设置列
            gridView.Columns = new GridView.ColumnSettings[] {
                new GridView.ColumnSettings(){ Text="ID",Width = 80},
                new GridView.ColumnSettings(){ Text="IMG1"},
                new GridView.ColumnSettings(){ Text="IMG2"},
                new GridView.ColumnSettings(){ Text="IMG3"},
                new GridView.ColumnSettings(){ Text="IMG4"},
                new GridView.ColumnSettings(){ Text="IMG5"},
                new GridView.ColumnSettings(){ Text="IMG6"},
                new GridView.ColumnSettings(){ Text="IMG7"},
                new GridView.ColumnSettings(){ Text="IMG8"},
                new GridView.ColumnSettings(){ Text="IMG9"},
            };

            // 随机数
            var random = new Random();

            // 所有图片
            var images = new Bitmap[] {
                Resources.Icon_00, Resources.Icon_01, Resources.Icon_02,
                Resources.Icon_03, Resources.Icon_04, Resources.Icon_05,
                Resources.Icon_06, Resources.Icon_07, Resources.Icon_08,
                Resources.Icon_09,};

            List<object[]> data = new List<object[]>();
            for (int i = 0; i < 1000000; i++)
            {
                var row = new object[10];
                data.Add(row);
                row[0] = i + 1;
                for (int col = 1; col < 10; col++)
                {
                    if (random.Next(4) > 0)             // 75%的概率单元格会加载图片
                    {
                        var imgIndex = random.Next(images.Length);
                        row[col] = images[imgIndex];
                    }
                }
            }

            gridView.DataSource = data;
        }

        private void tbDisplayRow_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tbDisplayRow.Text, out var row) || row < 1)
            {
                return;
            }
            else
            {
                gridView.DisplayRow = row;
            }
        }

        private void btnSelectedColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AllowFullOpen = false;
            dialog.ShowHelp = true;
            dialog.Color = gridView.SelectedBackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gridView.SelectedBackColor = dialog.Color;
            }
        }

        private void btnHoveredColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.AllowFullOpen = false;
            dialog.ShowHelp = true;
            dialog.Color = gridView.HoveredBackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                gridView.HoveredBackColor = dialog.Color;
            }
        }

        private void btnSelectionMode_Click(object sender, EventArgs e)
        {
            var text = (sender as ToolStripMenuItem).Text;
            var mode = (CellSelectionMode)Enum.Parse(typeof(CellSelectionMode), text);
            gridView.SelectionMode = mode;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var row = new object[] { 9999, Resources.Icon_00, Resources.Icon_01, Resources.Icon_02, Resources.Icon_03 };
            gridView.DataSource.Add(row);
            gridView.Invalidate();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = gridView.SelectedRowIndex;
            if (gridView.DataSource!=null && row >= 0 && row < gridView.DataSource.Count)
            {
                gridView.DataSource.RemoveAt(row);
                gridView.Invalidate();
            }
        }
    }
}
