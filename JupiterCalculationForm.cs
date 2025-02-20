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
                // 固定木星自转周期 T = 9小时50分钟 = 590分钟
                double T = 590; // 木星自转周期（分钟）

                // 获取用户输入
                double D = double.Parse(txtD.Text);     // 木星视角直径（单位：弧秒）
                double p = double.Parse(txtp.Text);     // 相机像素大小（单位：微米）
                double f = double.Parse(txtF.Text);     // 望远镜焦距（单位：毫米）

                // 计算分辨率（角秒/像素）
                double resolution = Math.Atan(p / 1000 / f) * (180 / Math.PI) * 3600;

                // 计算 t
                double t = (Math.Atan(resolution / (D / 2)) / (2 * Math.PI)) * T;

                // 显示结果
                lblResult.Text = $"木星移动一个像素的时间（即造成运动模糊）为 t = {t:F2} 分钟\n建议拍摄时间低于 t/2 = {t / 2:F2} 分钟";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"计算时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void JupiterCalculationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
