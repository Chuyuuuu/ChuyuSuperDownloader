using System;
using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    static class Program
    {
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main()
        {
            // �����Ӿ�Ч��
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ����Ӧ�ó�����ʾ������
            Application.Run(new DownloaderMainFrom());
        }
    }
}