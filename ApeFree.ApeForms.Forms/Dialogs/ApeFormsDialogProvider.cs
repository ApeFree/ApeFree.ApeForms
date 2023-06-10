using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Core.Controls;
using ApeFree.ApeForms.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeDialogs
{
    public class ApeFormsDialogProvider : DialogProvider<Control>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="settings"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override IDialog<DateTime> CreateDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
        {
            var view = new DateTimePicker();
            view.Format = DateTimePickerFormat.Custom;
            view.CustomFormat = settings.DateTimeFormat;

            var dialog = new ApeFormsDialog<DateTime>(settings);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.Result.UpdateResultData(view.Value);
                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };

            // 添加选项按钮
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;
            settings.CurrentTimeOption.OptionSelectedCallback = (s, e) => view.Value = DateTime.Now;

            return dialog;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
        {
            var view = new TextBox();
            view.Multiline = settings.IsMultiline;
            view.Text = settings.DefaultText;

            if (settings.PrecheckResult == null)
            {
                settings.PrecheckResult = text =>
                {
                    if (!settings.AllowEmpty && string.IsNullOrEmpty(text))
                    {
                        return false;
                    }
                    if (text.Length > settings.MaximumLength || text.Length < settings.MinimumLength)
                    {
                        return false;
                    }
                    return true;
                };
            }

            var dialog = new ApeFormsDialog<string>(settings);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.Result.UpdateResultData(view.Text);
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="settings"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override IDialog<bool> CreateMessageDialog(MessageDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<bool>(settings);
            settings.ConfirmOption.OptionSelectedCallback = (s, e) => e.Dialog.Dismiss(false);
            return dialog;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="settings"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override IDialog<string> CreatePasswordDialog(PasswordDialogSettings settings, Control context = null)
        {
            ApeFormsDialog<string> dialog = (ApeFormsDialog<string>)CreateInputDialog(settings, context);
            ((TextBox)dialog.ContentView).PasswordChar = settings.PasswordChar;
            return dialog;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="settings"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override IDialog<bool> CreatePromptDialog(PromptDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<bool>(settings);
            settings.PositiveOption.OptionSelectedCallback = (s, e) => { dialog.Result.UpdateResultData(true); e.Dialog.Dismiss(false); };
            settings.NegativeOption.OptionSelectedCallback = (s, e) => { dialog.Result.UpdateResultData(false); e.Dialog.Dismiss(false); };

            return dialog;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc/></typeparam>
        /// <param name="settings"><inheritdoc/></param>
        /// <param name="collection"><inheritdoc/></param>
        /// <param name="defaultSelectedItem"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
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
            var dialog = new ApeFormsDialog<T>(settings);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                var value = view.SelectedIndex >= 0 ? collection.ElementAt(view.SelectedIndex) : default;
                dialog.Result.UpdateResultData(value);

                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };

            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;

            return dialog;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="T"><inheritdoc/></typeparam>
        /// <param name="settings"><inheritdoc/></param>
        /// <param name="collection"><inheritdoc/></param>
        /// <param name="defaultSelectedItems"><inheritdoc/></param>
        /// <param name="context"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public override IDialog<IEnumerable<T>> CreateMultipleSelectionDialog<T>(MultipleSelectionDialogSettings<T> settings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems, Control context = null)
        {
            var view = new CheckedListBox();

            // 关联Selected与Checked（使操作变简单）
            view.SelectedIndexChanged += (s, e) =>
            {
                if (view.SelectedIndex != -1)
                {
                    view.SetItemChecked(view.SelectedIndex, !view.GetItemChecked(view.SelectedIndex));
                }
            };

            // 添加选项集合到列表控件
            foreach (var item in collection)
            {
                view.Items.Add(settings.ItemDisplayTextConvertCallback(item), defaultSelectedItems?.Contains(item) ?? false);
            }
            var dialog = new ApeFormsDialog<IEnumerable<T>>(settings);
            dialog.ContentView = view;

            settings.ConfirmOption.OptionSelectedCallback = (s, e) =>
            {
                var items = view.CheckedItems.Cast<object>().Select(str => view.Items.IndexOf(str)).Select(i => collection.ElementAt(i));
                dialog.Result.UpdateResultData(items);
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

        public override IDialog<DataEntrySheet> CreateDataEntrySheetDialog(DataEntrySheet sheet, DataEntrySheetDialogSettings settings, Control context = null)
        {
            var view = new DataEntryView();
            view.Fields = sheet.Fields.ToArray();

            var dialog = new ApeFormsDialog<DataEntrySheet>(settings);
            dialog.ContentView = view;
            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.Result.UpdateResultData(sheet);
                if (dialog.PerformPrecheck())
                {
                    e.Dialog.Dismiss(false);
                }
            };
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;
            return dialog;
        }
    }
}