using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Core.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            btn.Enabled = option.Enable;
            btn.AutoSize = true;

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

        public void SetOptionClickAction(string optionText, Action<IDialog, OptionButton> onClick)
        {
            var btn = InnerDialog.FindButtonByText(optionText);
            if (btn != null)
            {
                btn.Click += (s, e) => onClick?.Invoke(this, (OptionButton)btn);
            }
        }
    }
    public class ApeFormsDialogProvider : DialogProvider<Control>
    {
        public override IDialog<DateTime> CreateDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
        {
            var view = new DatePicker();

            var dialog = new ApeFormsDialog<DateTime>(settings, v => (v as DatePicker).SelectedDate);
            dialog.ContentView = view;

            Action<IDialog, Control> confirmOptionCallback = (d, o) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    d.Dismiss(false);
                }
            };

            // 添加选项按钮
            dialog.SetOptionClickAction(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.SetOptionClickAction(settings.ConfirmOptionText, confirmOptionCallback);
            dialog.SetOptionClickAction(settings.CurrentTimeOptionText, (d, o) => view.SelectedDate = DateTime.Now);

            return dialog;
        }

        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
        {
            var view = new TextBox();
            view.Multiline = settings.IsMultiline;
            view.Text = settings.DefaultContent;

            var dialog = new ApeFormsDialog<string>(settings, v => v.Text);
            dialog.ContentView = view;

            Action<IDialog, Control> confirmOptionCallback = (d, o) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    d.Dismiss(false);
                }
            };

            // 添加选项按钮
            dialog.SetOptionClickAction(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.SetOptionClickAction(settings.ConfirmOptionText, confirmOptionCallback);
            dialog.SetOptionClickAction(settings.ClearOptionText, (d, o) => view.Clear());

            // 单行输入的模式下，在输入框内使用回车键可确认输入
            if (!view.Multiline)
            {
                view.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        confirmOptionCallback.Invoke(dialog, view);
                    }
                };
            }

            return dialog;
        }

        public override IDialog<bool> CreateMessageDialog(MessageDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<bool>(settings);
            dialog.SetOptionClickAction(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            return dialog;
        }

        public override IDialog<string> CreatePasswordDialog(PasswordDialogSettings settings, Control context = null)
        {
            ApeFormsDialog<string> dialog = (ApeFormsDialog<string>)CreateInputDialog(settings, context);
            ((TextBox)dialog.ContentView).PasswordChar = settings.PasswordChar;
            return dialog;
        }

        public override IDialog<bool> CreatePromptDialog(PromptDialogSettings settings, Control context = null)
        {
            Control control = new Control();
            control.Visible = false;
            control.Enabled = false;

            var dialog = new ApeFormsDialog<bool>(settings, ctrl => ctrl.Enabled);
            dialog.ContentView = control;

            dialog.SetOptionClickAction(settings.PositiveOptionText, (d, o) => { control.Enabled = true; dialog.ExtractResultFromView(); d.Dismiss(false); });
            dialog.SetOptionClickAction(settings.NegativeOptionText, (d, o) => { control.Enabled = false; dialog.ExtractResultFromView(); d.Dismiss(false); });
            return dialog;
        }

        public override IDialog<T> CreateSelectionDialog<T>(SelectionDialogSettings<T> settings, IEnumerable<T> collection, T defaultSelectedItem, Control context = null)
        {
            var view = new CheckedListBox();
            view.SelectedIndexChanged += (s, e) =>
            {
                for (int i = 0; i < view.Items.Count; i++)
                {
                    view.SetItemChecked(i, false);
                }
                view.SetItemChecked(view.SelectedIndex, true);
            };
            foreach (var item in collection)
            {
                view.Items.Add(settings.ItemDisplayTextConvertCallback(item), item.Equals(defaultSelectedItem));
            }
            var dialog = new ApeFormsDialog<T>(settings, v =>
            {
                var index = (v as CheckedListBox).SelectedIndex;
                return index >= 0 ? collection.ElementAt(index) : default(T);
            });
            dialog.ContentView = view;

            Action<IDialog, Control> confirmOptionCallback = (d, o) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    d.Dismiss(false);
                }
            };

            dialog.SetOptionClickAction(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.SetOptionClickAction(settings.ConfirmOptionText, confirmOptionCallback);

            return dialog;
        }

        public override IDialog<IEnumerable<T>> CreateMultipleSelectionDialog<T>(MultipleSelectionDialogSettings<T> settings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems, Control context = null)
        {
            var view = new CheckedListBox();
            view.SelectedIndexChanged += (s, e) =>
            {
                if (view.SelectedIndex != -1)
                {
                    view.SetItemChecked(view.SelectedIndex, !view.GetItemChecked(view.SelectedIndex));
                }
            };
            foreach (var item in collection)
            {
                view.Items.Add(settings.ItemDisplayTextConvertCallback(item), defaultSelectedItems?.Contains(item) ?? false);
            }
            var dialog = new ApeFormsDialog<IEnumerable<T>>(settings, v =>
            {
                List<T> selectedItems = new List<T>();
                for (int i = 0; i < view.Items.Count; i++)
                {
                    if (view.GetItemChecked(i))
                    {
                        selectedItems.Add(collection.ElementAt(i));
                    }
                }
                return selectedItems;
            });
            dialog.ContentView = view;

            Action<IDialog, Control> confirmOptionCallback = (d, o) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    d.Dismiss(false);
                }
            };

            dialog.SetOptionClickAction(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.SetOptionClickAction(settings.ConfirmOptionText, confirmOptionCallback);
            dialog.SetOptionClickAction(settings.SelectAllOptionText, (d, o) =>
            {
                view.SelectedIndex = -1;
                for (int i = 0; i < view.Items.Count; i++)
                {
                    view.SetItemChecked(i, true);
                }
            });
            dialog.SetOptionClickAction(settings.ReverseSelectedOptionText, (d, o) =>
            {
                view.SelectedIndex = -1;
                for (int i = 0; i < view.Items.Count; i++)
                {
                    view.SetItemChecked(i, !view.GetItemChecked(i));
                }
            });

            return dialog;
        }
    }
}