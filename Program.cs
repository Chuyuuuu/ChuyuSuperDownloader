using System;
using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 启用视觉效果
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 启动应用程序并显示主窗体
            Application.Run(new DownloaderMainFrom());
        }
    }
}