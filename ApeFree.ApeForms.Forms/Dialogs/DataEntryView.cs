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

            foreach (var field in Fields.Reverse())
            {
                Control view = null;

                switch (field.FieldType)
                {
                    case FieldType.Text:
                        view = CreateTextFieldView(field as TextField);
                        break;
                    case FieldType.Password:
                        view = CreatePasswordFieldView(field as PasswordField);
                        break;
                    case FieldType.HexBytes:
                        view = CreateHexBytesFieldView(field as HexBytesField);
                        break;
                    case FieldType.FilePath:
                        view = CreateFilePathFieldView(field as FilePathField);
                        break;
                    case FieldType.Number:
                        view = CreateNumberFieldView(field as NumberField);
                        break;
                    case FieldType.SingleChoice:
                        view = CreateSingleChoiceFieldView(field as SingleChoiceField);
                        break;
                    case FieldType.MultipleChoice:
                        view = CreateMultipleChoiceFieldView(field as MultipleChoiceField);
                        break;
                    case FieldType.DateTime:
                        view = CreateDateTimeFieldView(field as DateTimeField);
                        break;
                    case FieldType.PicturePath:
                        view = CreatePicturePathFieldView(field as PicturePathField);
                        break;
                    case FieldType.ComboBox:
                        view = CreateComboBoxFieldView(field as ComboBoxField);
                        break;
                    default:
                        continue;
                }

                Items.Add(view);
            }
        }

        public Control GetFieldControlByTitle(string title)
        {
            return Items.FirstOrDefault(c => c.Text == title);
        }

        private Control CreateComboBoxFieldView(ComboBoxField field)
        {
            var group = CreateGroupBox(field);
            var box = new ComboBox();
            box.DropDownStyle = ComboBoxStyle.DropDownList;
            box.Items.AddRange(field.Items.Select(item => field.ItemDisplayTextConvertHandler(item)).ToArray());
            box.SelectedValueChanged += (s, e) => field.Data = field.Items[box.SelectedIndex];
            box.Text = field.ItemDisplayTextConvertHandler(field.Data ?? field.Items.FirstOrDefault() ?? string.Empty);
            box.Dock = DockStyle.Top;
            box.Parent = group;

            return group;
        }

        private Control CreateDateTimeFieldView(DateTimeField field)
        {
            var group = CreateGroupBox(field);

            var dtp = new DateTimePicker();
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.CustomFormat = field.DateTimeFormat;
            dtp.Value = field.Data;
            dtp.ValueChanged += (s, e) => field.Data = dtp.Value;
            dtp.Dock = DockStyle.Top;
            dtp.Parent = group;

            return group;
        }

        private Control CreateMultipleChoiceFieldView(MultipleChoiceField field)
        {
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

        private Control CreateSingleChoiceFieldView(SingleChoiceField field)
        {
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

        private Control CreateNumberFieldView(NumberField field)
        {
            var group = CreateGroupBox(field);

            var nud = new NumericUpDown();
            nud.DecimalPlaces = field.DecimalPlaces;
            nud.Maximum = field.Maximum;
            nud.Minimum = field.Minimum;
            nud.Value = field.Data;
            nud.ValueChanged += (s, e) => field.Data = nud.Value;
            nud.Dock = DockStyle.Top;
            nud.Parent = group;

            return group;
        }

        private Control CreateFilePathFieldView(FilePathField field)
        {
            var group = CreateGroupBox(field);

            var labPath = new TallerLabel();
            labPath.ForeColor = Color.DarkGray;
            labPath.Text = field.Data;
            labPath.Dock = DockStyle.Top;
            labPath.Parent = group;

            var btnBrowse = new SimpleButton();
            btnBrowse.Text = field.BrowseButtonText;
            btnBrowse.BackColor = Color.WhiteSmoke;
            btnBrowse.ForeColor = Color.Black;
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
                        field.Data = openFileDialog.FileName;
                    }
                }
            };

            return group;
        }

        private Control CreatePicturePathFieldView(PicturePathField field)
        {
            var group = CreateGroupBox(field);

            var pic = new PictureBox();
            pic.Size = new Size();
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Dock = DockStyle.Top;
            pic.Parent = group;

            var btnBrowse = new SimpleButton();
            btnBrowse.Text = field.BrowseButtonText;
            btnBrowse.BackColor = Color.WhiteSmoke;
            btnBrowse.ForeColor = Color.Black;
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
                        try
                        {
                            pic.ImageLocation = openFileDialog.FileName;
                            using (Image image = new Bitmap(openFileDialog.FileName))
                            {
                                // 计算图片高度
                                var height = Math.Min((int)((float)image.Height / image.Width * btnBrowse.Width), btnBrowse.Width);

                                // 使用动画效果
                                pic.SizeGradualChange(new Size(btnBrowse.Width, height));

                                // 不使用动画效果
                                // pic.Height = Math.Min(height, btnBrowse.Width);
                            }
                            field.Data = openFileDialog.FileName;
                        }
                        catch (Exception) { }
                    }
                }
            };

            return group;
        }

        private Control CreateHexBytesFieldView(HexBytesField field)
        {
            var group = CreateGroupBox(field);

            var tbField = new TextBox();
            tbField.Text = field.Data.ToHexString();
            tbField.TextChanged += (s, e) => field.Data = tbField.Text.GetBytes();
            tbField.MaxLength = field.MaximumLength;
            tbField.Dock = DockStyle.Top;
            tbField.Parent = group;

            return group;
        }

        private Control CreatePasswordFieldView(PasswordField field)
        {
            var group = CreateGroupBox(field);

            var tbField = new TextBox();
            tbField.Text = field.Data;
            tbField.TextChanged += (s, e) => field.Data = tbField.Text;
            tbField.PasswordChar = field.PasswordChar;
            tbField.MaxLength = field.MaximumLength;
            tbField.Dock = DockStyle.Top;
            tbField.Parent = group;

            return group;
        }

        private Control CreateTextFieldView(TextField field)
        {
            var group = CreateGroupBox(field);

            var tbField = new TextBox();
            tbField.Text = field.Data;
            tbField.TextChanged += (s, e) => field.Data = tbField.Text;
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
