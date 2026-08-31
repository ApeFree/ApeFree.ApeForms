using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class DialogDemoPanel : UserControl
    {
        ApeFormsDialogProvider provider = DialogFactory.Factory.GetApeFormsDialogProvider();

        public DialogDemoPanel()
        {
            InitializeComponent();

            btnNeutralOptionColor.BackColor = provider.OptionTagColors[DialogOptionTag.Neutral];
            btnPositiveOptionColor.BackColor = provider.OptionTagColors[DialogOptionTag.Positive];
            btnNegativeOptionColor.BackColor = provider.OptionTagColors[DialogOptionTag.Negative];
            btnFunctionalOptionColor.BackColor = provider.OptionTagColors[DialogOptionTag.Functional];
            btnSpecialOptionColor.BackColor = provider.OptionTagColors[DialogOptionTag.Special];
        }

        private void btnMessageDialog_Click(object sender, EventArgs e)
        {
            // 写法一：
            //var settings = new MessageDialogSettings()
            //{
            //    // 标题文本
            //    Title = tbTitle.Text,
            //    // 消息文本
            //    Content = tbContent.Text,
            //};
            //settings.ConfirmOption.Text = "知道了";

            //var dialog = provider.CreateMessageDialog(settings);
            //dialog.Show();

            // 写法二（推荐）：
            var dialog = provider.CreateMessageDialog(s =>
            {
                // 是否可取消
                s.Cancelable = true;
                // 修改确认键文本
                s.ConfirmOption.Text = "知道了";
                // 修改Dialog窗体尺寸
                s.DialogSize = new Size(400, 200);
                // 修改标题名
                s.Title = tbTitle.Text;
                // 修改文本内容
                s.Content = tbContent.Text;
            });
            (dialog as ApeFormsDialog<bool>).InnerDialog.ForeColor = Color.IndianRed;
            dialog.Show();
        }

        private void btnInputDialog_Click(object sender, EventArgs e)
        {
            // 写法一：
            //var settings = new InputDialogSettings()
            //{
            //    // 标题文本
            //    Title = tbTitle.Text,
            //    // 消息文本
            //    Content = tbContent.Text,
            //    // 输入框默认文本
            //    DefaultText = "You can set the default text in the input box by using the 'DefaultText' property.",
            //    // 是否可取消
            //    Cancelable = false,
            //    // 是否允许输入为空
            //    AllowEmpty = false,
            //    // 输入文本最大长度
            //    MaximumLength = 50,
            //    // 输入文本最小长度
            //    MinimumLength = 6,
            //    // 是否多行
            //    IsMultiline = false,
            //};
            //// 确认按钮文本
            //settings.ConfirmOption.Text = "Confirm(确定)";
            //// 取消按钮文本
            //settings.CancelOption.Text = "Cancel(取消)";
            //// 清空按钮文本
            //settings.ClearOption.Text = "Clear(清空)";
            //var dialog = provider.CreateInputDialog(settings, null);

            // 写法二（推荐）：
            var dialog = provider.CreateInputDialog(s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 输入框默认文本
                s.DefaultText = "You can set the default text in the input box by using the 'DefaultText' property.";
                // 是否可取消
                s.Cancelable = true;
                // 输入文本最大长度
                s.MaximumLength = 200;
                // 是否多行
                s.IsMultiline = false;
                // 确认按钮文本
                s.ConfirmOption.Text = "Confirm(确定)";
                // 取消按钮文本
                s.CancelOption.Text = "Cancel(取消)";
                // 清空按钮文本
                s.ClearOption.Text = "Clear(清空)";
                // 提醒色
                s.ReminderColor = Color.IndianRed;
            });
            dialog.Show();

            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入内容为：{dialog.Result.Data}");
            }
        }

        private void btnInputMultiLineDialog_Click(object sender, EventArgs e)
        {
            // 写法一：
            //var dialog = provider.CreateInputDialog(new InputDialogSettings()
            //{
            //    Title = tbTitle.Text,
            //    Content = tbContent.Text,
            //    DefaultText = "You can set the default text in the input box by using the 'DefaultText' property.",
            //    IsMultiline = true
            //}, null);

            // 写法二（推荐）：
            var dialog = provider.CreateInputDialog(s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 输入框默认文本
                s.DefaultText = "You can set the default text in the input box by using the 'DefaultText' property.";
                // 是否可取消
                s.Cancelable = true;
                // 输入文本最大长度
                s.MaximumLength = 200;
                // 是否多行
                s.IsMultiline = true;
                // 确认按钮文本
                s.ConfirmOption.Text = "Confirm(确定)";
                // 取消按钮文本
                s.CancelOption.Text = "Cancel(取消)";
                // 清空按钮文本
                s.ClearOption.Text = "Clear(清空)";
            });

            dialog.Show();

            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入内容为：{dialog.Result.Data}");
            }
        }

        private void btnPasswordDialog_Click(object sender, EventArgs e)
        {
            // 写法一：
            //var dialog = provider.CreatePasswordDialog(new PasswordDialogSettings()
            //{
            //    Title = tbTitle.Text,
            //    Content = tbContent.Text + $"\r\n提示：密码至少要[6]位",
            //    PasswordChar = '●',
            //    PrecheckResult = password => password != null && password.Length >= 6,
            //}, null);

            // 写法二（推荐）：
            var dialog = provider.CreatePasswordDialog(s =>
            {
                s.Title = tbTitle.Text;
                s.Content = tbContent.Text;
                s.PasswordChar = '●';
                s.PrecheckResult = password => new FormatCheckResult(password != null && password.Length >= 6, "密码至少要[6]位");
            });

            dialog.Show();
            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消输入");
            }
            else
            {
                Toast.Show($"输入密码为：{dialog.Result.Data}");
            }
        }

        private void btnPromptDialog_Click(object sender, EventArgs e)
        {
            // 写法一：
            //var dialog = provider.CreatePromptDialog(new PromptDialogSettings() { Title = tbTitle.Text, Content = tbContent.Text }, null);

            // 写法二（推荐）：
            var dialog = provider.CreatePromptDialog(s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 积极选项文本
                s.PositiveOption.Text = "Yes";
                // 消极选项文本
                s.NegativeOption.Text = "No";
            }, null);

            dialog.Show();

            if (dialog.Result.Data)
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

            // 写法一：
            //var dialog = provider.CreateSelectionDialog(new SelectionDialogSettings<Student>()
            //{
            //    Title = tbTitle.Text,
            //    Content = tbContent.Text,
            //    PrecheckResult = item =>
            //    {
            //        var b = item != null;
            //        if (!b)
            //        {
            //            Toast.Show("至少要选一项哦！", 2000, null, ToastMode.Reuse);
            //        }
            //        return b;
            //    },
            //    ItemDisplayTextConvertCallback = stu => $"{stu.Name} ({stu.Description})",
            //}, students, null, null);

            // 写法二（推荐）：
            var dialog = provider.CreateSelectionDialog(students, null, s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 设置预检查回调
                s.PrecheckResult = item => new FormatCheckResult(item != null, "至少要选一项哦！");

                s.ItemDisplayTextConvertCallback = stu => $"{stu.Name} ({stu.Description})";
            });

            dialog.Show();
            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"结果：{dialog.Result.Data.Name} , {dialog.Result.Data.Description}");
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

            // 写法一：
            //var dialog = provider.CreateMultipleSelectionDialog(new MultipleSelectionDialogSettings<Student>()
            //{
            //    Title = tbTitle.Text,
            //    Content = tbContent.Text,
            //    PrecheckResult = item =>
            //    {
            //        var b = item.Any();
            //        if (!b)
            //        {
            //            Toast.Show("至少要选一项哦！", 2000, null, ToastMode.Reuse);
            //        }
            //        return b;
            //    },
            //    ItemDisplayTextConvertCallback = stu => $"{stu.Name} ({stu.Description})",
            //}, students, null, null);

            // 写法二（推荐）：
            var dialog = provider.CreateMultipleSelectionDialog(students, null, s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 设置预检查回调
                s.PrecheckResult = item => new FormatCheckResult(item.Any(), "至少要选一项哦！");

                s.ItemDisplayTextConvertCallback = stu => $"{stu.Name} ({stu.Description})";
            });


            dialog.Show();
            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"结果：{string.Join("|", dialog.Result.Data.Select(s => $"{s.Name}({s.Description})"))}");
            }
        }

        private void btnDataEntrySheetDialog_Click(object sender, EventArgs e)
        {
            // 创建表单
            DataEntrySheet sheet = new DataEntrySheet();
            sheet.AddTextField(new TextField()
            {
                Title = "姓名",
                Data = "张三",
            });
            sheet.AddPasswordField(new PasswordField()
            {
                Title = "密码",
                Data = "12345",
                ValidityCheckHandler = str =>
                {
                    if (str.Length < 6)
                    {
                        return new FormatCheckResult("密码至少6位");
                    }
                    return FormatCheckResult.Success;
                },
            });
            sheet.AddDateTimeField(new DateTimeField()
            {
                Title = "生日",
                Data = DateTime.Now
            });
            sheet.AddNumberField(new NumberField()
            {
                Title = "年龄",
                Data = 16,
                DecimalPlaces = 1,
                Maximum = 120,
                Minimum = 0,
            });
            sheet.AddPicturePathField(new PicturePathField()
            {
                Title = "照片",
                BrowseButtonText = "选择照片...",
            });
            sheet.AddFilePathField(new FilePathField()
            {
                Title = "附件",
                BrowseButtonText = "选择附件...",
            });
            sheet.AddComboBoxField(new ComboBoxField()
            {
                Title = "学历",
                Items = new[] { "高中及以下", "大专", "本科", "硕士研究生", "博士研究生" },
                Data = "高中及以下",
            });
            sheet.AddSingleChoiceField(new SingleChoiceField()
            {
                Title = "户籍",
                Items = new[] { "城市户口", "农村户口", "不确定" },
                Data = "不确定",
            });
            sheet.AddMultipleChoiceField(new MultipleChoiceField()
            {
                Title = "特长",
                Items = new[] { "唱歌", "跳舞", "打篮球", "铁山靠" },
                Data = new[] { "打篮球", "铁山靠" },
            });
            sheet.AddTextField(new TextField()
            {
                Title = "简介",
                Data = "一个平平无奇的高中生，喜欢唱、跳、rap、篮球。",
                IsMultiline = true
            });

            var dialog = provider.CreateDataEntrySheetDialog(sheet, s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 窗体尺寸
                s.DialogSize = new Size(450, 700);
                // 预校验提交结果
                s.PrecheckResult = sht =>
                {
                    // 可以在此处进行数据检查，也可以在字段属性里设置数据有效性检查的回调。
                    // 区别是：
                    // 1. 在此处进行检查需要从表单中查找到属性然后强转格式。但可以检查多个字段相关联的数据。
                    // 2. 在字段中设置检查回调可以直接拿到对应的数据类型，由于是设置在表单中，
                    //    因此这部分的设置可以跟随表单一起使用（避免同一个表单在不同地方使用都需要在Dialog中写一遍格式检查的回调）。

                    if (((string)sht["户籍"].Data) == "不确定")
                    {
                        return new FormatCheckResult("户籍类型不可选择“不确定”");
                    }

                    if (!((object[])sht["特长"].Data).Any())
                    {
                        return new FormatCheckResult("至少选择一个特长");
                    }

                    return FormatCheckResult.Success;
                };
            }, null);
            dialog.Show();
            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消输入", 2000, null, ToastMode.Reuse);
            }
            else
            {
                var lines = sheet.Fields.Select(p =>
                {
                    if (p.Data is object[] array)
                    {
                        return $"{p.Title}：\t{string.Join("、", array)}";
                    }
                    else
                    {
                        return $"{p.Title}：\t{p.Data}";
                    }
                });
                Toast.Show($"{lines.Join("\r\n")}", 2000, null, ToastMode.Reuse);

                // Tips: 读取单个字段的数据可以通过以下方法
                // 方法1：通过字段标题查找
                // var str1 = sheet["简介"].Data.ToString();
                // 方法2：通过字段序号查找
                // var str2 = sheet[9].Data.ToString();
            }
        }

        private void btnDateTimeDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateDateTimeDialog(s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = tbContent.Text;
                // 默认时间
                s.DefaultDateTime = new DateTime(2008, 8, 8);
                // 是否可取消
                s.Cancelable = true;
                // 确认按钮文本
                s.ConfirmOption.Text = "Confirm(确定)";
                // 取消按钮文本
                s.CancelOption.Text = "Cancel(取消)";
                // 清空按钮文本
                s.CurrentTimeOption.Text = "Now(当前)";
            });

            dialog.Show();

            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"选择时间为：{dialog.Result.Data}");
            }
        }


        private void btnOpenFileDialog_Click(object sender, EventArgs e)
        {
            var path = Application.ExecutablePath;

            var dialog = provider.CreateOpenFileDialog(path, s =>
            {
                // 搜索通配符
                s.SearchPattern = "*.exe";

                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = $"{tbContent.Text}\r\n当前设置的通配符({nameof(s.SearchPattern)})为:{s.SearchPattern}";

                // 是否支持多选
                s.MultiSelect = true;
                // 是否可取消
                s.Cancelable = true;
                // 确认按钮文本
                s.ConfirmOption.Text = "Confirm(确定)";
                // 取消按钮文本
                s.CancelOption.Text = "Cancel(取消)";
            });

            dialog.Show();

            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"当前选中的文件路径：\r\n{dialog.Result.Data.Join("\r\n")}");
            }
        }

        private void btnOpenFolderDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateOpenFolderDialog("C:/", s =>
            {
                // 标题文本
                s.Title = tbTitle.Text;
                // 消息文本
                s.Content = $"{tbContent.Text}";

                // 是否支持多选
                s.MultiSelect = true;
                // 是否可取消
                s.Cancelable = true;
                // 确认按钮文本
                s.ConfirmOption.Text = "Confirm(确定)";
                // 取消按钮文本
                s.CancelOption.Text = "Cancel(取消)";
            });

            dialog.Show();

            if (dialog.Result.IsCancel)
            {
                Toast.Show("取消选择");
            }
            else
            {
                Toast.Show($"当前选中的文件夹路径：\r\n{dialog.Result.Data.Join("\r\n")}");
            }
        }

        private void btnOptionColor_Click(object sender, EventArgs e)
        {
            var btn = sender as Control;
            var tagName = btn.Text;
            var ot = tagName.ToEnum<DialogOptionTag>();

            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = btn.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                btn.BackColor = MyDialog.Color;
                provider.OptionTagColors[ot] = btn.BackColor;
            }
        }

        class Student
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
