using ApeFree.ApeDialogs.Settings;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsTextDialog : ApeFormsDialog<bool>
    {
        private readonly TextDialogSettings _settings;
        private readonly TextBox _textBox;

        public ApeFormsTextDialog(TextDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _textBox = new TextBox
            {
                Multiline = true,
                ReadOnly = settings.IsReadOnly,
                WordWrap = settings.WordWrap,
                ScrollBars = ScrollBars.Both,
                Text = settings.Text ?? string.Empty,
                Dock = DockStyle.Fill,
            };

            if (settings.Font.HasValue)
            {
                _textBox.Font = settings.Font.Value.ToFont();
            }

            InnerDialog.SetContentView(_textBox);
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.CloseOption))
            {
                button.Click += (_, _) => TrySubmit(true);
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(true);
    }
}