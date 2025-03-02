using System;
using System.Diagnostics;
using System.Drawing;
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
            linkLabel2 = new LinkLabel();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            linkLabel5 = new LinkLabel();
            label7 = new Label();
            linkLabel7 = new LinkLabel();
            label9 = new Label();
            linkLabel9 = new LinkLabel();
            label12 = new Label();
            linkLabel12 = new LinkLabel();
            label8 = new Label();
            linkLabel10 = new LinkLabel();
            linkLabel6 = new LinkLabel();
            label11 = new Label();
            label6 = new Label();
            label10 = new Label();
            label13 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 30);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(379, 24);
            label1.TabIndex = 0;
            label1.Text = "初雨的超级天文软件下载器 构建版本 ver.1.2.0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 72);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(136, 24);
            label2.TabIndex = 1;
            label2.Text = "软件开源地址：";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new Point(159, 72);
            linkLabel2.Margin = new Padding(4, 0, 4, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(488, 24);
            linkLabel2.TabIndex = 2;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "https://github.com/Chuyuuuu/ChuyuSuperDownloader";
            linkLabel2.LinkClicked += LinkLabel_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(33, 115);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(419, 24);
            label3.TabIndex = 3;
            label3.Text = "本软件调用aria2与wget        遵循GPL-3.0开源协议";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 183);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(64, 24);
            label4.TabIndex = 4;
            label4.Text = "鸣谢：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 223);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(143, 24);
            label5.TabIndex = 5;
            label5.Text = "aria2开源项目：";
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Location = new Point(159, 223);
            linkLabel5.Margin = new Padding(4, 0, 4, 0);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new Size(276, 24);
            linkLabel5.TabIndex = 6;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "https://github.com/aria2/aria2";
            linkLabel5.LinkClicked += LinkLabel_LinkClicked;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(33, 302);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(244, 24);
            label7.TabIndex = 9;
            label7.Text = "Windows Update Blocker：";
            // 
            // linkLabel7
            // 
            linkLabel7.AutoSize = true;
            linkLabel7.Location = new Point(260, 302);
            linkLabel7.Margin = new Padding(4, 0, 4, 0);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new Size(221, 24);
            linkLabel7.TabIndex = 10;
            linkLabel7.TabStop = true;
            linkLabel7.Text = "https://www.sordum.org";
            linkLabel7.LinkClicked += LinkLabel_LinkClicked;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(33, 262);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(143, 24);
            label9.TabIndex = 7;
            label9.Text = "wget开源项目：";
            // 
            // linkLabel9
            // 
            linkLabel9.AutoSize = true;
            linkLabel9.Location = new Point(159, 262);
            linkLabel9.Margin = new Padding(4, 0, 4, 0);
            linkLabel9.Name = "linkLabel9";
            linkLabel9.Size = new Size(287, 24);
            linkLabel9.TabIndex = 8;
            linkLabel9.TabStop = true;
            linkLabel9.Text = "https://github.com/mirror/wget";
            linkLabel9.LinkClicked += LinkLabel_LinkClicked;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(33, 342);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(220, 24);
            label12.TabIndex = 11;
            label12.Text = "支持PixInsight正版软件：";
            // 
            // linkLabel12
            // 
            linkLabel12.AutoSize = true;
            linkLabel12.Location = new Point(237, 342);
            linkLabel12.Margin = new Padding(4, 0, 4, 0);
            linkLabel12.Name = "linkLabel12";
            linkLabel12.Size = new Size(209, 24);
            linkLabel12.TabIndex = 12;
            linkLabel12.TabStop = true;
            linkLabel12.Text = "https://pixinsight.com/";
            linkLabel12.LinkClicked += LinkLabel_LinkClicked;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(33, 382);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(316, 24);
            label8.TabIndex = 13;
            label8.Text = "参与软件开发、测试与建议的小伙伴：";
            // 
            // linkLabel10
            // 
            linkLabel10.AutoSize = true;
            linkLabel10.Location = new Point(118, 422);
            linkLabel10.Margin = new Padding(4, 0, 4, 0);
            linkLabel10.Name = "linkLabel10";
            linkLabel10.Size = new Size(217, 24);
            linkLabel10.TabIndex = 14;
            linkLabel10.TabStop = true;
            linkLabel10.Text = "https://main.moerain.cn";
            linkLabel10.LinkClicked += LinkLabel_LinkClicked;
            // 
            // linkLabel6
            // 
            linkLabel6.AutoSize = true;
            linkLabel6.Location = new Point(118, 460);
            linkLabel6.Margin = new Padding(4, 0, 4, 0);
            linkLabel6.Name = "linkLabel6";
            linkLabel6.Size = new Size(232, 24);
            linkLabel6.TabIndex = 15;
            linkLabel6.TabStop = true;
            linkLabel6.Text = "https://blog.mclzyun.com";
            linkLabel6.LinkClicked += LinkLabel_LinkClicked;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(34, 498);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(349, 24);
            label11.TabIndex = 16;
            label11.Text = "@901，@卡卡叉，@Ruabeetu，@exist";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(34, 460);
            label6.Name = "label6";
            label6.Size = new Size(64, 24);
            label6.TabIndex = 17;
            label6.Text = "真境：";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(34, 422);
            label10.Name = "label10";
            label10.Size = new Size(82, 24);
            label10.TabIndex = 18;
            label10.Text = "萌雨社：";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.Navy;
            label13.Location = new Point(33, 542);
            label13.Name = "label13";
            label13.Size = new Size(244, 24);
            label13.TabIndex = 19;
            label13.Text = "特别感谢：东华大学天文协会";
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(687, 605);
            Controls.Add(label13);
            Controls.Add(label10);
            Controls.Add(label6);
            Controls.Add(label11);
            Controls.Add(linkLabel6);
            Controls.Add(linkLabel10);
            Controls.Add(label8);
            Controls.Add(linkLabel12);
            Controls.Add(label12);
            Controls.Add(linkLabel7);
            Controls.Add(label7);
            Controls.Add(linkLabel9);
            Controls.Add(label9);
            Controls.Add(linkLabel5);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(linkLabel2);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "AboutForm";
            Text = "关于";
            ResumeLayout(false);
            PerformLayout();
        }

        // 通用的链接点击事件处理方法
        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is LinkLabel linkLabel)
            {
                string url = linkLabel.Text;
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"无法打开链接：{url}\n错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 控件声明
        private Label label1;
        private Label label2;
        private LinkLabel linkLabel2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LinkLabel linkLabel5;
        private Label label7;
        private LinkLabel linkLabel7;
        private Label label9;
        private LinkLabel linkLabel9;
        private Label label12;
        private LinkLabel linkLabel12;
        private Label label8;
        private LinkLabel linkLabel10;
        private LinkLabel linkLabel6;
        private Label label6;
        private Label label10;
        private Label label13;
        private Label label11;
    }
}
