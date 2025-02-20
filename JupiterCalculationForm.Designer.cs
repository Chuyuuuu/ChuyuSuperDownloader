using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    partial class JupiterCalculationForm
    {
        /// <summary>
        ///  必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，则为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        ///  设计器支持所需的方法 - 不要修改
        ///  使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            lblD = new Label();
            txtD = new TextBox();
            lblF = new Label();
            txtF = new TextBox();
            btnCalculate = new Button();
            lblResult = new Label();
            lblp = new Label();
            txtp = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // lblD
            // 
            lblD.AutoSize = true;
            lblD.Location = new Point(13, 12);
            lblD.Margin = new Padding(4, 0, 4, 0);
            lblD.Name = "lblD";
            lblD.Size = new Size(222, 24);
            lblD.TabIndex = 0;
            lblD.Text = "D（木星视角直径，角秒）";
            // 
            // txtD
            // 
            txtD.Location = new Point(371, 6);
            txtD.Margin = new Padding(4, 5, 4, 5);
            txtD.Name = "txtD";
            txtD.Size = new Size(152, 30);
            txtD.TabIndex = 1;
            // 
            // lblF
            // 
            lblF.AutoSize = true;
            lblF.Location = new Point(14, 92);
            lblF.Margin = new Padding(4, 0, 4, 0);
            lblF.Name = "lblF";
            lblF.Size = new Size(196, 24);
            lblF.TabIndex = 4;
            lblF.Text = "f（望远镜焦距，毫米）";
            // 
            // txtF
            // 
            txtF.Location = new Point(371, 86);
            txtF.Margin = new Padding(4, 5, 4, 5);
            txtF.Name = "txtF";
            txtF.Size = new Size(152, 30);
            txtF.TabIndex = 5;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(371, 126);
            btnCalculate.Margin = new Padding(4, 5, 4, 5);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(152, 34);
            btnCalculate.TabIndex = 6;
            btnCalculate.Text = "计算";
            btnCalculate.UseVisualStyleBackColor = true;
            btnCalculate.Click += btnCalculate_Click;
            // 
            // lblResult
            // 
            lblResult.AutoSize = true;
            lblResult.Location = new Point(12, 177);
            lblResult.Margin = new Padding(4, 0, 4, 0);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(85, 24);
            lblResult.TabIndex = 7;
            lblResult.Text = "结果： --";
            // 
            // lblp
            // 
            lblp.AutoSize = true;
            lblp.Location = new Point(13, 52);
            lblp.Name = "lblp";
            lblp.Size = new Size(238, 24);
            lblp.TabIndex = 8;
            lblp.Text = "d（相机的像素大小，微米）";
            // 
            // txtp
            // 
            txtp.Location = new Point(371, 46);
            txtp.Margin = new Padding(4, 5, 4, 5);
            txtp.Name = "txtp";
            txtp.Size = new Size(152, 30);
            txtp.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 246);
            label1.Name = "label1";
            label1.Size = new Size(511, 24);
            label1.TabIndex = 10;
            label1.Text = "说明：木星视角直径可以在Stellarium中查询到，请注意单位！";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 270);
            label2.Name = "label2";
            label2.Size = new Size(493, 24);
            label2.TabIndex = 11;
            label2.Text = "实际拍摄时长可以长于该理论值，若使用WINJupos可以更长";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 296);
            label3.Name = "label3";
            label3.Size = new Size(383, 24);
            label3.TabIndex = 12;
            label3.Text = "计算公式来自@酱油颜的星空世界 与 @wnova";
            // 
            // JupiterCalculationForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(591, 329);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtp);
            Controls.Add(lblp);
            Controls.Add(lblResult);
            Controls.Add(btnCalculate);
            Controls.Add(txtF);
            Controls.Add(lblF);
            Controls.Add(txtD);
            Controls.Add(lblD);
            Margin = new Padding(4, 5, 4, 5);
            Name = "JupiterCalculationForm";
            Text = "木星拍摄时长计算";
            Load += JupiterCalculationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.TextBox txtd;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.TextBox txtF;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblResult;
        private Label lblp;
        private TextBox txtp;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
