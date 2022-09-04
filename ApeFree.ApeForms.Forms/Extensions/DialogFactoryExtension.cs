using ApeFree.ApeDialogs;
using ApeFree.ApeForms.Forms.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApeFree.ApeForms.Forms.Extensions
{
    public static class DialogFactoryExtension
    {
        public static ApeFormsDialogProvider GetApeFormsDialogProvider(this DialogFactory _)
        {
            return new ApeFormsDialogProvider();
        }
    }
}
