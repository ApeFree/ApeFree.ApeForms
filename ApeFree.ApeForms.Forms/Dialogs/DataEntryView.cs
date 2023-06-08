using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Core.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    [ToolboxItem(false)]
    public class DataEntryView : ControlListBox
    {
        private SheetField[] fields;

        /// <summary>
        /// 字段标题字体
        /// </summary>
        private readonly static Font TitleFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold);

        /// <summary>
        /// 字段数据字体
        /// </summary>
        private readonly static Font ValueFont = SystemFonts.DefaultFont;

        public DataEntryView()
        {
            Items.ItemRemoved += Items_ItemRemoved;
        }

        private void Items_ItemRemoved(object sender, Core.Utils.ListItemsChangedEventArgs<Control> e)
        {
            e.Item.Dispose();
        }

        public SheetField[] Fields
        {
            get => fields;
            set
            {
                if (fields != value)
                {
                    fields = value;
                    RefreshFields();
                }
            }
        }

        private void RefreshFields()
        {
            Items.Clear();

            foreach (var field in Fields.Reverse<SheetField>())
            {
                Control view = null;

                switch (field.FieldType)
                {
                    case FieldType.Text:
                        view = CreateTextFieldView(field);
                        break;
                    case FieldType.Password:
                        view = CreatePasswordFieldView(field);
                        break;
                    case FieldType.HexBytes:
                        view = CreateHexBytesFieldView(field);
                        break;
                    case FieldType.FilePath:
                        view = CreateFilePathFieldView(field);
                        break;
                    case FieldType.Number:
                        view = CreateNumberFieldView(field);
                        break;
                    case FieldType.SingleChoice:
                        view = CreateSingleChoiceFieldView(field);
                        break;
                    case FieldType.MultipleChoice:
                        view = CreateMultipleChoiceFieldView(field);
                        break;
                    case FieldType.DateTime:
                        view = CreateDateTimeFieldView(field);
                        break;
                    default:
                        continue;
                }

                Items.Add(view);
            }
        }

        private Control CreateDateTimeFieldView(SheetField sheetField)
        {
            var field = sheetField as DateTimeField;
            var group = CreateGroupBox(field);

            var dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = field.DateTimeFormat;
            dtp.Value = field.Data;
            dtp.ValueChanged += (s, e) => sheetField.Data = dtp.Value;
            dtp.Dock = DockStyle.Top;
            dtp.Parent = group;

            return group;
        }

        private Control CreateMultipleChoiceFieldView(SheetField sheetField)
        {
            var field = sheetField as MultipleChoiceField;
            var group = CreateGroupBox(field);

            foreach (var item in field.Items.Reverse())
            {
                var btn = new CheckBox();
                btn.Text = field.ItemDisplayTextConvertHandler(item);
                btn.Dock = DockStyle.Top;
                btn.Parent = group;
                if (field.Data != null)
                {
                    btn.Checked = field.Data.Contains(item);
                }
                btn.CheckedChanged += (s, e) =>
                {
                    if ((s as CheckBox).Checked)
                    {
                        var list = field.Data.ToList();
                        list.Add(item);
                        field.Data = list.ToArray();
                    }
                    else
                    {
                        var list = field.Data.ToList();
                        list.Remove(item);
                        field.Data = list.ToArray();
                    }
                };
            }

            return group;
        }

        private Control CreateSingleChoiceFieldView(SheetField sheetField)
        {
            var field = sheetField as SingleChoiceField;
            var group = CreateGroupBox(field);

            foreach (var item in field.Items.Reverse())
            {
                var btn = new RadioButton();
                btn.CheckedChanged += (s, e) =>
                {
                    if ((s as RadioButton).Checked)
                    {
                        field.Data = item;
                    }
                };
                btn.Text = field.ItemDisplayTextConvertHandler(item);
                btn.Dock = DockStyle.Top;
                btn.Parent = group;
                btn.Checked = item == field.Data;
            }

            return group;
        }

        private Control CreateNumberFieldView(SheetField sheetField)
        {
            var field = sheetField as NumberField;
            var group = CreateGroupBox(field);

            var nud = new NumericUpDown();
            nud.DecimalPlaces = field.DecimalPlaces;
            nud.Maximum = field.Maximum;
            nud.Minimum = field.Minimum;
            nud.Value = field.Data;
            nud.ValueChanged += (s, e) => sheetField.Data = nud.Value;
            nud.Dock = DockStyle.Top;
            nud.Parent = group;

            return group;
        }

        private Control CreateFilePathFieldView(SheetField sheetField)
        {
            var field = sheetField as FilePathField;
            var group = CreateGroupBox(field);

            var labPath = new TallerLabel();
            labPath.ForeColor = Color.DarkGray;
            labPath.Text = field.Data;
            labPath.TextChanged += (s, e) => sheetField.Data = labPath.Text;
            labPath.Dock = DockStyle.Top;
            labPath.Parent = group;

            var btnBrowse = new SimpleButton();
            btnBrowse.Text = field.BrowseButtonText;
            btnBrowse.Dock = DockStyle.Top;
            btnBrowse.Parent = group;
            btnBrowse.Click += (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        labPath.Text = openFileDialog.FileName;
                    }
                }
            };

            return group;
        }

        private Control CreateHexBytesFieldView(SheetField sheetField)
        {
            var field = sheetField as HexBytesField;
            var group = CreateGroupBox(field);

            var tbField = new TextBox();
            tbField.Text = field.Data.ToHexString();
            tbField.TextChanged += (s, e) => sheetField.Data = tbField.Text.GetBytes();
            tbField.MaxLength = field.MaximumLength;
            tbField.Dock = DockStyle.Top;
            tbField.Parent = group;

            return group;
        }

        private Control CreatePasswordFieldView(SheetField sheetField)
        {
            var field = sheetField as PasswordField;
            var group = CreateGroupBox(field);

            var tbField = new TextBox();
            tbField.Text = field.Data;
            tbField.TextChanged += (s, e) => sheetField.Data = tbField.Text;
            tbField.PasswordChar = field.PasswordChar;
            tbField.MaxLength = field.MaximumLength;
            tbField.Dock = DockStyle.Top;
            tbField.Parent = group;

            return group;
        }

        private Control CreateTextFieldView(SheetField sheetField)
        {
            var field = sheetField as TextField;
            var group = CreateGroupBox(field);

            var tbField = new TextBox();
            tbField.Text = field.Data;
            tbField.TextChanged += (s, e) => sheetField.Data = tbField.Text;
            tbField.MaxLength = field.MaximumLength;
            tbField.Dock = DockStyle.Top;
            tbField.Parent = group;
            if (field.IsMultiline)
            {
                tbField.Multiline = true;
                tbField.Height = 80;
                tbField.ScrollBars = ScrollBars.Vertical;
            }

            return group;
        }

        private GroupBox CreateGroupBox(SheetField field)
        {
            var group = new GroupBox();
            group.Font = TitleFont;
            group.Text = field.Title;
            group.AutoSize = true;
            group.Padding = new Padding(5);
            group.ControlAdded += (s, e) => e.Control.Font = ValueFont;
            return group;
        }
    }
}
