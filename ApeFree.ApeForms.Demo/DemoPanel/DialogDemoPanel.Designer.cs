namespace ApeFree.ApeForms.Demo.DemoPanel
{
    partial class DialogDemoPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnMessageDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnInputDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnInputMultiLineDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnPasswordDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnPromptDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnSelectionDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.btnMultipleSelectionDialog = new ApeFree.ApeForms.Core.Controls.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbContent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbTitle);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 93);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Basic Settings";
            // 
            // tbContent
            // 
            this.tbContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbContent.Location = new System.Drawing.Point(3, 62);
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(734, 21);
            this.tbContent.TabIndex = 3;
            this.tbContent.Text = "This is a Demo to demonstrate the Dialog functionality.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Content";
            // 
            // tbTitle
            // 
            this.tbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbTitle.Location = new System.Drawing.Point(3, 29);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(734, 21);
            this.tbTitle.TabIndex = 1;
            this.tbTitle.Text = "Dialog Demo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(740, 432);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dialogs";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnMessageDialog);
            this.flowLayoutPanel1.Controls.Add(this.btnInputDialog);
            this.flowLayoutPanel1.Controls.Add(this.btnInputMultiLineDialog);
            this.flowLayoutPanel1.Controls.Add(this.btnPasswordDialog);
            this.flowLayoutPanel1.Controls.Add(this.btnPromptDialog);
            this.flowLayoutPanel1.Controls.Add(this.btnSelectionDialog);
            this.flowLayoutPanel1.Controls.Add(this.btnMultipleSelectionDialog);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(734, 412);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // btnMessageDialog
            // 
            this.btnMessageDialog.AutoSize = true;
            this.btnMessageDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnMessageDialog.ForeColor = System.Drawing.Color.White;
            this.btnMessageDialog.Location = new System.Drawing.Point(3, 3);
            this.btnMessageDialog.Name = "btnMessageDialog";
            this.btnMessageDialog.Size = new System.Drawing.Size(153, 37);
            this.btnMessageDialog.TabIndex = 0;
            this.btnMessageDialog.Title = "MessageDialog";
            this.btnMessageDialog.Click += new System.EventHandler(this.btnMessageDialog_Click);
            // 
            // btnInputDialog
            // 
            this.btnInputDialog.AutoSize = true;
            this.btnInputDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnInputDialog.ForeColor = System.Drawing.Color.White;
            this.btnInputDialog.Location = new System.Drawing.Point(162, 3);
            this.btnInputDialog.Name = "btnInputDialog";
            this.btnInputDialog.Size = new System.Drawing.Size(153, 37);
            this.btnInputDialog.TabIndex = 1;
            this.btnInputDialog.Title = "InputDialog";
            this.btnInputDialog.Click += new System.EventHandler(this.btnInputDialog_Click);
            // 
            // btnInputMultiLineDialog
            // 
            this.btnInputMultiLineDialog.AutoSize = true;
            this.btnInputMultiLineDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnInputMultiLineDialog.ForeColor = System.Drawing.Color.White;
            this.btnInputMultiLineDialog.Location = new System.Drawing.Point(321, 3);
            this.btnInputMultiLineDialog.Name = "btnInputMultiLineDialog";
            this.btnInputMultiLineDialog.Size = new System.Drawing.Size(153, 37);
            this.btnInputMultiLineDialog.TabIndex = 2;
            this.btnInputMultiLineDialog.Title = "InputDialog(MultiLine)";
            this.btnInputMultiLineDialog.Click += new System.EventHandler(this.btnInputMultiLineDialog_Click);
            // 
            // btnPasswordDialog
            // 
            this.btnPasswordDialog.AutoSize = true;
            this.btnPasswordDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnPasswordDialog.ForeColor = System.Drawing.Color.White;
            this.btnPasswordDialog.Location = new System.Drawing.Point(480, 3);
            this.btnPasswordDialog.Name = "btnPasswordDialog";
            this.btnPasswordDialog.Size = new System.Drawing.Size(153, 37);
            this.btnPasswordDialog.TabIndex = 3;
            this.btnPasswordDialog.Title = "PasswordDialog";
            this.btnPasswordDialog.Click += new System.EventHandler(this.btnPasswordDialog_Click);
            // 
            // btnPromptDialog
            // 
            this.btnPromptDialog.AutoSize = true;
            this.btnPromptDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnPromptDialog.ForeColor = System.Drawing.Color.White;
            this.btnPromptDialog.Location = new System.Drawing.Point(3, 46);
            this.btnPromptDialog.Name = "btnPromptDialog";
            this.btnPromptDialog.Size = new System.Drawing.Size(153, 37);
            this.btnPromptDialog.TabIndex = 4;
            this.btnPromptDialog.Title = "PromptDialog";
            this.btnPromptDialog.Click += new System.EventHandler(this.btnPromptDialog_Click);
            // 
            // btnSelectionDialog
            // 
            this.btnSelectionDialog.AutoSize = true;
            this.btnSelectionDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSelectionDialog.ForeColor = System.Drawing.Color.White;
            this.btnSelectionDialog.Location = new System.Drawing.Point(162, 46);
            this.btnSelectionDialog.Name = "btnSelectionDialog";
            this.btnSelectionDialog.Size = new System.Drawing.Size(153, 37);
            this.btnSelectionDialog.TabIndex = 5;
            this.btnSelectionDialog.Title = "SelectionDialog";
            this.btnSelectionDialog.Click += new System.EventHandler(this.btnSelectionDialog_Click);
            // 
            // btnMultipleSelectionDialog
            // 
            this.btnMultipleSelectionDialog.AutoSize = true;
            this.btnMultipleSelectionDialog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnMultipleSelectionDialog.ForeColor = System.Drawing.Color.White;
            this.btnMultipleSelectionDialog.Location = new System.Drawing.Point(321, 46);
            this.btnMultipleSelectionDialog.Name = "btnMultipleSelectionDialog";
            this.btnMultipleSelectionDialog.Size = new System.Drawing.Size(153, 37);
            this.btnMultipleSelectionDialog.TabIndex = 6;
            this.btnMultipleSelectionDialog.Title = "MultipleSelectionDialog";
            this.btnMultipleSelectionDialog.Click += new System.EventHandler(this.btnMultipleSelectionDialog_Click);
            // 
            // DialogDemoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DialogDemoPanel";
            this.Size = new System.Drawing.Size(740, 525);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Core.Controls.SimpleButton btnMessageDialog;
        private Core.Controls.SimpleButton btnInputDialog;
        private Core.Controls.SimpleButton btnInputMultiLineDialog;
        private Core.Controls.SimpleButton btnPasswordDialog;
        private Core.Controls.SimpleButton btnPromptDialog;
        private Core.Controls.SimpleButton btnSelectionDialog;
        private Core.Controls.SimpleButton btnMultipleSelectionDialog;
    }
}
