using ApeFree.ApeDialogs.Settings;
using System;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsNumberInputDialog : ApeFormsDialog<decimal>
    {
        private readonly NumberInputDialogSettings _settings;
        private readonly NumericUpDown _numericUpDown;

        public ApeFormsNumberInputDialog(NumberInputDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _numericUpDown = new NumericUpDown
            {
                Minimum = settings.Minimum,
                Maximum = settings.Maximum,
                DecimalPlaces = settings.DecimalPlaces,
                Increment = settings.Increment,
                Value = Math.Max(settings.Minimum, Math.Min(settings.Maximum, settings.DefaultValue)),
                Dock = DockStyle.Top,
            };

            if (settings.Font.HasValue)
            {
                _numericUpDown.Font = settings.Font.Value.ToFont();
            }

            InnerDialog.SetContentView(_numericUpDown);
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

        protected override void OnConfirmRequested() => TrySubmit(_numericUpDown.Value);

        protected override void OnClearRequested()
        {
            _numericUpDown.Value = Math.Max(_settings.Minimum, Math.Min(_settings.Maximum, 0));
            _numericUpDown.Focus();
        }
    }
}