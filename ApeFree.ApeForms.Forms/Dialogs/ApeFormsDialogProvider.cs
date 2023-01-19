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
    public class ApeFormsDialog<TResult> : BaseDialog<Control, Control, Control, TResult>
    {
        public DialogForm InnerDialog { get; }
        public override string Title { get => InnerDialog.Text; set => InnerDialog.Text = value; }
        public override string Content { get => InnerDialog.Content; set => InnerDialog.Content = value; }

        public ApeFormsDialog(DialogSettings<TResult> settings, Func<Control, TResult> extractResultFromViewHandler = null) : base(settings)
        {
            InnerDialog = new DialogForm();

            Title = settings.Title;
            Content = settings.Content;

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

        public override Control AddOption(string text, Action<IDialog, Control> onClick = null)
        {
            var option = new SimpleButton();
            option.Text = text;
            option.AutoSize= true;
            option.Click += (s, e) => onClick?.Invoke(this, option);

            InnerDialog.AddButton(option);

            return option;
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
            dialog.AddOption(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.AddOption(settings.ConfirmOptionText, confirmOptionCallback);
            dialog.AddOption(settings.CurrentTimeOptionText, (d, o) => view.SelectedDate = DateTime.Now);

            return dialog;
        }

        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
        {
            var view = new TextBox();
            view.Multiline = settings.IsMultiline;

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
            dialog.AddOption(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.AddOption(settings.ConfirmOptionText, confirmOptionCallback);
            dialog.AddOption(settings.ClearOptionText, (d, o) => view.Clear());

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
            dialog.AddOption(settings.CancelOptionText, (d, o) => d.Dismiss(true));
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

            dialog.AddOption(settings.PositiveOptionText, (d, o) => { control.Enabled = true; dialog.ExtractResultFromView(); d.Dismiss(false); });
            dialog.AddOption(settings.NegativeOptionText, (d, o) => { control.Enabled = false; dialog.ExtractResultFromView(); d.Dismiss(false); });
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

            dialog.AddOption(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.AddOption(settings.ConfirmOptionText, confirmOptionCallback);

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

            dialog.AddOption(settings.CancelOptionText, (d, o) => d.Dismiss(true));
            dialog.AddOption(settings.ConfirmOptionText, confirmOptionCallback);
            dialog.AddOption(settings.SelectAllOptionText, (d, o) =>
            {
                view.SelectedIndex = -1;
                for (int i = 0; i < view.Items.Count; i++)
                {
                    view.SetItemChecked(i, true);
                }
            });
            dialog.AddOption(settings.ReverseSelectedOptionText, (d, o) =>
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