using System;
using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 26);
            label1.Name = "label1";
            label1.Size = new Size(379, 24);
            label1.TabIndex = 0;
            label1.Text = "初雨的超级天文软件下载器 构建版本 ver.1.0.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(27, 63);
            label2.Name = "label2";
            label2.Size = new Size(614, 24);
            label2.TabIndex = 1;
            label2.Text = "软件开源地址：https://github.com/Chuyuuuu/ChuyuSuperDownloader";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(27, 101);
            label3.Name = "label3";
            label3.Size = new Size(419, 24);
            label3.TabIndex = 2;
            label3.Text = "本软件调用aria2与wget        遵循GPL-3.0开源协议";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 160);
            label4.Name = "label4";
            label4.Size = new Size(64, 24);
            label4.TabIndex = 3;
            label4.Text = "鸣谢：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 195);
            label5.Name = "label5";
            label5.Size = new Size(409, 24);
            label5.TabIndex = 4;
            label5.Text = "aria2开源项目：https://github.com/aria2/aria2";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(28, 368);
            label6.Name = "label6";
            label6.Size = new Size(286, 24);
            label6.TabIndex = 5;
            label6.Text = "真境：https://blog.mclzyun.com";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(27, 264);
            label7.Name = "label7";
            label7.Size = new Size(491, 24);
            label7.TabIndex = 6;
            label7.Text = "Windows Update Blocker项目：https://www.sordum.org";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(27, 300);
            label8.Name = "label8";
            label8.Size = new Size(262, 24);
            label8.TabIndex = 7;
            label8.Text = "参与软件开发、测试的小伙伴：";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(27, 229);
            label9.Name = "label9";
            label9.Size = new Size(420, 24);
            label9.TabIndex = 8;
            label9.Text = "wget开源项目：https://github.com/mirror/wget";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(27, 334);
            label10.Name = "label10";
            label10.Size = new Size(289, 24);
            label10.TabIndex = 9;
            label10.Text = "萌雨社：https://main.moerain.cn";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(28, 405);
            label11.Name = "label11";
            label11.Size = new Size(273, 24);
            label11.TabIndex = 10;
            label11.Text = "@901，@卡卡叉，@Ruabeetu";
            // 
            // AboutForm
            // 
            ClientSize = new Size(656, 467);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AboutForm";
            Text = "关于";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
    }
}
