using ApeFree.ApeDialogs.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsPasswordDialog : ApeFormsDialog<string>
    {
        private readonly PasswordDialogSettings _settings;
        private readonly TextBox _textBox;

        public ApeFormsPasswordDialog(PasswordDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _textBox = new TextBox
            {
                PasswordChar = settings.PasswordChar,
                Dock = DockStyle.Fill,
            };

            if (settings.Font.HasValue)
            {
                _textBox.Font = settings.Font.Value.ToFont();
            }

            _textBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    OnConfirmRequested();
                }
            };

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