using ApeFree.ApeDialogs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsSelectionDialog<T> : ApeFormsDialog<T>
    {
        private readonly SelectionDialogSettings<T> _settings;
        private readonly ListBox _listBox;
        private readonly List<T> _items;

        public ApeFormsSelectionDialog(SelectionDialogSettings<T> settings, IEnumerable<T> collection, T defaultSelectedItem = default, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            _items = collection?.ToList() ?? new List<T>();

            _listBox = new ListBox
            {
                Dock = DockStyle.Fill,
                IntegralHeight = false,
            };

            if (settings.Font.HasValue)
            {
                _listBox.Font = settings.Font.Value.ToFont();
            }

            int selectedIndex = -1;
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                var displayText = settings.ItemDisplayConverter != null
                    ? settings.ItemDisplayConverter(item)
                    : item?.ToString() ?? string.Empty;

                _listBox.Items.Add(displayText);

                if (Equals(item, defaultSelectedItem))
                {
                    selectedIndex = i;
                }
            }

            if (selectedIndex >= 0)
            {
                _listBox.SelectedIndex = selectedIndex;
            }

            _listBox.DoubleClick += (_, _) => OnConfirmRequested();

            InnerDialog.SetContentView(_listBox);
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

        protected override void OnConfirmRequested()
        {
            if (_listBox.SelectedIndex >= 0 && _listBox.SelectedIndex < _items.Count)
            {
                TrySubmit(_items[_listBox.SelectedIndex]);
            }
            else
            {
                TrySubmit(default);
            }
        }
    }
}