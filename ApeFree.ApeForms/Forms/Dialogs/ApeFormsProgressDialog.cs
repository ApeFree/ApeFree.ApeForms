using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsProgressDialog : ApeFormsDialog<bool>, IProgressDialog
    {
        private readonly ProgressDialogSettings _settings;
        private readonly ProgressBar _progressBar;
        private readonly Label _labMessage;

        public ApeFormsProgressDialog(ProgressDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _settings = settings;

            var container = new Panel { Dock = DockStyle.Fill };

            _progressBar = new ProgressBar
            {
                Dock = DockStyle.Top,
                Height = 24,
                Style = settings.IsIndeterminate ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks,
                Value = (int)Math.Max(0, Math.Min(100, settings.InitialProgress * 100)),
            };

            _labMessage = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                Text = settings.InitialMessage ?? string.Empty,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            container.Controls.Add(_labMessage);
            container.Controls.Add(_progressBar);

            InnerDialog.SetContentView(container);
            AddDefaultOptions();
        }

        public bool IsIndeterminate => _settings.IsIndeterminate;

        public void UpdateProgress(double progress, string message = null)
        {
            if (InnerDialog == null || InnerDialog.IsDisposed)
            {
                return;
            }

            if (InnerDialog.InvokeRequired)
            {
                InnerDialog.BeginInvoke(new Action(() => UpdateProgress(progress, message)));
                return;
            }

            var clamped = Math.Max(0, Math.Min(1.0, progress));
            if (!_progressBar.IsDisposed && !IsIndeterminate)
            {
                _progressBar.Value = (int)(clamped * 100);
            }

            if (message != null && !_labMessage.IsDisposed)
            {
                _labMessage.Text = message;
            }

            if (clamped >= 1.0)
            {
                TrySubmit(true);
            }
        }
    }
}