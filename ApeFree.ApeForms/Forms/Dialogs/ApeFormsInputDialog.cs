using ApeFree.ApeDialogs.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsInputDialog : ApeFormsDialog<string>
    {
        private readonly InputDialogSettings _settings;
        private readonly TextBox _textBox;

        public ApeFormsInputDialog(InputDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _textBox = new TextBox
            {
                Multiline = settings.IsMultiline,
                MaxLength = settings.MaximumLength,
                Text = settings.DefaultText ?? string.Empty,
                Dock = DockStyle.Fill,
                ScrollBars = settings.IsMultiline ? ScrollBars.Vertical : ScrollBars.None,
            };

            if (settings.Font.HasValue)
            {
                _textBox.Font = settings.Font.Value.ToFont();
            }

            if (!settings.IsMultiline)
            {
                _textBox.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        OnConfirmRequested();
                    }
                };
            }

            InnerDialog.SetContentView(_textBox);
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => TrySubmit(_textBox.Text);
            }
            else if (ReferenceEquals(dialogOption, _settings.ClearOption))
            {
                button.Click += (_, _) =>
                {
                    _textBox.Clear();
                    _textBox.Focus();
                };
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(_textBox.Text);
    }
}