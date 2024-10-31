using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Core.Controls;
using ApeFree.ApeForms.Core.Controls.Views;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private ErrorProvider errorProvider = new ErrorProvider() { BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError, BlinkRate = 100 };

        /// <inheritdoc/>
        public override IDialog<DateTime> CreateDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
        {
            var view = new DateTimePicker();
            view.Format = DateTimePickerFormat.Custom;
            view.CustomFormat = settings.DateTimeFormat;
            view.Value = settings.DefaultDateTime;
            view.Font = settings.Font;

            var dialog = new ApeFormsDialog<DateTime>(settings);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.Result.UpdateResultData(view.Value);
                var result = dialog.PerformPrecheck();
                //if (result.IsSuccess)
                //{
                //    e.Dialog.Dismiss(false);
                //}
            };

            // 添加选项按钮
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;
            settings.CurrentTimeOption.OptionSelectedCallback = (s, e) => view.Value = DateTime.Now;

            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
        {
            var view = new TextBox();
            view.Multiline = settings.IsMultiline;
            view.Text = settings.DefaultText;
            view.MaxLength = settings.MaximumLength;
            view.Font = settings.Font;

            var dialog = new ApeFormsDialog<string>(settings);
            dialog.ContentView = view;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                dialog.Result.UpdateResultData(view.Text);
                var result = dialog.PerformPrecheck();
                //if (result.IsSuccess)
                //{
                //    e.Dialog.Dismiss(false);
                //}
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

        /// <inheritdoc/>
        public override IDialog<bool> CreateMessageDialog(MessageDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<bool>(settings);
            settings.ConfirmOption.OptionSelectedCallback = (s, e) => e.Dialog.Dismiss(false);
            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<string> CreatePasswordDialog(PasswordDialogSettings settings, Control context = null)
        {
            ApeFormsDialog<string> dialog = (ApeFormsDialog<string>)CreateInputDialog(settings, context);
            ((TextBox)dialog.ContentView).PasswordChar = settings.PasswordChar;
            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<bool> CreatePromptDialog(PromptDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<bool>(settings);
            settings.PositiveOption.OptionSelectedCallback = (s, e) => { dialog.Result.UpdateResultData(true); e.Dialog.Dismiss(false); };
            settings.NegativeOption.OptionSelectedCallback = (s, e) => { dialog.Result.UpdateResultData(false); e.Dialog.Dismiss(false); };

            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<T> CreateSelectionDialog<T>(SelectionDialogSettings<T> settings, IEnumerable<T> collection, T defaultSelectedItem, Control context = null)
        {
            var view = new CheckedListBox();
            view.Font = settings.Font;
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
                var value = view.CheckedItems.Count > 0 ? collection.ElementAt(view.CheckedIndices[0]) : default;
                dialog.Result.UpdateResultData(value);
                var result = dialog.PerformPrecheck();
            };

            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;

            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<IEnumerable<T>> CreateMultipleSelectionDialog<T>(MultipleSelectionDialogSettings<T> settings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems, Control context = null)
        {
            var view = new CheckedListBox();
            view.Font = settings.Font;

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
                var result = dialog.PerformPrecheck();
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

        /// <inheritdoc/>
        public override IDialog<DataEntrySheet> CreateDataEntrySheetDialog(DataEntrySheet sheet, DataEntrySheetDialogSettings settings, Control context = null)
        {
            var view = new DataEntryView();
            view.Fields = sheet.Fields.ToArray();
            view.Font = settings.Font;

            var dialog = new ApeFormsDialog<DataEntrySheet>(settings);
            dialog.ContentView = view;
            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                errorProvider.Clear();

                foreach (var field in sheet.Fields)
                {
                    var vcr = field.ValidityCheck();
                    if (!vcr.IsSuccess)
                    {
                        var ctrl = view.GetFieldControlByTitle(field.Title);
                        errorProvider.SetIconAlignment(ctrl, ErrorIconAlignment.TopRight);
                        errorProvider.SetIconPadding(ctrl, -20);
                        errorProvider.SetError(ctrl, vcr.ErrorMessage);
                        Toast.Show(vcr.ErrorMessage, 2000, null, ToastMode.Reuse);
                        // dialog.InnerDialog.Shake();

                        // 将垂直滚动条滚动到校验失败的控件的位置
                        //view.VerticalScroll.Value = ctrl.Top - view.AutoScrollPosition.Y;
                        //view.PerformLayout();
                        view.VerticalScrollGradualChange(ctrl, 0, 2);

                        return;
                    }
                }

                dialog.Result.UpdateResultData(sheet);
                var result = dialog.PerformPrecheck();
                //if (result.IsSuccess)
                //{
                //    e.Dialog.Dismiss(false);
                //}
            };
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;
            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<string[]> CreateOpenFileDialog(string path, OpenFileDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<string[]>(settings);

            var view = context is DriveBrowserView v ? v : new DriveBrowserView();
            view.DisplayItemType = DisplayItemType.FolderAndFile;
            view.SearchPattern = settings.SearchPattern;
            view.MultiSelect = settings.MultiSelect;
            view.Font = settings.Font;
            view.OnSelectedItemsChanged += (s, e) => dialog.Content = view.SelectedFiles.Join("\r\n");
            dialog.ContentView = view;

            DialogEventHandler openDefaultFolderHandler = null;
            openDefaultFolderHandler = new DialogEventHandler((d, e) =>
            {
                view.OpenFolder(Path.GetDirectoryName(path));
                dialog.Shown -= openDefaultFolderHandler;
            });
            dialog.Shown += openDefaultFolderHandler;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                if (view.SelectedFiles.Any())
                {
                    dialog.Result.UpdateResultData(view.SelectedFiles);
                    var result = dialog.PerformPrecheck();
                }
            };

            // 添加选项按钮
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;

            return dialog;
        }

        /// <inheritdoc/>
        public override IDialog<string[]> CreateOpenFolderDialog(string path, OpenFolderDialogSettings settings, Control context = null)
        {
            var dialog = new ApeFormsDialog<string[]>(settings);

            var view = context is DriveBrowserView v ? v : new DriveBrowserView();
            view.DisplayItemType = DisplayItemType.OnlyFolder;
            view.MultiSelect = settings.MultiSelect;
            view.Font = settings.Font;
            view.OnSelectedItemsChanged += (s, e) => dialog.Content = view.SelectedFolders.Join("\r\n");
            dialog.ContentView = view;

            DialogEventHandler openDefaultFolderHandler = null;
            openDefaultFolderHandler = new DialogEventHandler((d, e) =>
            {
                view.OpenFolder(path);
                dialog.Shown -= openDefaultFolderHandler;
            });
            dialog.Shown += openDefaultFolderHandler;

            Action<object, OptionSelectedEventArgs> confirmOptionCallback = (s, e) =>
            {
                if (view.SelectedFolders.Any())
                {
                    dialog.Result.UpdateResultData(view.SelectedFolders);
                    var result = dialog.PerformPrecheck();
                }
            };

            // 添加选项按钮
            settings.ConfirmOption.OptionSelectedCallback = confirmOptionCallback;

            return dialog;
        }
    }
}