using System;
using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    public partial class JupiterCalculationForm : Form
    {
        public JupiterCalculationForm()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // �̶�ľ����ת���� T = 9Сʱ50���� = 590����
                double T = 590; // ľ����ת���ڣ����ӣ�

                // ��ȡ�û�����
                double D = double.Parse(txtD.Text);     // ľ���ӽ�ֱ������λ�����룩
                double p = double.Parse(txtp.Text);     // ������ش�С����λ��΢�ף�
                double f = double.Parse(txtF.Text);     // ��Զ�����ࣨ��λ�����ף�

                // ����ֱ��ʣ�����/���أ�
                double resolution = Math.Atan(p / 1000 / f) * (180 / Math.PI) * 3600;

                // ���� t
                double t = (Math.Atan(resolution / (D / 2)) / (2 * Math.PI)) * T;

                // ��ʾ���
                lblResult.Text = $"ľ���ƶ�һ�����ص�ʱ�䣨������˶�ģ����Ϊ t = {t:F2} ����\n��������ʱ����� t/2 = {t / 2:F2} ����";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"����ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void JupiterCalculationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
