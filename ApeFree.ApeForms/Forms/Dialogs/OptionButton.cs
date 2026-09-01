using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Controls.Buttons;
using System;
using System.ComponentModel;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    [ToolboxItem(false)]
    public class OptionButton : SimpleButton
    {
        /// <summary>
        /// 选项信息
        /// </summary>
        public DialogOption Option { get; }

        public OptionButton(DialogOption option)
        {
            Option = option ?? throw new ArgumentNullException(nameof(option));
            Text = option.Text;
            Enabled = option.IsEnabled;
            AutoSize = true;
        }

        public OptionButton()
        {
            AutoSize = true;
        }
    }
}