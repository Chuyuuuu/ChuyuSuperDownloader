namespace AstronomySoftwareDownloader
{
    partial class ConfigForm
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
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
        /// �����֧������ķ��� - �벻Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.lblMaxNumCon = new System.Windows.Forms.Label();
            this.txtMaxNumCon = new System.Windows.Forms.TextBox();
            this.lblSplit = new System.Windows.Forms.Label();
            this.txtSplit = new System.Windows.Forms.TextBox();
            this.lblMinSplitSize = new System.Windows.Forms.Label();
            this.txtMinSplitSize = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMaxNumCon
            // 
            this.lblMaxNumCon.AutoSize = true;
            this.lblMaxNumCon.Location = new System.Drawing.Point(20, 20);
            this.lblMaxNumCon.Name = "lblMaxNumCon";
            this.lblMaxNumCon.Size = new System.Drawing.Size(134, 18);
            this.lblMaxNumCon.TabIndex = 0;
            this.lblMaxNumCon.Text = "��������� (-x)��";
            // 
            // txtMaxNumCon
            // 
            this.txtMaxNumCon.Location = new System.Drawing.Point(160, 17);
            this.txtMaxNumCon.Name = "txtMaxNumCon";
            this.txtMaxNumCon.Size = new System.Drawing.Size(100, 28);
            this.txtMaxNumCon.TabIndex = 1;
            // 
            // lblSplit
            // 
            this.lblSplit.AutoSize = true;
            this.lblSplit.Location = new System.Drawing.Point(20, 60);
            this.lblSplit.Name = "lblSplit";
            this.lblSplit.Size = new System.Drawing.Size(98, 18);
            this.lblSplit.TabIndex = 2;
            this.lblSplit.Text = "�ֶ��� (-s)��";
            // 
            // txtSplit
            // 
            this.txtSplit.Location = new System.Drawing.Point(160, 57);
            this.txtSplit.Name = "txtSplit";
            this.txtSplit.Size = new System.Drawing.Size(100, 28);
            this.txtSplit.TabIndex = 3;
            // 
            // lblMinSplitSize
            // 
            this.lblMinSplitSize.AutoSize = true;
            this.lblMinSplitSize.Location = new System.Drawing.Point(20, 100);
            this.lblMinSplitSize.Name = "lblMinSplitSize";
            this.lblMinSplitSize.Size = new System.Drawing.Size(134, 18);
            this.lblMinSplitSize.TabIndex = 4;
            this.lblMinSplitSize.Text = "��С�ֶδ�С (-k)��";
            // 
            // txtMinSplitSize
            // 
            this.txtMinSplitSize.Location = new System.Drawing.Point(160, 97);
            this.txtMinSplitSize.Name = "txtMinSplitSize";
            this.txtMinSplitSize.Size = new System.Drawing.Size(100, 28);
            this.txtMinSplitSize.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(50, 150);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 32);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "����";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(160, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMinSplitSize);
            this.Controls.Add(this.lblMinSplitSize);
            this.Controls.Add(this.txtSplit);
            this.Controls.Add(this.lblSplit);
            this.Controls.Add(this.txtMaxNumCon);
            this.Controls.Add(this.lblMaxNumCon);
            this.Name = "ConfigForm";
            this.Text = "�༭ Aria2 ����";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMaxNumCon;
        private System.Windows.Forms.TextBox txtMaxNumCon;
        private System.Windows.Forms.Label lblSplit;
        private System.Windows.Forms.TextBox txtSplit;
        private System.Windows.Forms.Label lblMinSplitSize;
        private System.Windows.Forms.TextBox txtMinSplitSize;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
