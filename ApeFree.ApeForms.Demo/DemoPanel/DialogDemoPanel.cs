using ApeFree.ApeDialogs.Core;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeDialogs.Theme;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class DialogDemoPanel : UserControl
    {
        private readonly ApeFormsDialogProvider provider = new ApeFormsDialogProvider();

        public DialogDemoPanel()
        {
            InitializeComponent();

            btnNeutralOptionColor.BackColor = provider.Theme.GetColor(DialogOptionTag.Neutral).ToColor();
            btnPositiveOptionColor.BackColor = provider.Theme.GetColor(DialogOptionTag.Positive).ToColor();
            btnNegativeOptionColor.BackColor = provider.Theme.GetColor(DialogOptionTag.Negative).ToColor();
            btnFunctionalOptionColor.BackColor = provider.Theme.GetColor(DialogOptionTag.Functional).ToColor();
            btnSpecialOptionColor.BackColor = provider.Theme.GetColor(DialogOptionTag.Special).ToColor();
        }

        private void btnMessageDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateMessageDialog(new MessageDialogSettings
            {
                Cancelable = true,
                ConfirmOption = new DialogOption("知道了", DialogOptionTag.Positive),
                DialogSize = new ApeSize(400, 200),
                Title = tbTitle.Text,
                Content = tbContent.Text,
            });
            dialog.Show();
        }

        private void btnInputDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateInputDialog(new InputDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DefaultText = "You can set the default text in the input box by using the 'DefaultText' property.",
                Cancelable = true,
                MaximumLength = 200,
                IsMultiline = false,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入内容为：{result.Value}");
            }
        }

        private void btnInputMultiLineDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateInputDialog(new InputDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DefaultText = "You can set the default text in the input box by using the 'DefaultText' property.",
                Cancelable = true,
                MaximumLength = 200,
                IsMultiline = true,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入内容为：{result.Value}");
            }
        }

        private void btnPasswordDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreatePasswordDialog(new PasswordDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                PasswordChar = '●',
                Validator = password => password != null && password.Length >= 6
                    ? ValidationResult.Success
                    : ValidationResult.Failure("密码至少要[6]位"),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入密码为：{result.Value}");
            }
        }

        private void btnPromptDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreatePromptDialog(new PromptDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                PositiveOption = new DialogOption("Yes", DialogOptionTag.Positive),
                NegativeOption = new DialogOption("No", DialogOptionTag.Negative),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消操作");
            }
            else if (result.Value)
            {
                Toast.Show("结果：积极选项");
            }
            else
            {
                Toast.Show("结果：消极选项");
            }
        }

        private void btnSelectionDialog_Click(object sender, EventArgs e)
        {
            Student[] students = new Student[] {
                new Student("张三","一年级"),
                new Student("李四","三年级"),
                new Student("王二","五年级"),
                new Student("马六","五年级"),
            };

            var dialog = provider.CreateSelectionDialog(new SelectionDialogSettings<Student>
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                ItemDisplayConverter = stu => $"{stu.Name} ({stu.Description})",
                Validator = item => item != null
                    ? ValidationResult.Success
                    : ValidationResult.Failure("至少要选一项哦！"),
            }, students);

            var result = dialog.Show();
            if (result.IsCanceled || result.Value == null)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"结果：{result.Value.Name} , {result.Value.Description}");
            }
        }

        private void btnMultipleSelectionDialog_Click(object sender, EventArgs e)
        {
            Student[] students = new Student[] {
                new Student("张三","一年级"),
                new Student("李四","三年级"),
                new Student("王二","五年级"),
                new Student("马六","五年级"),
            };

            var dialog = provider.CreateMultipleSelectionDialog(new MultipleSelectionDialogSettings<Student>
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                ItemDisplayConverter = stu => $"{stu.Name} ({stu.Description})",
                Validator = items => items != null && items.Any()
                    ? ValidationResult.Success
                    : ValidationResult.Failure("至少要选一项哦！"),
            }, students);

            var result = dialog.Show();
            if (result.IsCanceled || result.Value == null)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"结果：{string.Join("|", result.Value.Select(s => $"{s.Name}({s.Description})"))}");
            }
        }

        private void btnDataEntrySheetDialog_Click(object sender, EventArgs e)
        {
            var sheet = new DataEntrySheet();
            sheet.AddField(new TextField
            {
                Title = "姓名",
                Value = "张三",
            });
            sheet.AddField(new PasswordField
            {
                Title = "密码",
                Value = "12345",
                Validator = str => str != null && str.Length >= 6
                    ? ValidationResult.Success
                    : ValidationResult.Failure("密码至少6位"),
            });
            sheet.AddField(new DateTimeField
            {
                Title = "生日",
                Value = DateTime.Now,
            });
            sheet.AddField(new NumberField
            {
                Title = "年龄",
                Value = 16,
                DecimalPlaces = 0,
                Maximum = 120,
                Minimum = 0,
            });
            sheet.AddField(new PicturePathField
            {
                Title = "照片",
                BrowseButtonText = "选择照片...",
            });
            sheet.AddField(new FilePathField
            {
                Title = "附件",
                BrowseButtonText = "选择附件...",
            });
            sheet.AddField(new ComboBoxField
            {
                Title = "学历",
                Items = new[] { "高中及以下", "大专", "本科", "硕士研究生", "博士研究生" },
                Value = "高中及以下",
            });
            sheet.AddField(new SingleChoiceField
            {
                Title = "户籍",
                Items = new[] { "城市户口", "农村户口", "不确定" },
                Value = "不确定",
                Validator = obj => (string)obj != "不确定"
                    ? ValidationResult.Success
                    : ValidationResult.Failure("户籍类型不可选择“不确定”"),
            });
            sheet.AddField(new MultipleChoiceField
            {
                Title = "特长",
                Items = new[] { "唱歌", "跳舞", "打篮球", "铁山靠" },
                Value = new object[] { "打篮球", "铁山靠" },
                Validator = items => items != null && items.Count > 0
                    ? ValidationResult.Success
                    : ValidationResult.Failure("至少选择一个特长"),
            });
            sheet.AddField(new TextField
            {
                Title = "简介",
                Value = "一个平平无奇的高中生，喜欢唱、跳、rap、篮球。",
                IsMultiline = true,
            });

            var dialog = provider.CreateDataEntrySheetDialog(sheet, new DataEntrySheetDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DialogSize = new ApeSize(450, 700),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消输入", 2000, null, ToastMode.Reuse);
            }
            else
            {
                var lines = sheet.Fields.Select(p =>
                {
                    if (p.GetRawValue() is object[] array)
                    {
                        return $"{p.Title}：\t{string.Join("、", array)}";
                    }
                    else
                    {
                        return $"{p.Title}：\t{p.GetRawValue()}";
                    }
                });
                Toast.Show(string.Join("\r\n", lines), 2000, null, ToastMode.Reuse);
            }
        }

        private void btnDateTimeDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateDateTimeDialog(new DateTimeDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DefaultDateTime = new DateTime(2008, 8, 8),
                Cancelable = true,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
                CurrentTimeOption = new DialogOption("Now(当前)", DialogOptionTag.Functional, Behavior: DialogOptionBehavior.SetCurrentTime),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"选择时间为：{result.Value}");
            }
        }

        private void btnComboBoxInputDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateComboBoxInputDialog(new ComboBoxInputDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                Items = new[] { "Option A", "Option B", "Option C", "Custom Item" },
                DefaultText = "Option A",
                IsEditable = true,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"选中/输入内容：{result.Value}");
            }
        }

        private void btnNumberInputDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateNumberInputDialog(new NumberInputDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                Minimum = 0,
                Maximum = 1000,
                DefaultValue = 100,
                DecimalPlaces = 2,
                Increment = 10,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入数值为：{result.Value}");
            }
        }

        private void btnSliderDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateSliderDialog(new SliderDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                Minimum = 0,
                Maximum = 100,
                DefaultValue = 50,
                TickFrequency = 5,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
                OnValueChanged = val => System.Diagnostics.Debug.WriteLine($"Slider: {val}"),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消调节");
            }
            else
            {
                Toast.Show($"最终滑块数值：{result.Value}");
            }
        }

        private void btnRangeSliderDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateRangeSliderDialog(new RangeSliderDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                Minimum = 0,
                Maximum = 100,
                DefaultStart = 20,
                DefaultEnd = 80,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
                OnValueChanged = range => System.Diagnostics.Debug.WriteLine($"Range: {range.Start} ~ {range.End}"),
            });

            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消调节");
            }
            else
            {
                Toast.Show($"最终范围：{result.Value.Start:0.##} ~ {result.Value.End:0.##}");
            }
        }

        private async void btnProgressDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateProgressDialog(new ProgressDialogSettings
            {
                Title = tbTitle.Text,
                Content = "正在处理任务，请稍候...",
                InitialProgress = 0,
                Cancelable = false,
            });

            var task = dialog.ShowAsync();

            for (int i = 1; i <= 10; i++)
            {
                await System.Threading.Tasks.Task.Delay(200);
                dialog.UpdateProgress(i / 10.0, $"正在执行第 {i * 10}% 步骤...");
            }

            await task;
            Toast.Show("任务执行完成！");
        }

        private void btnToastDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateToastDialog(new ToastDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                Duration = TimeSpan.FromSeconds(3),
            });
            dialog.ShowAsync();
        }

        private void btnTextDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateTextDialog(new TextDialogSettings
            {
                Title = tbTitle.Text,
                Content = "查看许可协议与使用说明",
                Text = "Apache License\nVersion 2.0, January 2004\nhttp://www.apache.org/licenses/\n\nTERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION\n\n1. Definitions.\n\n\"License\" shall mean the terms and conditions for use, reproduction, and distribution as defined by Sections 1 through 9 of this document.\n\n\"Licensor\" shall mean the copyright owner or entity authorized by the copyright owner that is granting the License.",
                IsReadOnly = true,
                Cancelable = true,
                CloseOption = new DialogOption("I Agree(同意)", DialogOptionTag.Positive),
            });
            dialog.Show();
        }

        private void btnColorDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateColorDialog(new ColorDialogSettings
            {
                Title = tbTitle.Text,
                Content = "请选择一种颜色：",
                DefaultColor = new ApeColor(255, 0, 122, 204),
                Cancelable = true,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
                PresetColors = new[]
                {
                    new ApeColor(255, 244, 67, 54),
                    new ApeColor(255, 233, 30, 99),
                    new ApeColor(255, 156, 39, 176),
                    new ApeColor(255, 103, 58, 183),
                    new ApeColor(255, 63, 81, 181),
                    new ApeColor(255, 33, 150, 243),
                    new ApeColor(255, 0, 188, 212),
                    new ApeColor(255, 0, 150, 136),
                    new ApeColor(255, 76, 175, 80),
                    new ApeColor(255, 139, 195, 74),
                    new ApeColor(255, 255, 235, 59),
                    new ApeColor(255, 255, 152, 0),
                    new ApeColor(255, 255, 87, 34),
                    new ApeColor(255, 121, 85, 72),
                    new ApeColor(255, 158, 158, 158),
                    new ApeColor(255, 96, 125, 139),
                },
            });
            var result = dialog.Show();
            if (result.IsCanceled)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"选中的颜色为：{result.Value}");
            }
        }

        private void btnSaveFileDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateSaveFileDialog(new SaveFileDialogSettings
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DefaultFileName = "Document.txt",
                SearchPattern = "*.*",
                InitialPath = Application.StartupPath,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled || string.IsNullOrEmpty(result.Value))
            {
                Toast.Show("取消保存");
            }
            else
            {
                Toast.Show($"保存路径为：{result.Value}");
            }
        }

        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateOpenFileDialog(new OpenFileDialogSettings
            {
                InitialPath = Application.StartupPath,
                SearchPattern = "*.exe",
                Title = tbTitle.Text,
                Content = $"{tbContent.Text}\r\n当前设置的通配符为: *.exe",
                MultiSelect = true,
                Cancelable = true,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled || result.Value == null)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"当前选中的文件路径：\r\n{string.Join("\r\n", result.Value)}");
            }
        }

        private void btnOpenFolderDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateOpenFolderDialog(new OpenFolderDialogSettings
            {
                InitialPath = "C:\\",
                Title = tbTitle.Text,
                Content = tbContent.Text,
                MultiSelect = true,
                Cancelable = true,
                ConfirmOption = new DialogOption("Confirm(确定)", DialogOptionTag.Positive),
                CancelOption = new DialogOption("Cancel(取消)", DialogOptionTag.Negative, Behavior: DialogOptionBehavior.Cancel),
            });

            var result = dialog.Show();
            if (result.IsCanceled || result.Value == null)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"当前选中的文件夹路径：\r\n{string.Join("\r\n", result.Value)}");
            }
        }

        private void btnOptionColor_Click(object sender, EventArgs e)
        {
            var btn = sender as Control;
            if (btn == null) return;

            using (var myDialog = new ColorDialog())
            {
                myDialog.AllowFullOpen = false;
                myDialog.ShowHelp = true;
                myDialog.Color = btn.BackColor;
                if (myDialog.ShowDialog() == DialogResult.OK)
                {
                    btn.BackColor = myDialog.Color;
                }
            }
        }

        private class Student
        {
            public Student(string name, string description)
            {
                Name = name;
                Description = description;
            }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}