using ApeFree.ApeDialogs.Settings;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsDataEntrySheetDialog : ApeFormsDialog<DataEntrySheet>
    {
        private readonly DataEntrySheet _sheet;
        private readonly DataEntrySheetDialogSettings _settings;
        private readonly DataEntryView _view;

        public ApeFormsDataEntrySheetDialog(DataEntrySheet sheet, DataEntrySheetDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));
            _settings = settings;

            _view = new DataEntryView
            {
                Fields = sheet.Fields.ToArray(),
            };

            if (settings.Font.HasValue)
            {
                _view.Font = settings.Font.Value.ToFont();
            }

            InnerDialog.SetContentView(_view);
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => OnConfirmRequested();
            }
            return button;
        }

        protected override void OnConfirmRequested()
        {
            var validation = _sheet.Validate();
            if (!validation.IsSuccess)
            {
                OnValidationFailed(validation);
                return;
            }
            TrySubmit(_sheet);
        }
    }
}