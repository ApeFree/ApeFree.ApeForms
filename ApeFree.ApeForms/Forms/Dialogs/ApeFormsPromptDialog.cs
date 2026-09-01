using ApeFree.ApeDialogs.Settings;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsPromptDialog : ApeFormsDialog<bool>
    {
        private readonly PromptDialogSettings _settings;

        public ApeFormsPromptDialog(PromptDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.PositiveOption))
            {
                button.Click += (_, _) => TrySubmit(true);
            }
            else if (ReferenceEquals(dialogOption, _settings.NegativeOption))
            {
                button.Click += (_, _) => TrySubmit(false);
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(true);
    }
}