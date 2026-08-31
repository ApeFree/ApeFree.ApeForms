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
using ApeFree.ApeForms.Utils;
using System;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Controls.Containers
{
    /// <summary>
    /// 无色的面板
    /// 背景色随父容器的背景色变化而变化
    /// </summary>
    public class ColorlessPanel : UserControl
    {
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);

            // 如果有父容器，则获取其背景色，并更新控件的背景色
            if (this.Parent != null)
            {
                this.BackColor = this.Parent.BackColor;
            }
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);

            // 更新控件的背景色为父容器的背景色
            this.BackColor = this.Parent.BackColor;
        }
    }
}
