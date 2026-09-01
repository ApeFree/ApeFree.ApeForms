using ApeFree.ApeDialogs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsMultipleSelectionDialog<T> : ApeFormsDialog<IReadOnlyList<T>>
    {
        private readonly MultipleSelectionDialogSettings<T> _settings;
        private readonly CheckedListBox _checkedListBox;
        private readonly List<T> _items;

        public ApeFormsMultipleSelectionDialog(MultipleSelectionDialogSettings<T> settings, IEnumerable<T> collection, IEnumerable<T> defaultSelectedItems = null, Control context = null)
            : base(settings, context)
        {
            _settings = settings;
            _items = collection?.ToList() ?? new List<T>();

            _checkedListBox = new CheckedListBox
            {
                Dock = DockStyle.Fill,
                IntegralHeight = false,
                CheckOnClick = true,
            };

            if (settings.Font.HasValue)
            {
                _checkedListBox.Font = settings.Font.Value.ToFont();
            }

            var defaultSet = new HashSet<T>(defaultSelectedItems ?? Enumerable.Empty<T>());
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                var displayText = settings.ItemDisplayConverter != null
                    ? settings.ItemDisplayConverter(item)
                    : item?.ToString() ?? string.Empty;

                _checkedListBox.Items.Add(displayText, defaultSet.Contains(item));
            }

            InnerDialog.SetContentView(_checkedListBox);
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
            var result = new List<T>();
            for (int i = 0; i < _checkedListBox.Items.Count; i++)
            {
                if (_checkedListBox.GetItemChecked(i))
                {
                    result.Add(_items[i]);
                }
            }
            TrySubmit(result);
        }

        protected override void OnSelectAllRequested()
        {
            for (int i = 0; i < _checkedListBox.Items.Count; i++)
            {
                _checkedListBox.SetItemChecked(i, true);
            }
        }

        protected override void OnReverseSelectionRequested()
        {
            for (int i = 0; i < _checkedListBox.Items.Count; i++)
            {
                _checkedListBox.SetItemChecked(i, !_checkedListBox.GetItemChecked(i));
            }
        }
    }
}