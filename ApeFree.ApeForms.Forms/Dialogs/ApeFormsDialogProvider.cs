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
    public class ApeFormsDialog<TResult> : BaseDialog<Control, Control, Control, TResult>
    {
        private readonly DialogSettings settings;

        public DialogForm InnerDialog { get; }
        public override string Title { get => InnerDialog.Text; set => InnerDialog.Text = value; }
        public override string Content { get => InnerDialog.Content; set => InnerDialog.Content = value; }

        public ApeFormsDialog(DialogSettings settings, Func<Control, TResult> extractResultFromViewHandler = null)
        {
            this.settings = settings;

            InnerDialog = new DialogForm();

            Title = settings.Title;
            Content = settings.Content;

            ExtractResultFromViewHandler = extractResultFromViewHandler;
        }

        protected override void DismissHandler()
        {
            InnerDialog.Close();
        }

        //protected override void SetOptions(IEnumerable<DialogOption> options)
        //{
        //    List<Button> buttons = new List<Button>();
        //    foreach (DialogOption option in options)
        //    {
        //        Button button = new Button();
        //        button.Text = option.Text;
        //        button.Enabled = option.Enable;

        //        switch (option.OptionType)
        //        {
        //            case DialogOptionType.Neutral:
        //                break;
        //            case DialogOptionType.Positive:
        //                button.BackColor = Color.DarkGreen;
        //                button.ForeColor = Color.White;
        //                break;
        //            case DialogOptionType.Negative:
        //                button.BackColor = Color.OrangeRed;
        //                button.ForeColor = Color.White;
        //                break;
        //            case DialogOptionType.Functional:
        //            case DialogOptionType.Special:
        //                button.BackColor = Color.White;
        //                button.ForeColor = Color.Black;
        //                break;
        //        }
        //        buttons.Add(button);
        //    }
        //    InnerDialog.SetOptions(buttons);
        //}

        protected override void ShowHandler()
        {
            InnerDialog.SetContentView(ContentView);
            InnerDialog.ShowDialog();
        }

        public override Control AddOption(string text, Action<IDialog, Control> onClick = null)
        {
            var option = new Button();
            option.Text = text;
            option.Click += (s, e) => onClick(this, option);

            InnerDialog.AddButton(option);

            return option;
        }

        public override void ClearOptions()
        {
            InnerDialog.ClearButtons();
        }
    }
    public class ApeFormsDialogProvider : DialogProvider<Control, Control>
    {
        public override IDialog<DateTime> CreateDateTimeDialog(DateTimeDialogSettings settings, Control context = null)
        {
            throw new NotImplementedException();
        }

        public override IDialog<string> CreateInputDialog(InputDialogSettings settings, Control context = null)
        {
            var view = new TextBox();

            var dialog = new ApeFormsDialog<string>(settings, v => v.Text);
            dialog.ContentView = view;
            dialog.AddOption(settings.ClearOptionText, (d, o) => view.Clear());
            dialog.AddOption(settings.ConfirmOptionText, (d, o) => { dialog.ExtractResultFromView(); d.Dismiss(); });
            dialog.AddOption(settings.CancelOptionText, (d, o) => d.Dismiss());

            return dialog;
        }

        public override IDialog<bool> CreateMessageDialog(MessageDialogSettings settings, Control context = null)
        {
            throw new NotImplementedException();
        }

        public override IDialog<IEnumerable<T>> CreateMultipleSelectionDialog<T>(MultipleSelectionDialogSettings settings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems, Control context = null)
        {
            throw new NotImplementedException();
        }

        public override IDialog<string> CreatePasswordDialog(PasswordDialogSettings settings, Control context = null)
        {
            throw new NotImplementedException();
        }

        public override IDialog<bool> CreatePromptDialog(PromptDialogSettings settings, Control context = null)
        {
            throw new NotImplementedException();
        }

        public override IDialog<T> CreateSelectionDialog<T>(SelectionDialogSettings settings, IEnumerable<T> collection, T defaultSelectedItem, Control context = null)
        {
            throw new NotImplementedException();
        }
    }
}