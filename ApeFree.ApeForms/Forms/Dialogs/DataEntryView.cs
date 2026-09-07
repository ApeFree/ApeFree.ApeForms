using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Controls.Buttons;
using ApeFree.ApeForms.Controls.Labels;
using ApeFree.ApeForms.Controls.ListBoxes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Forms.Dialogs
{
    [ToolboxItem(false)]
    public class DataEntryView : ControlListBox
    {
        private SheetField[] _fields;

        public DataEntryView()
        {
            Items.ItemRemoved += Items_ItemRemoved;
        }

        private void Items_ItemRemoved(object sender, ApeFree.ApeForms.Utils.ListItemsChangedEventArgs<Control> e)
        {
            e.Item?.Dispose();
        }

        public SheetField[] Fields
        {
            get => _fields;
            set
            {
                if (_fields != value)
                {
                    _fields = value;
                    RefreshFields();
                }
            }
        }

        private void RefreshFields()
        {
            Items.Clear();

            if (Fields == null)
            {
                return;
            }

            foreach (var field in Enumerable.Reverse(Fields))
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
                    case FieldType.PicturePath:
                        view = CreatePicturePathFieldView(field as PicturePathField);
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
                    case FieldType.ComboBox:
                        view = CreateComboBoxFieldView(field as ComboBoxField);
                        break;
                    case FieldType.DateTime:
                        view = CreateDateTimeFieldView(field as DateTimeField);
                        break;
                }

                if (view != null)
                {
                    Items.Add(view);
                }
            }
        }

        public Control GetFieldControlByTitle(string title)
        {
            return Items.FirstOrDefault(c => c.Text == title);
        }

        /// <summary>
        /// 字段标题字体
        /// </summary>
        public Font TitleFont => new Font(Font, FontStyle.Bold);

        /// <summary>
        /// 字段数据字体
        /// </summary>
        public Font ValueFont => Font;

        private GroupBox CreateGroupBox(SheetField field)
        {
            var gb = new GroupBox
            {
                Text = field.Title,
                Font = TitleFont,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(5),
            };
            gb.ControlAdded += (s, e) => e.Control.Font = ValueFont;
            return gb;
        }

        private Control CreateTextFieldView(TextField field)
        {
            var group = CreateGroupBox(field);
            var tb = new TextBox
            {
                Text = field.Value ?? string.Empty,
                MaxLength = field.MaximumLength,
                Multiline = field.IsMultiline,
                Dock = DockStyle.Top,
            };
            if (field.IsMultiline)
            {
                tb.Height = 60;
                tb.ScrollBars = ScrollBars.Vertical;
            }
            tb.TextChanged += (s, e) => field.Value = tb.Text;
            group.Controls.Add(tb);
            return group;
        }

        private Control CreatePasswordFieldView(PasswordField field)
        {
            var group = CreateGroupBox(field);
            var tb = new TextBox
            {
                Text = field.Value ?? string.Empty,
                MaxLength = field.MaximumLength,
                PasswordChar = field.PasswordChar,
                Dock = DockStyle.Top,
            };
            tb.TextChanged += (s, e) => field.Value = tb.Text;
            group.Controls.Add(tb);
            return group;
        }

        private Control CreateHexBytesFieldView(HexBytesField field)
        {
            var group = CreateGroupBox(field);
            var tb = new TextBox
            {
                Text = field.Value != null ? BitConverter.ToString(field.Value).Replace("-", " ") : string.Empty,
                MaxLength = field.MaximumLength,
                Multiline = field.IsMultiline,
                Dock = DockStyle.Top,
            };
            if (field.IsMultiline)
            {
                tb.Height = 60;
                tb.ScrollBars = ScrollBars.Vertical;
            }
            tb.TextChanged += (s, e) =>
            {
                try
                {
                    var clean = tb.Text.Replace(" ", "").Replace("-", "");
                    if (clean.Length % 2 == 0)
                    {
                        var bytes = new byte[clean.Length / 2];
                        for (int i = 0; i < bytes.Length; i++)
                        {
                            bytes[i] = byte.Parse(clean.Substring(i * 2, 2), NumberStyles.HexNumber);
                        }
                        field.Value = bytes;
                    }
                }
                catch
                {
                }
            };
            group.Controls.Add(tb);
            return group;
        }

        private Control CreateFilePathFieldView(FilePathField field)
        {
            var group = CreateGroupBox(field);

            var labPath = new TallerLabel
            {
                ForeColor = Color.DarkGray,
                Text = field.Value ?? string.Empty,
                Dock = DockStyle.Top,
            };

            var btnBrowse = new SimpleButton
            {
                Text = field.BrowseButtonText ?? "Browse...",
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Dock = DockStyle.Top,
                Height = 26,
            };

            btnBrowse.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "All files (*.*)|*.*";
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        labPath.Text = ofd.FileName;
                        field.Value = ofd.FileName;
                    }
                }
            };

            group.Controls.Add(labPath);
            group.Controls.Add(btnBrowse);
            return group;
        }

        private Control CreatePicturePathFieldView(PicturePathField field)
        {
            var group = CreateGroupBox(field);

            var pic = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Top,
                Height = 0,
            };

            var btnBrowse = new SimpleButton
            {
                Text = field.BrowseButtonText ?? "Browse Image...",
                BackColor = Color.WhiteSmoke,
                ForeColor = Color.Black,
                Dock = DockStyle.Top,
                Height = 26,
            };

            void UpdateImage(string path)
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    try
                    {
                        pic.ImageLocation = path;
                        using (var image = new Bitmap(path))
                        {
                            var width = btnBrowse.Width > 0 ? btnBrowse.Width : (group.Width > 0 ? group.Width : 380);
                            var height = Math.Min((int)((float)image.Height / image.Width * width), width);
                            pic.Height = height;
                        }
                    }
                    catch { }
                }
                else
                {
                    pic.Height = 0;
                }
            }

            btnBrowse.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All files (*.*)|*.*";
                    ofd.RestoreDirectory = true;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        UpdateImage(ofd.FileName);
                        field.Value = ofd.FileName;
                    }
                }
            };

            group.Controls.Add(pic);
            group.Controls.Add(btnBrowse);

            if (!string.IsNullOrEmpty(field.Value))
            {
                UpdateImage(field.Value);
            }

            return group;
        }

        private Control CreateNumberFieldView(NumberField field)
        {
            var group = CreateGroupBox(field);
            var nud = new NumericUpDown
            {
                DecimalPlaces = field.DecimalPlaces,
                Minimum = field.Minimum,
                Maximum = field.Maximum,
                Value = Math.Max(field.Minimum, Math.Min(field.Maximum, field.Value)),
                Dock = DockStyle.Top,
            };
            nud.ValueChanged += (s, e) => field.Value = nud.Value;
            group.Controls.Add(nud);
            return group;
        }

        private Control CreateSingleChoiceFieldView(SingleChoiceField field)
        {
            var group = CreateGroupBox(field);

            if (field.Items != null)
            {
                int itemHeight = Math.Max(20, ValueFont.Height + 5);

                foreach (var item in Enumerable.Reverse(field.Items))
                {
                    var rb = new RadioButton
                    {
                        Text = field.ItemDisplayConverter != null ? field.ItemDisplayConverter(item) : item?.ToString() ?? string.Empty,
                        Dock = DockStyle.Top,
                        Height = itemHeight,
                        Checked = Equals(item, field.Value),
                    };
                    rb.CheckedChanged += (s, e) =>
                    {
                        if (rb.Checked)
                        {
                            field.Value = item;
                        }
                    };
                    group.Controls.Add(rb);
                }
            }

            return group;
        }

        private Control CreateMultipleChoiceFieldView(MultipleChoiceField field)
        {
            var group = CreateGroupBox(field);

            if (field.Items != null)
            {
                int itemHeight = Math.Max(20, ValueFont.Height + 5);
                var selectedList = field.Value != null ? new List<object>(field.Value) : new List<object>();

                foreach (var item in Enumerable.Reverse(field.Items))
                {
                    var cb = new CheckBox
                    {
                        Text = field.ItemDisplayConverter != null ? field.ItemDisplayConverter(item) : item?.ToString() ?? string.Empty,
                        Dock = DockStyle.Top,
                        Height = itemHeight,
                        Checked = selectedList.Contains(item),
                    };
                    cb.CheckedChanged += (s, e) =>
                    {
                        if (cb.Checked)
                        {
                            if (!selectedList.Contains(item))
                            {
                                selectedList.Add(item);
                            }
                        }
                        else
                        {
                            selectedList.Remove(item);
                        }
                        field.Value = selectedList.ToArray();
                    };
                    group.Controls.Add(cb);
                }
            }

            return group;
        }

        private Control CreateComboBoxFieldView(ComboBoxField field)
        {
            var group = CreateGroupBox(field);
            var cb = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Top,
            };

            if (field.Items != null)
            {
                foreach (var item in field.Items)
                {
                    var text = field.ItemDisplayConverter != null ? field.ItemDisplayConverter(item) : item?.ToString() ?? string.Empty;
                    cb.Items.Add(text);
                }

                if (field.Value != null)
                {
                    int idx = ((IList<object>)field.Items).IndexOf(field.Value);
                    if (idx >= 0) cb.SelectedIndex = idx;
                }
                else if (cb.Items.Count > 0)
                {
                    cb.SelectedIndex = 0;
                    field.Value = field.Items[0];
                }
            }

            cb.SelectedIndexChanged += (s, e) =>
            {
                if (cb.SelectedIndex >= 0 && field.Items != null && cb.SelectedIndex < field.Items.Count)
                {
                    field.Value = field.Items[cb.SelectedIndex];
                }
            };

            group.Controls.Add(cb);
            return group;
        }

        private Control CreateDateTimeFieldView(DateTimeField field)
        {
            var group = CreateGroupBox(field);
            var dtp = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = !string.IsNullOrEmpty(field.DateTimeFormat) ? field.DateTimeFormat : "yyyy-MM-dd HH:mm:ss",
                Value = field.Value != default ? field.Value : DateTime.Now,
                Dock = DockStyle.Top,
            };
            dtp.ValueChanged += (s, e) => field.Value = dtp.Value;
            group.Controls.Add(dtp);
            return group;
        }
    }
}