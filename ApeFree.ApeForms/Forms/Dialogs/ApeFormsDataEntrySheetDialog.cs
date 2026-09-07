using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    public class ApeFormsDataEntrySheetDialog : ApeFormsDialog<DataEntrySheet>
    {
        private readonly DataEntrySheet _sheet;
        private readonly DataEntrySheetDialogSettings _settings;
        private readonly DataEntryView _view;
        private readonly ErrorProvider _errorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError, BlinkRate = 100 };

        public ApeFormsDataEntrySheetDialog(DataEntrySheet sheet, DataEntrySheetDialogSettings settings, Control context = null)
            : base(settings, context)
        {
            _sheet = sheet ?? throw new ArgumentNullException(nameof(sheet));
            _settings = settings;

            _view = new DataEntryView
            {
                Fields = sheet.Fields.ToArray(),
            };

            if (settings.Font.HasValue)
            {
                _view.Font = settings.Font.Value.ToFont();
            }

            _errorProvider.ContainerControl = _view;

            InnerDialog.SetContentView(_view);
            InnerDialog.FormClosed += (_, _) => _errorProvider.Dispose();
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
            _errorProvider.Clear();

            foreach (var field in _sheet.Fields)
            {
                var vcr = field.Validate();
                if (!vcr.IsSuccess)
                {
                    var ctrl = _view.GetFieldControlByTitle(field.Title);
                    if (ctrl != null)
                    {
                        _errorProvider.SetIconAlignment(ctrl, ErrorIconAlignment.TopRight);
                        _errorProvider.SetIconPadding(ctrl, -20);
                        _errorProvider.SetError(ctrl, vcr.ErrorMessage);

                        // 将垂直滚动条滚动到校验失败的控件的位置
                        _view.ScrollControlIntoView(ctrl);
                        int targetY = Math.Max(0, ctrl.Top - _view.AutoScrollPosition.Y);
                        _view.AutoScrollPosition = new Point(0, targetY);
                        _view.PerformLayout();
                    }

                    if (InnerDialog != null && !InnerDialog.IsDisposed)
                    {
                        InnerDialog.Shake();
                        Toast.Show(vcr.ErrorMessage, 2000, InnerDialog, ToastMode.Reuse);
                    }

                    return;
                }
            }

            TrySubmit(_sheet);
        }
    }
}