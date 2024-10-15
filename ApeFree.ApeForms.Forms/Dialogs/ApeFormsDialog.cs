using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Forms.Notifications;
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

        public ApeFormsDialog(DialogSettings<TResult> settings) : base(settings)
        {
            InnerDialog = new DialogForm();

            Title = settings.Title;
            Content = settings.Content;
            InnerDialog.ControlBox = settings.Cancelable;
            InnerDialog.Size = settings.DialogSize ?? DefaultDialogSize;
            InnerDialog.ContentFont = settings.Font;
            InnerDialog.ReminderColor = settings.ReminderColor;

            foreach (var item in settings.GetOptions())
            {
                AddOption(item);
            }
        }

        /// <inheritdoc/>
        protected override void DismissHandler()
        {
            InnerDialog.Close();
        }

        /// <inheritdoc/>
        protected override void ShowHandler()
        {
            InnerDialog.SetContentView(ContentView);
            InnerDialog.ShowDialog();
        }

        /// <inheritdoc/>
        public override OptionButton AddOption(DialogOption option, Action<IDialog, OptionButton> onClick = null)
        {
            var btn = new OptionButton();
            btn.Text = option.Text;
            btn.Enabled = option.Enabled;
            btn.AutoSize = true;
            btn.ClickCallback = e => option.OptionSelectedCallback?.Invoke(btn, new OptionSelectedEventArgs(this, option));

            if (Settings.OptionTagColors?.TryGetValue(option.OptionTag, out Color color) != null)
            {
                btn.BackColor = color;
            }

            btn.Click += (s, e) => onClick?.Invoke(this, btn);
            InnerDialog.AddButton(btn);
            return btn;
        }

        /// <inheritdoc/>
        public override void ClearOptions()
        {
            InnerDialog.ClearButtons();
        }

        /// <inheritdoc/>
        protected override void PrecheckFailsCallback(FormatCheckResult result)
        {
            // 抖动窗口
            InnerDialog.Shake();

            // 消息提示
            Toast.Show(result.ErrorMessage, 2000, null, ToastMode.Reuse);
        }
    }
}