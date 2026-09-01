using ApeFree.ApeDialogs.Settings;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsToastDialog : ApeFormsDialog<bool>
    {
        private readonly ToastDialogSettings _settings;

        public ApeFormsToastDialog(ToastDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            if (settings.IsClickable)
            {
                InnerDialog.Click += (_, _) => TrySubmit(true);
            }

            AddDefaultOptions();
        }

        protected override void OnConfirmRequested() => TrySubmit(true);
    }
}