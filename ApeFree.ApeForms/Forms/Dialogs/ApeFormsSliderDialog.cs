using ApeFree.ApeDialogs.Settings;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsSliderDialog : ApeFormsAdjustDialogBase<decimal>
    {
        private readonly SliderDialogSettings _settings;
        private readonly TrackBar _trackBar;
        private readonly Label _valLabel;
        private decimal _currentValue;

        public ApeFormsSliderDialog(SliderDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            _currentValue = Math.Max(settings.Minimum, Math.Min(settings.Maximum, settings.DefaultValue));

            var container = new Panel { Dock = DockStyle.Fill };

            _valLabel = new Label
            {
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(SystemFonts.DefaultFont.FontFamily, 14, FontStyle.Bold),
                Height = 32,
                Visible = settings.ShowValueLabel,
                Text = _currentValue.ToString("0.##", CultureInfo.CurrentCulture),
            };

            int steps = 100;
            _trackBar = new TrackBar
            {
                Dock = DockStyle.Top,
                Minimum = 0,
                Maximum = steps,
                TickStyle = settings.TickFrequency > 0 ? TickStyle.BottomRight : TickStyle.None,
            };

            double range = (double)(settings.Maximum - settings.Minimum);
            if (range <= 0) range = 1;
            double progress = (double)(_currentValue - settings.Minimum) / range;
            _trackBar.Value = (int)Math.Round(Math.Max(0, Math.Min(steps, progress * steps)));

            _trackBar.ValueChanged += (s, e) =>
            {
                double ratio = (double)_trackBar.Value / steps;
                _currentValue = settings.Minimum + (decimal)ratio * (settings.Maximum - settings.Minimum);
                if (settings.TickFrequency > 0)
                {
                    _currentValue = Math.Round(_currentValue / settings.TickFrequency) * settings.TickFrequency;
                    _currentValue = Math.Max(settings.Minimum, Math.Min(settings.Maximum, _currentValue));
                }
                _valLabel.Text = _currentValue.ToString("0.##", CultureInfo.CurrentCulture);
                RaiseValueChanged(_currentValue);
            };

            container.Controls.Add(_trackBar);
            container.Controls.Add(_valLabel);

            InnerDialog.SetContentView(container);
            AddDefaultOptions();
        }

        public override decimal Value => _currentValue;

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => OnConfirmRequested();
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(_currentValue);
    }
}