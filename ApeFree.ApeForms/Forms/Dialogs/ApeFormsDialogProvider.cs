using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeDialogs.Theme;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    /// <summary>
    /// ApeDialogs 的 WinForms 实现提供器。
    /// 上下文类型为 <see cref="Control"/>（通常为主窗体或父控件）。
    /// </summary>
    public class ApeFormsDialogProvider : DialogProvider<Control>
    {
        private readonly IDialogTheme _theme;

        /// <summary>
        /// 默认构造提供器（使用默认主题）。
        /// </summary>
        public ApeFormsDialogProvider()
            : this(DefaultTheme.Instance)
        {
        }

        /// <summary>
        /// 构造提供器。
        /// </summary>
        /// <param name="theme">主题配色</param>
        public ApeFormsDialogProvider(IDialogTheme theme)
        {
            _theme = theme ?? throw new ArgumentNullException(nameof(theme));
        }

        /// <inheritdoc/>
        public override IDialogTheme Theme => _theme;

        /// <inheritdoc/>
        public override IDialog<bool> CreateMessageDialog(MessageDialogSettings settings, Control context = null)
            => new ApeFormsMessageDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<bool> CreatePromptDialog(PromptDialogSettings settings, Control context = null)
            => new ApeFormsPromptDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
            => new ApeFormsInputDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<string> CreatePasswordDialog(PasswordDialogSettings settings, Control context = null)
            => new ApeFormsPasswordDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<T> CreateSelectionDialog<T>(SelectionDialogSettings<T> settings, IEnumerable<T> collection, T defaultSelectedItem = default, Control context = null)
            => new ApeFormsSelectionDialog<T>(settings, collection, defaultSelectedItem, context);

        /// <inheritdoc/>
        public override IDialog<IReadOnlyList<T>> CreateMultipleSelectionDialog<T>(MultipleSelectionDialogSettings<T> settings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems = null, Control context = null)
            => new ApeFormsMultipleSelectionDialog<T>(settings, collection, defaultSelectedItems, context);

        /// <inheritdoc/>
        public override IDialog<DateTime> CreateDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
            => new ApeFormsDateTimeDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<DataEntrySheet> CreateDataEntrySheetDialog(DataEntrySheet sheet, DataEntrySheetDialogSettings settings, Control context = null)
            => new ApeFormsDataEntrySheetDialog(sheet, settings, context);

        /// <inheritdoc/>
        public override IDialog<IReadOnlyList<string>> CreateOpenFileDialog(OpenFileDialogSettings settings, Control context = null)
            => new ApeFormsOpenFileDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<IReadOnlyList<string>> CreateOpenFolderDialog(OpenFolderDialogSettings settings, Control context = null)
            => new ApeFormsOpenFolderDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<decimal> CreateNumberInputDialog(NumberInputDialogSettings settings, Control context = null)
            => new ApeFormsNumberInputDialog(settings, context);

        /// <inheritdoc/>
        public override IAdjustDialog<decimal> CreateSliderDialog(SliderDialogSettings settings, Control context = null)
            => new ApeFormsSliderDialog(settings, context);

        /// <inheritdoc/>
        public override IAdjustDialog<ValueRange<decimal>> CreateRangeSliderDialog(RangeSliderDialogSettings settings, Control context = null)
            => new ApeFormsRangeSliderDialog(settings, context);

        /// <inheritdoc/>
        public override IProgressDialog CreateProgressDialog(ProgressDialogSettings settings, Control context = null)
            => new ApeFormsProgressDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<bool> CreateToastDialog(ToastDialogSettings settings, Control context = null)
            => new ApeFormsToastDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<bool> CreateTextDialog(TextDialogSettings settings, Control context = null)
            => new ApeFormsTextDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<ApeColor> CreateColorDialog(ColorDialogSettings settings, Control context = null)
            => new ApeFormsColorDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<string> CreateSaveFileDialog(SaveFileDialogSettings settings, Control context = null)
            => new ApeFormsSaveFileDialog(settings, context);

        /// <inheritdoc/>
        public override IDialog<string> CreateComboBoxInputDialog(ComboBoxInputDialogSettings settings, Control context = null)
            => new ApeFormsComboBoxInputDialog(settings, context);
    }
}