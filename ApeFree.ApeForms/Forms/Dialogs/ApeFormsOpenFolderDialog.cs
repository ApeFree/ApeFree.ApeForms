using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Controls.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsOpenFolderDialog : ApeFormsDialog<IReadOnlyList<string>>
    {
        private readonly OpenFolderDialogSettings _settings;
        private readonly DriveBrowserView _view;

        public ApeFormsOpenFolderDialog(OpenFolderDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            _view = new DriveBrowserView
            {
                DisplayItemType = DisplayItemType.OnlyFolder,
                MultiSelect = settings.MultiSelect,
                Dock = DockStyle.Fill,
            };

            if (settings.Font.HasValue)
            {
                _view.Font = settings.Font.Value.ToFont();
            }

            InnerDialog.Size = new System.Drawing.Size(600, 400);
            InnerDialog.SetContentView(_view);

            if (!string.IsNullOrEmpty(settings.InitialPath) && Directory.Exists(settings.InitialPath))
            {
                InnerDialog.Shown += (_, _) => _view.OpenFolder(settings.InitialPath);
            }

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
            var folders = _view.SelectedFolders?.ToList() ?? new List<string>();
            if (folders.Count == 0 && !string.IsNullOrEmpty(_view.CurrentPath))
            {
                folders.Add(_view.CurrentPath);
            }

            if (folders.Count > 0)
            {
                TrySubmit(folders);
            }
            else
            {
                OnValidationFailed(ApeFree.ApeDialogs.Core.ValidationResult.Failure("Please select a folder."));
            }
        }
    }
}