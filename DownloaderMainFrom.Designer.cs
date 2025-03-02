namespace AstronomySoftwareDownloader
{
    partial class DownloaderMainFrom
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnDownloadNINA = new Button();
            btnDownloadASCOM = new Button();
            btnDownloadPHD2 = new Button();
            btnDownloadSharpCap = new Button();
            btnDownloadQHYCCD = new Button();
            btnAbortDownload = new Button();
            lblStatus = new Label();
            lblAria2cVersion = new Label();
            txtOutput = new TextBox();
            progressBar = new ProgressBar();
            label1 = new Label();
            menuStrip1 = new MenuStrip();
            实用功能ToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            手动进行Aria2下载ToolStripMenuItem = new ToolStripMenuItem();
            编辑Aria2下载器配置ToolStripMenuItem = new ToolStripMenuItem();
            唤起NINA插件目录ToolStripMenuItem = new ToolStripMenuItem();
            唤起Siril插件目录ToolStripMenuItem = new ToolStripMenuItem();
            木星拍摄时长计算ToolStripMenuItem = new ToolStripMenuItem();
            天文摄影计算器ToolStripMenuItem = new ToolStripMenuItem();
            关于软件ToolStripMenuItem = new ToolStripMenuItem();
            label2 = new Label();
            btnDownloadASIASCOM = new Button();
            btnDownloadZWO_Camera_Driver = new Button();
            btnDownloadD80 = new Button();
            label3 = new Label();
            btnDownloadD50 = new Button();
            btnDownloadD20 = new Button();
            btnDownloadASTAP = new Button();
            btnDownloadTouptek = new Button();
            label4 = new Label();
            btnDownloadStellarium = new Button();
            btnDownloadKstars = new Button();
            btnDownloadtodesk = new Button();
            btnDownloadCanon = new Button();
            btnDownloadSONY = new Button();
            btnDownloadDSLRAscom = new Button();
            btnDownloadOnStep = new Button();
            btnDownloadG05 = new Button();
            label5 = new Label();
            btnDownloadGSServer = new Button();
            label6 = new Label();
            btnDownloadPIPP = new Button();
            btnDownloadWinJUPOS = new Button();
            btnDownloadAutoStakkert = new Button();
            btnDownloadRegistax = new Button();
            btnDownloadSiril = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnDownloadNINA
            // 
            btnDownloadNINA.Location = new Point(12, 63);
            btnDownloadNINA.Name = "btnDownloadNINA";
            btnDownloadNINA.Size = new Size(106, 40);
            btnDownloadNINA.TabIndex = 0;
            btnDownloadNINA.Text = "NINA深空";
            btnDownloadNINA.UseVisualStyleBackColor = true;
            btnDownloadNINA.Click += btnDownloadNINA_Click;
            // 
            // btnDownloadASCOM
            // 
            btnDownloadASCOM.Location = new Point(12, 141);
            btnDownloadASCOM.Name = "btnDownloadASCOM";
            btnDownloadASCOM.Size = new Size(129, 40);
            btnDownloadASCOM.TabIndex = 1;
            btnDownloadASCOM.Text = "ASCOM平台";
            btnDownloadASCOM.UseVisualStyleBackColor = true;
            btnDownloadASCOM.Click += btnDownloadASCOM_Click;
            // 
            // btnDownloadPHD2
            // 
            btnDownloadPHD2.Location = new Point(122, 63);
            btnDownloadPHD2.Name = "btnDownloadPHD2";
            btnDownloadPHD2.Size = new Size(104, 40);
            btnDownloadPHD2.TabIndex = 2;
            btnDownloadPHD2.Text = "PHD2导星";
            btnDownloadPHD2.UseVisualStyleBackColor = true;
            btnDownloadPHD2.Click += btnDownloadPHD2_Click;
            // 
            // btnDownloadSharpCap
            // 
            btnDownloadSharpCap.Location = new Point(232, 63);
            btnDownloadSharpCap.Name = "btnDownloadSharpCap";
            btnDownloadSharpCap.Size = new Size(177, 40);
            btnDownloadSharpCap.TabIndex = 3;
            btnDownloadSharpCap.Text = "SharpCap行星摄影";
            btnDownloadSharpCap.UseVisualStyleBackColor = true;
            btnDownloadSharpCap.Click += btnDownloadSharpCap_Click;
            // 
            // btnDownloadQHYCCD
            // 
            btnDownloadQHYCCD.Location = new Point(147, 141);
            btnDownloadQHYCCD.Name = "btnDownloadQHYCCD";
            btnDownloadQHYCCD.Size = new Size(134, 40);
            btnDownloadQHYCCD.TabIndex = 12;
            btnDownloadQHYCCD.Text = "QHYCCD驱动";
            btnDownloadQHYCCD.Click += btnDownloadQHYCCD_Click;
            // 
            // btnAbortDownload
            // 
            btnAbortDownload.Location = new Point(678, 429);
            btnAbortDownload.Name = "btnAbortDownload";
            btnAbortDownload.Size = new Size(120, 40);
            btnAbortDownload.TabIndex = 4;
            btnAbortDownload.Text = "中止任务";
            btnAbortDownload.UseVisualStyleBackColor = true;
            btnAbortDownload.Click += btnAbortDownload_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(216, 437);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 24);
            lblStatus.TabIndex = 5;
            // 
            // lblAria2cVersion
            // 
            lblAria2cVersion.AutoSize = true;
            lblAria2cVersion.Location = new Point(12, 409);
            lblAria2cVersion.Name = "lblAria2cVersion";
            lblAria2cVersion.Size = new Size(168, 24);
            lblAria2cVersion.TabIndex = 6;
            lblAria2cVersion.Text = "aria2c版本：1.37.0";
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(12, 486);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(786, 180);
            txtOutput.TabIndex = 7;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(12, 679);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(786, 23);
            progressBar.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 36);
            label1.Name = "label1";
            label1.Size = new Size(154, 24);
            label1.TabIndex = 9;
            label1.Text = "天文摄影实用程序";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 实用功能ToolStripMenuItem, 关于软件ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(810, 32);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // 实用功能ToolStripMenuItem
            // 
            实用功能ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, 手动进行Aria2下载ToolStripMenuItem, 编辑Aria2下载器配置ToolStripMenuItem, 唤起NINA插件目录ToolStripMenuItem, 唤起Siril插件目录ToolStripMenuItem, 木星拍摄时长计算ToolStripMenuItem, 天文摄影计算器ToolStripMenuItem });
            实用功能ToolStripMenuItem.Name = "实用功能ToolStripMenuItem";
            实用功能ToolStripMenuItem.Size = new Size(98, 28);
            实用功能ToolStripMenuItem.Text = "辅助功能";

            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(297, 34);
            toolStripMenuItem1.Text = "禁止Windows系统更新";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // 手动进行Aria2下载ToolStripMenuItem
            // 
            手动进行Aria2下载ToolStripMenuItem.Name = "手动进行Aria2下载ToolStripMenuItem";
            手动进行Aria2下载ToolStripMenuItem.Size = new Size(297, 34);
            手动进行Aria2下载ToolStripMenuItem.Text = "手动进行Aria2下载";
            手动进行Aria2下载ToolStripMenuItem.Click += 手动进行Aria2下载ToolStripMenuItem_Click;
            // 
            // 编辑Aria2下载器配置ToolStripMenuItem
            // 
            编辑Aria2下载器配置ToolStripMenuItem.Name = "编辑Aria2下载器配置ToolStripMenuItem";
            编辑Aria2下载器配置ToolStripMenuItem.Size = new Size(297, 34);
            编辑Aria2下载器配置ToolStripMenuItem.Text = "编辑Aria2下载器配置";
            编辑Aria2下载器配置ToolStripMenuItem.Click += 编辑Aria2下载器配置ToolStripMenuItem_Click;
            // 
            // 唤起NINA插件目录ToolStripMenuItem
            // 
            唤起NINA插件目录ToolStripMenuItem.Name = "唤起NINA插件目录ToolStripMenuItem";
            唤起NINA插件目录ToolStripMenuItem.Size = new Size(297, 34);
            唤起NINA插件目录ToolStripMenuItem.Text = "唤起NINA插件目录";
            唤起NINA插件目录ToolStripMenuItem.Click += 唤起NINA插件目录ToolStripMenuItem_Click;
            // 
            // 唤起Siril插件目录ToolStripMenuItem
            // 
            唤起Siril插件目录ToolStripMenuItem.Name = "唤起Siril插件目录ToolStripMenuItem";
            唤起Siril插件目录ToolStripMenuItem.Size = new Size(297, 34);
            唤起Siril插件目录ToolStripMenuItem.Text = "唤起Siril脚本目录";
            唤起Siril插件目录ToolStripMenuItem.Click += 唤起Siril插件目录ToolStripMenuItem_Click;
            // 
            // 木星拍摄时长计算ToolStripMenuItem
            // 
            木星拍摄时长计算ToolStripMenuItem.Name = "木星拍摄时长计算ToolStripMenuItem";
            木星拍摄时长计算ToolStripMenuItem.Size = new Size(297, 34);
            木星拍摄时长计算ToolStripMenuItem.Text = "木星拍摄时长计算";
            木星拍摄时长计算ToolStripMenuItem.Click += 木星拍摄时长计算ToolStripMenuItem_Click;
            // 
            // 天文摄影计算器ToolStripMenuItem
            // 
            天文摄影计算器ToolStripMenuItem.Name = "天文摄影计算器ToolStripMenuItem";
            天文摄影计算器ToolStripMenuItem.Size = new Size(297, 34);
            天文摄影计算器ToolStripMenuItem.Text = "天文摄影计算器";
            天文摄影计算器ToolStripMenuItem.Click += 天文摄影计算器ToolStripMenuItem_Click;
            // 
            // 关于软件ToolStripMenuItem
            // 
            关于软件ToolStripMenuItem.Name = "关于软件ToolStripMenuItem";
            关于软件ToolStripMenuItem.Size = new Size(98, 28);
            关于软件ToolStripMenuItem.Text = "关于软件";
            关于软件ToolStripMenuItem.Click += 关于软件ToolStripMenuItem_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 114);
            label2.Name = "label2";
            label2.Size = new Size(154, 24);
            label2.TabIndex = 11;
            label2.Text = "天文摄影相关驱动";
            // 
            // btnDownloadASIASCOM
            // 
            btnDownloadASIASCOM.Location = new Point(287, 141);
            btnDownloadASIASCOM.Name = "btnDownloadASIASCOM";
            btnDownloadASIASCOM.Size = new Size(177, 40);
            btnDownloadASIASCOM.TabIndex = 13;
            btnDownloadASIASCOM.Text = "ZWO ASCOM驱动";
            btnDownloadASIASCOM.UseVisualStyleBackColor = true;
            btnDownloadASIASCOM.Click += btnDownloadASIASCOM_Click;
            // 
            // btnDownloadZWO_Camera_Driver
            // 
            btnDownloadZWO_Camera_Driver.Location = new Point(470, 141);
            btnDownloadZWO_Camera_Driver.Name = "btnDownloadZWO_Camera_Driver";
            btnDownloadZWO_Camera_Driver.Size = new Size(173, 40);
            btnDownloadZWO_Camera_Driver.TabIndex = 14;
            btnDownloadZWO_Camera_Driver.Text = "ZWO CAM驱动";
            btnDownloadZWO_Camera_Driver.UseVisualStyleBackColor = true;
            btnDownloadZWO_Camera_Driver.Click += btnDownloadZWO_Camera_Driver_Click;
            // 
            // btnDownloadD80
            // 
            btnDownloadD80.Location = new Point(163, 265);
            btnDownloadD80.Name = "btnDownloadD80";
            btnDownloadD80.Size = new Size(154, 40);
            btnDownloadD80.TabIndex = 15;
            btnDownloadD80.Text = "D80";
            btnDownloadD80.UseVisualStyleBackColor = true;
            btnDownloadD80.Click += btnDownloadD80_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 238);
            label3.Name = "label3";
            label3.Size = new Size(175, 24);
            label3.TabIndex = 16;
            label3.Text = "ASTAP解析与数据库";
            // 
            // btnDownloadD50
            // 
            btnDownloadD50.Location = new Point(323, 265);
            btnDownloadD50.Name = "btnDownloadD50";
            btnDownloadD50.Size = new Size(152, 40);
            btnDownloadD50.TabIndex = 17;
            btnDownloadD50.Text = "D50";
            btnDownloadD50.UseVisualStyleBackColor = true;
            btnDownloadD50.Click += btnDownloadD50_Click;
            // 
            // btnDownloadD20
            // 
            btnDownloadD20.Location = new Point(481, 265);
            btnDownloadD20.Name = "btnDownloadD20";
            btnDownloadD20.Size = new Size(162, 40);
            btnDownloadD20.TabIndex = 18;
            btnDownloadD20.Text = "D20";
            btnDownloadD20.UseVisualStyleBackColor = true;
            btnDownloadD20.Click += btnDownloadD20_Click;
            // 
            // btnDownloadASTAP
            // 
            btnDownloadASTAP.Location = new Point(12, 265);
            btnDownloadASTAP.Name = "btnDownloadASTAP";
            btnDownloadASTAP.Size = new Size(145, 40);
            btnDownloadASTAP.TabIndex = 19;
            btnDownloadASTAP.Text = "ASTAP主程序";
            btnDownloadASTAP.UseVisualStyleBackColor = true;
            btnDownloadASTAP.Click += btnDownloadASTAP_Click;
            // 
            // btnDownloadTouptek
            // 
            btnDownloadTouptek.Location = new Point(649, 141);
            btnDownloadTouptek.Name = "btnDownloadTouptek";
            btnDownloadTouptek.Size = new Size(149, 40);
            btnDownloadTouptek.TabIndex = 20;
            btnDownloadTouptek.Text = "Touptek驱动";
            btnDownloadTouptek.UseVisualStyleBackColor = true;
            btnDownloadTouptek.Click += btnDownloadTouptek_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(414, 36);
            label4.Name = "label4";
            label4.Size = new Size(172, 24);
            label4.TabIndex = 21;
            label4.Text = "星图与远程控制程序";
            // 
            // btnDownloadStellarium
            // 
            btnDownloadStellarium.Location = new Point(498, 63);
            btnDownloadStellarium.Name = "btnDownloadStellarium";
            btnDownloadStellarium.Size = new Size(154, 40);
            btnDownloadStellarium.TabIndex = 22;
            btnDownloadStellarium.Text = "Stellarium QT6";
            btnDownloadStellarium.UseVisualStyleBackColor = true;
            btnDownloadStellarium.Click += btnDownloadStellarium_Click;
            // 
            // btnDownloadKstars
            // 
            btnDownloadKstars.Location = new Point(414, 63);
            btnDownloadKstars.Name = "btnDownloadKstars";
            btnDownloadKstars.Size = new Size(78, 40);
            btnDownloadKstars.TabIndex = 23;
            btnDownloadKstars.Text = "Kstars";
            btnDownloadKstars.UseVisualStyleBackColor = true;
            btnDownloadKstars.Click += btnDownloadKstars_Click;
            // 
            // btnDownloadtodesk
            // 
            btnDownloadtodesk.Location = new Point(658, 63);
            btnDownloadtodesk.Name = "btnDownloadtodesk";
            btnDownloadtodesk.Size = new Size(140, 40);
            btnDownloadtodesk.TabIndex = 24;
            btnDownloadtodesk.Text = "ToDesk远控";
            btnDownloadtodesk.UseVisualStyleBackColor = true;
            btnDownloadtodesk.Click += btnDownloadtodesk_Click;
            // 
            // btnDownloadCanon
            // 
            btnDownloadCanon.Location = new Point(12, 187);
            btnDownloadCanon.Name = "btnDownloadCanon";
            btnDownloadCanon.Size = new Size(129, 40);
            btnDownloadCanon.TabIndex = 25;
            btnDownloadCanon.Text = "佳能 EOS";
            btnDownloadCanon.UseVisualStyleBackColor = true;
            btnDownloadCanon.Click += btnDownloadCanon_Click;
            // 
            // btnDownloadSONY
            // 
            btnDownloadSONY.Location = new Point(147, 187);
            btnDownloadSONY.Name = "btnDownloadSONY";
            btnDownloadSONY.Size = new Size(129, 40);
            btnDownloadSONY.TabIndex = 26;
            btnDownloadSONY.Text = "索尼 IED";
            btnDownloadSONY.UseVisualStyleBackColor = true;
            btnDownloadSONY.Click += btnDownloadSONY_Click;
            // 
            // btnDownloadDSLRAscom
            // 
            btnDownloadDSLRAscom.Location = new Point(281, 187);
            btnDownloadDSLRAscom.Name = "btnDownloadDSLRAscom";
            btnDownloadDSLRAscom.Size = new Size(216, 40);
            btnDownloadDSLRAscom.TabIndex = 27;
            btnDownloadDSLRAscom.Text = "第三方ASCOM单反驱动";
            btnDownloadDSLRAscom.UseVisualStyleBackColor = true;
            btnDownloadDSLRAscom.Click += btnDownloadDSLRAscom_Click;
            // 
            // btnDownloadOnStep
            // 
            btnDownloadOnStep.Location = new Point(514, 187);
            btnDownloadOnStep.Name = "btnDownloadOnStep";
            btnDownloadOnStep.Size = new Size(129, 40);
            btnDownloadOnStep.TabIndex = 28;
            btnDownloadOnStep.Text = "ONSTEP";
            btnDownloadOnStep.UseVisualStyleBackColor = true;
            btnDownloadOnStep.Click += btnDownloadOnStep_Click;
            // 
            // btnDownloadG05
            // 
            btnDownloadG05.Location = new Point(649, 265);
            btnDownloadG05.Name = "btnDownloadG05";
            btnDownloadG05.Size = new Size(149, 40);
            btnDownloadG05.TabIndex = 29;
            btnDownloadG05.Text = "G05";
            btnDownloadG05.UseVisualStyleBackColor = true;
            btnDownloadG05.Click += btnDownloadG05_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 445);
            label5.Name = "label5";
            label5.Size = new Size(159, 24);
            label5.TabIndex = 30;
            label5.Text = "wget版本：1.21.4";
            // 
            // btnDownloadGSServer
            // 
            btnDownloadGSServer.Location = new Point(649, 187);
            btnDownloadGSServer.Name = "btnDownloadGSServer";
            btnDownloadGSServer.Size = new Size(149, 40);
            btnDownloadGSServer.TabIndex = 31;
            btnDownloadGSServer.Text = "GS Server";
            btnDownloadGSServer.UseVisualStyleBackColor = true;
            btnDownloadGSServer.Click += btnDownloadGSServer_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 318);
            label6.Name = "label6";
            label6.Size = new Size(418, 24);
            label6.TabIndex = 32;
            label6.Text = "深空与行星后期（不提供PixInsight，请支持正版）";
            // 
            // btnDownloadPIPP
            // 
            btnDownloadPIPP.Location = new Point(12, 345);
            btnDownloadPIPP.Name = "btnDownloadPIPP";
            btnDownloadPIPP.Size = new Size(111, 40);
            btnDownloadPIPP.TabIndex = 33;
            btnDownloadPIPP.Text = "PIPP稳像";
            btnDownloadPIPP.UseVisualStyleBackColor = true;
            btnDownloadPIPP.Click += btnDownloadPIPP_Click;
            // 
            // btnDownloadWinJUPOS
            // 
            btnDownloadWinJUPOS.Location = new Point(129, 345);
            btnDownloadWinJUPOS.Name = "btnDownloadWinJUPOS";
            btnDownloadWinJUPOS.Size = new Size(172, 40);
            btnDownloadWinJUPOS.TabIndex = 34;
            btnDownloadWinJUPOS.Text = "WINJupos修自转";
            btnDownloadWinJUPOS.UseVisualStyleBackColor = true;
            btnDownloadWinJUPOS.Click += btnDownloadWinJUPOS_Click;
            // 
            // btnDownloadAutoStakkert
            // 
            btnDownloadAutoStakkert.Location = new Point(307, 345);
            btnDownloadAutoStakkert.Name = "btnDownloadAutoStakkert";
            btnDownloadAutoStakkert.Size = new Size(185, 39);
            btnDownloadAutoStakkert.TabIndex = 35;
            btnDownloadAutoStakkert.Text = "AutoStakkert叠加";
            btnDownloadAutoStakkert.UseVisualStyleBackColor = true;
            btnDownloadAutoStakkert.Click += btnDownloadAutoStakkert_Click;
            // 
            // btnDownloadRegistax
            // 
            btnDownloadRegistax.Location = new Point(498, 346);
            btnDownloadRegistax.Name = "btnDownloadRegistax";
            btnDownloadRegistax.Size = new Size(145, 39);
            btnDownloadRegistax.TabIndex = 36;
            btnDownloadRegistax.Text = "Registax锐化";
            btnDownloadRegistax.UseVisualStyleBackColor = true;
            btnDownloadRegistax.Click += btnDownloadRegistax_Click;
            // 
            // btnDownloadSiril
            // 
            btnDownloadSiril.Location = new Point(649, 345);
            btnDownloadSiril.Name = "btnDownloadSiril";
            btnDownloadSiril.Size = new Size(149, 40);
            btnDownloadSiril.TabIndex = 37;
            btnDownloadSiril.Text = "Siril深空后期";
            btnDownloadSiril.UseVisualStyleBackColor = true;
            btnDownloadSiril.Click += btnDownloadSiril_Click;
            // 
            // DownloaderMainFrom
            // 
            ClientSize = new Size(810, 724);
            Controls.Add(btnDownloadSiril);
            Controls.Add(btnDownloadRegistax);
            Controls.Add(btnDownloadAutoStakkert);
            Controls.Add(btnDownloadWinJUPOS);
            Controls.Add(btnDownloadPIPP);
            Controls.Add(label6);
            Controls.Add(btnDownloadGSServer);
            Controls.Add(label5);
            Controls.Add(btnDownloadG05);
            Controls.Add(btnDownloadOnStep);
            Controls.Add(btnDownloadDSLRAscom);
            Controls.Add(btnDownloadSONY);
            Controls.Add(btnDownloadCanon);
            Controls.Add(btnDownloadtodesk);
            Controls.Add(btnDownloadKstars);
            Controls.Add(btnDownloadStellarium);
            Controls.Add(label4);
            Controls.Add(btnDownloadTouptek);
            Controls.Add(btnDownloadASTAP);
            Controls.Add(btnDownloadD20);
            Controls.Add(btnDownloadD50);
            Controls.Add(label3);
            Controls.Add(btnDownloadD80);
            Controls.Add(btnDownloadZWO_Camera_Driver);
            Controls.Add(btnDownloadASIASCOM);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(progressBar);
            Controls.Add(txtOutput);
            Controls.Add(lblAria2cVersion);
            Controls.Add(lblStatus);
            Controls.Add(btnAbortDownload);
            Controls.Add(btnDownloadASCOM);
            Controls.Add(btnDownloadNINA);
            Controls.Add(btnDownloadPHD2);
            Controls.Add(btnDownloadSharpCap);
            Controls.Add(btnDownloadQHYCCD);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "DownloaderMainFrom";
            Text = "初雨的超级天文软件下载器";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Button btnDownloadNINA;
        private System.Windows.Forms.Button btnDownloadASCOM;
        private System.Windows.Forms.Button btnDownloadPHD2;
        private System.Windows.Forms.Button btnDownloadSharpCap;
        private System.Windows.Forms.Button btnDownloadQHYCCD;
        private System.Windows.Forms.Button btnAbortDownload;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAria2cVersion;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ProgressBar progressBar;
        private Label label1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 实用功能ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem 手动进行Aria2下载ToolStripMenuItem;
        private ToolStripMenuItem 编辑Aria2下载器配置ToolStripMenuItem;
        private ToolStripMenuItem 唤起NINA插件目录ToolStripMenuItem;
        private ToolStripMenuItem 唤起Siril插件目录ToolStripMenuItem;
        private ToolStripMenuItem 关于软件ToolStripMenuItem;
        private Label label2;
        private Button btnDownloadASIASCOM;
        private Button btnDownloadZWO_Camera_Driver;
        private Button btnDownloadD80;
        private Label label3;
        private Button btnDownloadD50;
        private Button btnDownloadD20;
        private Button btnDownloadASTAP;
        private ToolStripMenuItem 木星拍摄时长计算ToolStripMenuItem;
        private Button btnDownloadTouptek;
        private Label label4;
        private Button btnDownloadStellarium;
        private Button btnDownloadKstars;
        private Button btnDownloadtodesk;
        private Button btnDownloadCanon;
        private Button btnDownloadSONY;
        private Button btnDownloadDSLRAscom;
        private Button btnDownloadOnStep;
        private Button btnDownloadG05;
        private Label label5;
        private Button btnDownloadGSServer;
        private Label label6;
        private Button btnDownloadPIPP;
        private Button btnDownloadWinJUPOS;
        private Button btnDownloadAutoStakkert;
        private Button btnDownloadRegistax;
        private Button btnDownloadSiril;
        private ToolStripMenuItem 天文摄影计算器ToolStripMenuItem;
    }
}

