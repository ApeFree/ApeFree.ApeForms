using ApeFree.ApeForms.Forms.Dialogs;

namespace ApeFree.ApeDialogs
{
    public static class DialogFactoryExtension
    {
        public static ApeFormsDialogProvider GetApeFormsDialogProvider(this DialogFactory _)
        {
            return new ApeFormsDialogProvider();
        }
    }
}
