using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeDialogs.Theme;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    /// <summary>
    /// ApeForms Dialog 实现基类。统一管理 DialogForm 宿主窗体、选项装配、快捷键与模态展示。
    /// </summary>
    /// <typeparam name="TResult">结果数据类型</typeparam>
    public class ApeFormsDialog<TResult> : BaseDialog<Control, OptionButton, Control, TResult>
    {
        public static Size DefaultDialogSize = new Size(480, 240);

        private readonly IDialogTheme _theme;

        public DialogForm InnerDialog { get; private set; }

        public override string Title
        {
            get => InnerDialog?.Text ?? string.Empty;
            set
            {
                if (InnerDialog != null)
                {
                    InnerDialog.Text = value;
                }
            }
        }

        public override string Content
        {
            get => InnerDialog?.Content ?? string.Empty;
            set
            {
                if (InnerDialog != null)
                {
                    InnerDialog.Content = value;
                }
            }
        }

        public ApeFormsDialog(DialogSettings<TResult> settings, Control context = null, Control contentView = null)
            : base(settings, context, contentView)
        {
            _theme = settings.Theme ?? DefaultTheme.Instance;

            InnerDialog = new DialogForm
            {
                Text = settings.Title,
                Content = settings.Content ?? string.Empty,
                ControlBox = settings.Cancelable,
                Size = settings.DialogSize.HasValue ? settings.DialogSize.Value.ToSize() : DefaultDialogSize,
            };

            if (settings.Font.HasValue)
            {
                InnerDialog.ContentFont = settings.Font.Value.ToFont();
            }

            var reminderColor = (settings.ReminderColor ?? _theme.ReminderColor).ToColor();
            InnerDialog.ReminderColor = reminderColor;

            if (contentView != null)
            {
                InnerDialog.SetContentView(contentView);
            }

            InnerDialog.Shown += (_, _) => RaiseShown();
            InnerDialog.FormClosed += (_, _) => Cancel();

            InnerDialog.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape && settings.Cancelable)
                {
                    Cancel();
                }
            };
        }

        /// <inheritdoc/>
        public override OptionButton AddOption(DialogOption dialogOption)
        {
            if (dialogOption == null)
            {
                throw new ArgumentNullException(nameof(dialogOption));
            }

            var btn = new OptionButton(dialogOption);

            var apeColor = _theme.GetColor(dialogOption.Tag);
            btn.BackColor = apeColor.ToColor();

            btn.Click += (s, e) =>
            {
                dialogOption.Action?.Invoke(this);
                dialogOption.OnSelected?.Invoke(this, new OptionSelectedEventArgs(this, dialogOption));
            };

            InnerDialog.AddButton(btn);
            return btn;
        }

        /// <inheritdoc/>
        public override void ClearOptions()
        {
            InnerDialog?.ClearButtons();
        }

        /// <inheritdoc/>
        protected override void ShowHandler()
        {
            if (InnerDialog == null)
            {
                return;
            }

            try
            {
                Form parentForm = Context?.FindForm() ?? Form.ActiveForm;
                if (parentForm != null && !parentForm.IsDisposed)
                {
                    InnerDialog.StartPosition = FormStartPosition.CenterParent;
                    InnerDialog.TopMost = parentForm.TopMost;
                    InnerDialog.ShowDialog(parentForm);
                }
                else
                {
                    InnerDialog.StartPosition = FormStartPosition.CenterScreen;
                    InnerDialog.ShowDialog();
                }
            }
            finally
            {
                InnerDialog?.Dispose();
                InnerDialog = null;
            }
        }

        /// <inheritdoc/>
        protected override void DismissHandler()
        {
            if (InnerDialog == null || InnerDialog.IsDisposed)
            {
                return;
            }

            if (InnerDialog.InvokeRequired)
            {
                InnerDialog.BeginInvoke(new Action(() =>
                {
                    if (InnerDialog != null && !InnerDialog.IsDisposed)
                    {
                        InnerDialog.Close();
                    }
                }));
            }
            else
            {
                InnerDialog.Close();
            }
        }

        /// <inheritdoc/>
        protected override void OnValidationFailed(ValidationResult result)
        {
            if (InnerDialog != null && !InnerDialog.IsDisposed)
            {
                InnerDialog.Shake();
                Toast.Show(result.ErrorMessage, 2000, InnerDialog, ToastMode.Reuse);
            }
        }

        /// <summary>
        /// 回车确认动作（由具体 Dialog 覆写）。
        /// </summary>
        protected virtual void OnConfirmRequested()
        {
        }
    }
}