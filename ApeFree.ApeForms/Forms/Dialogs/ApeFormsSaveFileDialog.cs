using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Controls.Views;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsSaveFileDialog : ApeFormsDialog<string>
    {
        private readonly SaveFileDialogSettings _settings;
        private readonly DriveBrowserView _view;
        private readonly TextBox _tbFileName;

        public ApeFormsSaveFileDialog(SaveFileDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            var container = new Panel { Dock = DockStyle.Fill };

            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 36, Padding = new Padding(4) };
            var labName = new Label { Text = "File name:", AutoSize = true, Location = new Point(4, 8) };
            _tbFileName = new TextBox
            {
                Location = new Point(70, 6),
                Width = 350,
                Text = settings.DefaultFileName ?? string.Empty,
            };
            bottomPanel.Controls.Add(labName);
            bottomPanel.Controls.Add(_tbFileName);

            _view = new DriveBrowserView
            {
                DisplayItemType = DisplayItemType.FolderAndFile,
                SearchPattern = !string.IsNullOrEmpty(settings.SearchPattern) ? settings.SearchPattern : "*.*",
                Dock = DockStyle.Fill,
            };

            _view.OnSelectedItemsChanged += (s, e) =>
            {
                if (_view.SelectedFiles != null && _view.SelectedFiles.Length > 0)
                {
                    _tbFileName.Text = Path.GetFileName(_view.SelectedFiles[0]);
                }
            };

            if (settings.Font.HasValue)
            {
                _view.Font = settings.Font.Value.ToFont();
                _tbFileName.Font = settings.Font.Value.ToFont();
            }

            container.Controls.Add(_view);
            container.Controls.Add(bottomPanel);

            InnerDialog.Size = new Size(650, 450);
            InnerDialog.SetContentView(container);

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
            var dir = _view.CurrentPath;
            var fileName = _tbFileName.Text?.Trim();

            if (string.IsNullOrEmpty(fileName))
            {
                OnValidationFailed(ApeFree.ApeDialogs.Core.ValidationResult.Failure("Please enter a file name."));
                return;
            }

            var fullPath = Path.Combine(dir ?? Environment.CurrentDirectory, fileName);

            if (_settings.OverwritePrompt && File.Exists(fullPath))
            {
                if (MessageBox.Show($"File '{fileName}' already exists. Do you want to replace it?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            TrySubmit(fullPath);
        }
    }
}