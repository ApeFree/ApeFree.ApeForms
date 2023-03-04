using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Core.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsDialogProvider : DialogProvider<Control>
    {
        public override IDialog<DateTime> CreateDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
        {
            var view = new DatePicker();

            var dialog = new ApeFormsDialog<DateTime>(settings, v => (v as DatePicker).SelectedDate);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };

            // 添加选项按钮
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;
            settings.CurrentTimeOption.OptionSelectedCallback = (s, e) => view.SelectedDate = DateTime.Now;

            return dialog;
        }

        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
        {
            var view = new TextBox();
            view.Multiline = settings.IsMultiline;
            view.Text = settings.DefaultContent;

            var dialog = new ApeFormsDialog<string>(settings, v => v.Text);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };

            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;
            settings.ClearOption.OptionSelectedCallback = (s, e) => view.Clear();

            // 单行输入的模式下，在输入框内使用回车键可确认输入
            if (!view.Multiline)
            {
                view.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Return)
                    {
                        confirmOptionCallback.Invoke(view, new OptionSelectedEventArgs(dialog, settings.ConfirmOption));
                    }
                };
            }

            return dialog;
        }

        public override IDialog<bool> CreateMessageDialog(MessageDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<bool>(settings);
            settings.ConfirmOption.OptionSelectedCallback = (s, e) => e.Dialog.Dismiss(false);
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
            //Control control = new Control();
            //control.Visible = false;
            //control.Enabled = false;
            bool result = false;
            var dialog = new ApeFormsDialog<bool>(settings, ctrl => result);
            //dialog.ContentView = control;

            settings.PositiveOption.OptionSelectedCallback = (s, e) => { result = true; dialog.ExtractResultFromView(); e.Dialog.Dismiss(false); };
            settings.NegativeOption.OptionSelectedCallback = (s, e) => { result = false; dialog.ExtractResultFromView(); e.Dialog.Dismiss(false); };

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

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };

            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;

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

            settings.ConfirmOption.OptionSelectedCallback = (s, e) =>
            {
                dialog.ExtractResultFromView();
                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };
            settings.SelectAllOption.OptionSelectedCallback = (s, e) =>
            {
                view.SelectedIndex = -1;
                for (int i = 0; i < view.Items.Count; i++)
                {
                    view.SetItemChecked(i, true);
                }
            };
            settings.ReverseSelectedOption.OptionSelectedCallback = (s, e) =>
            {
                view.SelectedIndex = -1;
                for (int i = 0; i < view.Items.Count; i++)
                {
                    view.SetItemChecked(i, !view.GetItemChecked(i));
                }
            };

            return dialog;
        }
    }
}