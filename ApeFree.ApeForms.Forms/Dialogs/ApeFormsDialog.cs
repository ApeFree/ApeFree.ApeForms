using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsDialog<TResult> : BaseDialog<Control, OptionButton, Control, TResult>
    {
        public static Size DefaultDialogSize = new Size(480, 240);

        public DialogForm InnerDialog { get; }
        public override string Title { get => InnerDialog.Text; set => InnerDialog.Text = value; }
        public override string Content { get => InnerDialog.Content; set => InnerDialog.Content = value; }

        public ApeFormsDialog(DialogSettings<TResult> settings, Func<Control, TResult> extractResultFromViewHandler = null) : base(settings)
        {
            InnerDialog = new DialogForm();

            Title = settings.Title;
            Content = settings.Content;
            InnerDialog.ControlBox = settings.Cancelable;
            InnerDialog.Size = settings.DialogSize ?? DefaultDialogSize;

            foreach (var item in settings.GetOptions())
            {
                AddOption(item);
            }

            ExtractResultFromViewHandler = extractResultFromViewHandler;
        }

        protected override void DismissHandler()
        {
            InnerDialog.Close();
        }

        protected override void ShowHandler()
        {
            InnerDialog.SetContentView(ContentView);
            InnerDialog.ShowDialog();
        }

        public override OptionButton AddOption(DialogOption option, Action<IDialog, OptionButton> onClick = null)
        {
            var btn = CreateOptionHandler(option);
            btn.Click += (s, e) => onClick?.Invoke(this, btn);
            InnerDialog.AddButton(btn);
            return btn;
        }

        public override void ClearOptions()
        {
            InnerDialog.ClearButtons();
        }

        protected override void PrecheckFailsCallback()
        {
            // 抖动窗口
            InnerDialog.Shake();
        }

        protected override OptionButton CreateOptionHandler(DialogOption option)
        {
            var btn = new OptionButton();
            btn.Text = option.Text;
            btn.Enabled = option.Enabled;
            btn.AutoSize = true;
            btn.ClickCallback = e => option.OptionSelectedCallback?.Invoke(btn, new OptionSelectedEventArgs(this, option));

            switch (option.OptionType)
            {
                case DialogOptionType.Neutral:
                    btn.BackColor = SystemColors.Highlight;
                    break;
                case DialogOptionType.Positive:
                    btn.BackColor = Color.ForestGreen;
                    break;
                case DialogOptionType.Negative:
                    btn.BackColor = Color.IndianRed;
                    break;
                case DialogOptionType.Functional:
                    btn.BackColor = SystemColors.Highlight;
                    break;
                case DialogOptionType.Special:
                    btn.BackColor = Color.MediumPurple;
                    break;
            }

            return btn;
        }

        //public void SetOptionClickAction(string optionText, Action<IDialog, OptionButton> onClick)
        //{
        //    var btn = InnerDialog.FindButtonByText(optionText);
        //    if (btn != null)
        //    {
        //        btn.Click += (s, e) => onClick?.Invoke(this, (OptionButton)btn);
        //    }
        //}
    }
}