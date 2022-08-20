using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Core.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsDialog<TResult> : BaseDialog<Control, Control, TResult>
    {
        private readonly DialogSettings settings;

        public DialogForm InnerDialog { get; }
        public override string Title { get => InnerDialog.Text; set => InnerDialog.Text = value; }
        public override string Content { get => InnerDialog.Content; set => InnerDialog.Content = value; }

        public ApeFormsDialog(DialogSettings settings)
        {
            this.settings = settings;

            InnerDialog = new DialogForm();
            Title = settings.Title;
            Content = settings.Content;

            var options = settings.GetOptions();
            SetOptions(options);
        }

        protected override void DismissHandler()
        {
            InnerDialog.Close();
        }

        protected override void SetOptions(IEnumerable<DialogOption> options)
        {
            List<Button> buttons = new List<Button>();
            foreach (DialogOption option in options)
            {
                Button button = new Button();
                button.Text = option.Text;
                button.Enabled = option.Enable;

                switch (option.OptionType)
                {
                    case DialogOptionType.Neutral:
                        break;
                    case DialogOptionType.Positive:
                        button.BackColor = Color.DarkGreen;
                        button.ForeColor = Color.White;
                        break;
                    case DialogOptionType.Negative:
                        button.BackColor = Color.OrangeRed;
                        button.ForeColor = Color.White;
                        break;
                    case DialogOptionType.Functional:
                    case DialogOptionType.Special:
                        button.BackColor = Color.White;
                        button.ForeColor = Color.Black;
                        break;
                }
                buttons.Add(button);
            }
            InnerDialog.SetOptions(buttons);
        }

        protected override void ShowHandler()
        {
            InnerDialog.SetContentView(ContentView);
            InnerDialog.ShowDialog();
        }
    }

    public class ApeFormsDialogProvider : DialogProvider<Control, Control>
    {
        public override BaseDialog<Control, Control, DateTime> CreateDateTimeDialog(DateTimeDialogSettings setings, Control context)
        {
            throw new NotImplementedException();
        }

        public override BaseDialog<Control, Control, string> CreateInputDialog(InputDialogSettings setings, Control context)
        {
            var dialog = new ApeFormsDialog<string>(setings);
            dialog.ContentView = new TextBox();
            return dialog;
        }

        public override BaseDialog<Control, Control, object> CreateMessageDialog(MessageDialogSettings setings, Control context)
        {
            throw new NotImplementedException();
        }

        public override BaseDialog<Control, Control, IEnumerable<T>> CreateMultipleSelectionDialog<T>(MultipleSelectionDialogSettings setings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems, Control context)
        {
            throw new NotImplementedException();
        }

        public override BaseDialog<Control, Control, string> CreatePasswordDialog(PasswordDialogSettings setings, Control context)
        {
            throw new NotImplementedException();
        }

        public override BaseDialog<Control, Control, bool> CreatePromptDialog(PromptDialogSettings setings, Control context)
        {
            throw new NotImplementedException();
        }

        public override BaseDialog<Control, Control, T> CreateSelectionDialog<T>(SelectionDialogSettings setings, IEnumerable<T> collection, T defaultSelectedItem, Control context)
        {
            throw new NotImplementedException();
        }
    }
}
