using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    partial class JupiterCalculationForm
    {
        /// <summary>
        ///  ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ����Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        ///  �����֧������ķ��� - ��Ҫ�޸�
        ///  ʹ�ô���༭���޸Ĵ˷��������ݡ�
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
            lblD.Text = "D��ľ���ӽ�ֱ�������룩";
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
            lblF.Text = "f����Զ�����࣬���ף�";
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
            btnCalculate.Text = "����";
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
            lblResult.Text = "����� --";
            // 
            // lblp
            // 
            lblp.AutoSize = true;
            lblp.Location = new Point(13, 52);
            lblp.Name = "lblp";
            lblp.Size = new Size(238, 24);
            lblp.TabIndex = 8;
            lblp.Text = "d����������ش�С��΢�ף�";
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
            label1.Text = "˵����ľ���ӽ�ֱ��������Stellarium�в�ѯ������ע�ⵥλ��";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 270);
            label2.Name = "label2";
            label2.Size = new Size(493, 24);
            label2.TabIndex = 11;
            label2.Text = "ʵ������ʱ�����Գ��ڸ�����ֵ����ʹ��WINJupos���Ը���";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 296);
            label3.Name = "label3";
            label3.Size = new Size(383, 24);
            label3.TabIndex = 12;
            label3.Text = "���㹫ʽ����@�����յ��ǿ����� �� @wnova";
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
            Text = "ľ������ʱ������";
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
