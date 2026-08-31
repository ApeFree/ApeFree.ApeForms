using ApeFree.ApeDialogs.Settings;
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    [ToolboxItem(false)]
    public class OptionButton : SimpleButton
    {
        /// <summary>
        /// 单击事件回调
        /// </summary>
        internal Action<EventArgs> ClickCallback { get; set; }

        /// <summary>
        /// 选项信息
        /// </summary>
        public DialogOption Option { get; }

        //public OptionButton(DialogOption option)
        //{
        //    Option = option;

        //    Text= option.Text;
        //    Enabled= option.Enabled;
        //}

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            ClickCallback?.Invoke(e);
        }
    }
}
