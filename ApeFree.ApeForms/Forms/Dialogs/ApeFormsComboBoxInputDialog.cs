using ApeFree.ApeDialogs.Settings;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsComboBoxInputDialog : ApeFormsDialog<string>
    {
        private readonly ComboBoxInputDialogSettings _settings;
        private readonly ComboBox _comboBox;

        public ApeFormsComboBoxInputDialog(ComboBoxInputDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _comboBox = new ComboBox
            {
                DropDownStyle = settings.IsEditable ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList,
                Dock = DockStyle.Top,
            };

            if (settings.Items != null)
            {
                foreach (var item in settings.Items)
                {
                    _comboBox.Items.Add(item);
                }
            }

            if (settings.IsEditable)
            {
                _comboBox.Text = settings.DefaultText ?? string.Empty;
            }
            else
            {
                var match = settings.Items?.FirstOrDefault(i => i == settings.DefaultText);
                if (match != null)
                {
                    _comboBox.SelectedItem = match;
                }
                else if (_comboBox.Items.Count > 0)
                {
                    _comboBox.SelectedIndex = 0;
                }
            }

            if (settings.Font.HasValue)
            {
                _comboBox.Font = settings.Font.Value.ToFont();
            }

            InnerDialog.SetContentView(_comboBox);
            AddDefaultOptions();
        }

        public override OptionButton AddOption(DialogOption dialogOption)
        {
            var button = base.AddOption(dialogOption);
            if (ReferenceEquals(dialogOption, _settings.ConfirmOption))
            {
                button.Click += (_, _) => OnConfirmRequested();
            }
            return button;
        }

        protected override void OnConfirmRequested() => TrySubmit(_comboBox.Text);
    }
}