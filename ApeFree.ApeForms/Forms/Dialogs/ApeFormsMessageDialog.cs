using ApeFree.ApeDialogs.Settings;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsMessageDialog : ApeFormsDialog<bool>
    {
        private readonly MessageDialogSettings _settings;

        public ApeFormsMessageDialog(MessageDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => TrySubmit(true);
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(true);
    }
}