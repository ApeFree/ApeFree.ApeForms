using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeDialogs.Theme;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsColorDialog : ApeFormsDialog<ApeColor>
    {
        private readonly ColorDialogSettings _settings;
        private ApeColor _selectedColor;
        private readonly Panel _pnlPreview;
        private readonly NumericUpDown _nudR;
        private readonly NumericUpDown _nudG;
        private readonly NumericUpDown _nudB;
        private readonly NumericUpDown _nudA;
        private readonly TextBox _tbHex;
        private bool _isUpdating;

        public ApeFormsColorDialog(ColorDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            _selectedColor = settings.DefaultColor;

            InnerDialog.Size = new Size(480, 320);

            var container = new Panel { Dock = DockStyle.Fill };

            var topPanel = new Panel { Dock = DockStyle.Top, Height = 80, Padding = new Padding(8) };
            _pnlPreview = new Panel
            {
                Size = new Size(64, 64),
                Location = new Point(8, 8),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = _selectedColor.ToColor(),
            };
            topPanel.Controls.Add(_pnlPreview);

            int x = 84;
            _nudR = CreateChannelNumeric("R:", ref x, topPanel, _selectedColor.R);
            _nudG = CreateChannelNumeric("G:", ref x, topPanel, _selectedColor.G);
            _nudB = CreateChannelNumeric("B:", ref x, topPanel, _selectedColor.B);

            if (settings.AllowAlpha)
            {
                _nudA = CreateChannelNumeric("A:", ref x, topPanel, _selectedColor.A);
            }

            var labHex = new Label { Text = "Hex:", Location = new Point(x, 12), AutoSize = true };
            _tbHex = new TextBox
            {
                Location = new Point(x, 32),
                Width = 70,
                Text = _selectedColor.ToString(),
            };
            _tbHex.TextChanged += (s, e) =>
            {
                if (_isUpdating) return;
                if (TryParseHex(_tbHex.Text, out var color))
                {
                    _selectedColor = color;
                    UpdateUI(fromHex: true);
                }
            };
            topPanel.Controls.Add(labHex);
            topPanel.Controls.Add(_tbHex);

            var presetPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(8),
            };

            if (settings.PresetColors != null && settings.PresetColors.Count > 0)
            {
                foreach (var preset in settings.PresetColors)
                {
                    var pBtn = new Button
                    {
                        Size = new Size(24, 24),
                        BackColor = preset.ToColor(),
                        FlatStyle = FlatStyle.Flat,
                        Margin = new Padding(2),
                    };
                    pBtn.FlatAppearance.BorderSize = 1;
                    pBtn.FlatAppearance.BorderColor = Color.LightGray;
                    pBtn.Click += (_, _) =>
                    {
                        _selectedColor = preset;
                        UpdateUI();
                    };
                    presetPanel.Controls.Add(pBtn);
                }
            }

            container.Controls.Add(presetPanel);
            container.Controls.Add(topPanel);

            InnerDialog.SetContentView(container);
            AddDefaultOptions();
        }

        private NumericUpDown CreateChannelNumeric(string label, ref int x, Panel parent, byte initialVal)
        {
            var lab = new Label { Text = label, Location = new Point(x, 12), AutoSize = true };
            var nud = new NumericUpDown
            {
                Minimum = 0,
                Maximum = 255,
                Value = initialVal,
                Location = new Point(x, 32),
                Width = 50,
            };
            nud.ValueChanged += (s, e) =>
            {
                if (_isUpdating) return;
                _selectedColor = new ApeColor(
                    _nudA != null ? (byte)_nudA.Value : (byte)255,
                    (byte)_nudR.Value,
                    (byte)_nudG.Value,
                    (byte)_nudB.Value
                );
                UpdateUI(fromNumeric: true);
            };
            parent.Controls.Add(lab);
            parent.Controls.Add(nud);
            x += 56;
            return nud;
        }

        private void UpdateUI(bool fromNumeric = false, bool fromHex = false)
        {
            _isUpdating = true;
            try
            {
                _pnlPreview.BackColor = _selectedColor.ToColor();
                if (!fromNumeric)
                {
                    _nudR.Value = _selectedColor.R;
                    _nudG.Value = _selectedColor.G;
                    _nudB.Value = _selectedColor.B;
                    if (_nudA != null) _nudA.Value = _selectedColor.A;
                }
                if (!fromHex)
                {
                    _tbHex.Text = _selectedColor.ToString();
                }
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private static bool TryParseHex(string text, out ApeColor color)
        {
            color = default;
            if (string.IsNullOrWhiteSpace(text)) return false;
            text = text.Trim().TrimStart('#');
            if (text.Length == 6 && int.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int rgb))
            {
                color = new ApeColor(255, (byte)((rgb >> 16) & 0xFF), (byte)((rgb >> 8) & 0xFF), (byte)(rgb & 0xFF));
                return true;
            }
            if (text.Length == 8 && uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint argb))
            {
                color = ApeColor.FromArgb((int)argb);
                return true;
            }
            return false;
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

        protected override void OnConfirmRequested() => TrySubmit(_selectedColor);
    }
}