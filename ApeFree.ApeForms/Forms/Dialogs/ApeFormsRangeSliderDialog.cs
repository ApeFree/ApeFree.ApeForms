using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsRangeSliderDialog : ApeFormsAdjustDialogBase<ValueRange<decimal>>
    {
        private readonly RangeSliderDialogSettings _settings;
        private readonly NumericUpDown _nudStart;
        private readonly NumericUpDown _nudEnd;
        private readonly Label _valLabel;
        private ValueRange<decimal> _currentRange;

        public ApeFormsRangeSliderDialog(RangeSliderDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            _currentRange = new ValueRange<decimal>(settings.DefaultStart, settings.DefaultEnd);

            var container = new Panel { Dock = DockStyle.Fill };

            _valLabel = new Label
            {
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(SystemFonts.DefaultFont.FontFamily, 12, FontStyle.Bold),
                Height = 28,
                Visible = settings.ShowValueLabel,
                Text = $"{_currentRange.Start:0.##} ~ {_currentRange.End:0.##}",
            };

            var inputsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10),
            };

            var labStart = new Label { Text = "Start:", AutoSize = true, Padding = new Padding(0, 4, 0, 0) };
            _nudStart = new NumericUpDown
            {
                Minimum = settings.Minimum,
                Maximum = settings.Maximum,
                DecimalPlaces = 2,
                Value = Math.Max(settings.Minimum, Math.Min(settings.Maximum, _currentRange.Start)),
                Width = 100,
            };

            var labEnd = new Label { Text = "End:", AutoSize = true, Padding = new Padding(10, 4, 0, 0) };
            _nudEnd = new NumericUpDown
            {
                Minimum = settings.Minimum,
                Maximum = settings.Maximum,
                DecimalPlaces = 2,
                Value = Math.Max(settings.Minimum, Math.Min(settings.Maximum, _currentRange.End)),
                Width = 100,
            };

            _nudStart.ValueChanged += (s, e) =>
            {
                if (_nudStart.Value > _nudEnd.Value)
                {
                    _nudEnd.Value = _nudStart.Value;
                }
                UpdateRange();
            };

            _nudEnd.ValueChanged += (s, e) =>
            {
                if (_nudEnd.Value < _nudStart.Value)
                {
                    _nudStart.Value = _nudEnd.Value;
                }
                UpdateRange();
            };

            inputsPanel.Controls.Add(labStart);
            inputsPanel.Controls.Add(_nudStart);
            inputsPanel.Controls.Add(labEnd);
            inputsPanel.Controls.Add(_nudEnd);

            container.Controls.Add(inputsPanel);
            container.Controls.Add(_valLabel);

            InnerDialog.SetContentView(container);
            AddDefaultOptions();
        }

        private void UpdateRange()
        {
            _currentRange = new ValueRange<decimal>(_nudStart.Value, _nudEnd.Value);
            _valLabel.Text = $"{_currentRange.Start:0.##} ~ {_currentRange.End:0.##}";
            RaiseValueChanged(_currentRange);
        }

        public override ValueRange<decimal> Value => _currentRange;

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => OnConfirmRequested();
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(_currentRange);
    }
}