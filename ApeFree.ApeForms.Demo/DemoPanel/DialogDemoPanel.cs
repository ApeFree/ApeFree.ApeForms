using ApeFree.ApeDialogs;
using ApeFree.ApeDialogs.Settings;
using ApeFree.ApeForms.Forms.Dialogs;
using ApeFree.ApeForms.Forms.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApeFree.ApeForms.Demo.DemoPanel
{
    public partial class DialogDemoPanel : UserControl
    {
        ApeFormsDialogProvider provider = DialogFactory.Factory.GetApeFormsDialogProvider();

        public DialogDemoPanel()
        {
            InitializeComponent();
        }

        private void btnMessageDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateMessageDialog(new MessageDialogSettings() { Title = tbTitle.Text, Content = tbContent.Text }, null);
            dialog.Show();
        }

        private void btnInputDialog_Click(object sender, EventArgs e)
        {
            var dialog = provider.CreateInputDialog(new InputDialogSettings()
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DefaultContent = "You can set the default text in the input box by using the 'DefaultContent' property.",
                ClearOptionText = "Clear(清空)",
                ConfirmOptionText = "Confirm(确定)",
                CancelOptionText = "Cancel(取消)",
            }, null);
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
            var dialog = provider.CreateInputDialog(new InputDialogSettings()
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                DefaultContent = "You can set the default text in the input box by using the 'DefaultContent' property.",
                IsMultiline = true
            }, null);
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
            var dialog = provider.CreatePasswordDialog(new PasswordDialogSettings()
            {
                Title = tbTitle.Text,
                Content = tbContent.Text + $"\r\n提示：密码至少要[6]位",
                PasswordChar = '●',
                PrecheckResult = password => password != null && password.Length >= 6,
            }, null);
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
            var dialog = provider.CreatePromptDialog(new PromptDialogSettings() { Title = tbTitle.Text, Content = tbContent.Text, PositiveOptionText = "Yes", NegativeOptionText = "No" }, null);
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
            };

            var dialog = provider.CreateSelectionDialog(new SelectionDialogSettings<Student>()
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                PrecheckResult = item =>
                {
                    var b = item != null;
                    if (!b)
                    {
                        Toast.Show("至少要选一项哦！", 2000, null, ToastMode.Reuse);
                    }
                    return b;
                },
                ItemDisplayTextConvertCallback = stu => $"{stu.Name} ({stu.Description})",
            }, students, null, null); ;
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
            };

            var dialog = provider.CreateMultipleSelectionDialog(new MultipleSelectionDialogSettings<Student>()
            {
                Title = tbTitle.Text,
                Content = tbContent.Text,
                PrecheckResult = item =>
                {
                    var b = item.Any();
                    if (!b)
                    {
                        Toast.Show("至少要选一项哦！", 2000, null, ToastMode.Reuse);
                    }
                    return b;
                },
                ItemDisplayTextConvertCallback = stu => $"{stu.Name} ({stu.Description})",
            }, students, null, null); ;
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
