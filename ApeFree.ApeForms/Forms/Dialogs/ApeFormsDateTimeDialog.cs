using ApeFree.ApeDialogs.Settings;
using System;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsDateTimeDialog : ApeFormsDialog<DateTime>
    {
        private readonly DateTimeDialogSettings _settings;
        private readonly DateTimePicker _dateTimePicker;

        public ApeFormsDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _dateTimePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = !string.IsNullOrEmpty(settings.DateTimeFormat) ? settings.DateTimeFormat : "yyyy-MM-dd HH:mm:ss",
                Value = settings.DefaultDateTime,
                Dock = DockStyle.Top,
            };

            if (settings.Font.HasValue)
            {
                _dateTimePicker.Font = settings.Font.Value.ToFont();
            }

            InnerDialog.SetContentView(_dateTimePicker);
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => OnConfirmRequested();
            }
            else if (ReferenceEquals(dialogOption, _settings.CurrentTimeOption))
            {
                button.Click += (_, _) => _dateTimePicker.Value = DateTime.Now;
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(_dateTimePicker.Value);
    }
}