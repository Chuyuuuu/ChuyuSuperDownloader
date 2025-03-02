using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text;



namespace AstronomySoftwareDownloader
{
    public partial class DownloaderMainFrom : Form
    {

        private string aria2cPath = @"bin/aria2c.exe";
        private readonly Regex progressRegex = new Regex(@"\((\d+)%\)"); // 进度匹配正则表达式
        private bool isTaskRunning = false; // 用于标识任务是否正在执行
        private Process downloadProcess = null; // 用于存储当前下载的进程
        private bool isDownloadAborted = false; // 标记下载是否被中止


        public DownloaderMainFrom()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            CheckAria2c();
            LoadAria2cConfig();
        }

        // 配置文件路径
        private string configFilePath = Path.Combine(Application.StartupPath, "config", "downloadcfg.ini");
        // 用于存储配置的字典
        private Dictionary<string, string> aria2cConfig = new Dictionary<string, string>();

        // 加载配置
        private void LoadAria2cConfig()
        {
            if (!File.Exists(configFilePath))
            {
                MessageBox.Show("未检测到标准下载配置", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                // 读取配置文件
                var lines = File.ReadAllLines(configFilePath);
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                    {
                        var parts = line.Split(new[] { '=' }, 2);
                        if (parts.Length == 2)
                        {
                            aria2cConfig[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }
            }
        }

        // 启动时检查 aria2c 是否存在，并获取版本号
        private void CheckAria2c()
        {
            if (!File.Exists(aria2cPath))
            {
                MessageBox.Show("aria2c.exe 文件未找到！下载将无法完成", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        // 生成 aria2c 参数的方法
        private string GetAria2cArguments(string downloadUrl, string installerName, string tempDirectory)
        {
            // 基本的参数列表
            var arguments = new List<string>
    {
        $"-o \"{installerName}\"",
        $"-d \"{tempDirectory}\"",
        $"\"{downloadUrl}\""
    };

            // 从配置中读取参数并添加
            foreach (var kvp in aria2cConfig)
            {
                switch (kvp.Key)
                {
                    case "MaxNumCon":
                        arguments.Insert(0, $"-x {kvp.Value}");
                        break;
                    case "Split":
                        arguments.Insert(0, $"-s {kvp.Value}");
                        break;
                    case "MinSplitSize":
                        arguments.Insert(0, $"-k {kvp.Value}");
                        break;
                    // 根据需要添加更多参数
                    default:
                        break;
                }
            }

            return string.Join(" ", arguments);
        }

        private async Task DownloadAndInstallSoftware(string downloadUrl, string installerName, bool isZip = false)
        {
            if (isTaskRunning)
            {
                MessageBox.Show("下载任务正在执行中，请等待当前任务完成", "任务执行中", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isTaskRunning = true; // 设置任务正在执行
            isDownloadAborted = false; // 重置下载中止标志

            // Check if the URL is from GitHub and apply the proxy
            string originalUrl = downloadUrl; // 保留原始URL
            if (downloadUrl.Contains("github.com"))
            {
                downloadUrl = "https://gh-proxy.com/" + downloadUrl;
            }

            string installerPath = await DownloadSoftwareAsync(downloadUrl, installerName, originalUrl);
            if (isDownloadAborted)
            {
                lblStatus.Text = "下载已中止，取消安装。";
                isTaskRunning = false;
                return;
            }
            if (installerPath != null && File.Exists(installerPath))
            {
                lblStatus.Text = $"文件下载成功：{installerPath}";

                if (isZip)
                {
                    string extractedPath = await ExtractZip(installerPath);
                    if (extractedPath != null)
                    {
                        InstallSoftware(extractedPath);
                    }
                }
                else
                {
                    InstallSoftware(installerPath);
                }
            }

            isTaskRunning = false; // 任务完成
        }

        // 执行安装
        private void InstallSoftware(string installerPath)
        {
            try
            {
                if (File.Exists(installerPath))
                {
                    lblStatus.Text = "正在安装...";

                    string extension = Path.GetExtension(installerPath).ToLower();

                    if (extension == ".exe")
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo(installerPath)
                        {
                            UseShellExecute = true
                        };

                        Process process = Process.Start(startInfo);
                        process.WaitForExit();

                        lblStatus.Text = "安装程序已启动。";
                    }
                    else if (extension == ".zip")
                    {
                        // 直接打开 ZIP 文件所在的目录
                        ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe", $"/select,\"{installerPath}\"")
                        {
                            UseShellExecute = true
                        };
                        Process.Start(startInfo);

                        lblStatus.Text = "已打开 AutoStakkert 所在文件夹，请手动解压并运行。";
                    }
                    else
                    {
                        lblStatus.Text = $"无法处理的文件类型：{extension}";
                    }
                }
                else
                {
                    lblStatus.Text = $"安装文件 {installerPath} 不存在。";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"安装时出错：{ex.Message}";
            }
        }


        private async Task<string> ExtractZip(string zipFilePath)
        {
            try
            {
                // 如果文件名包含 "AutoStakkert"，则不解压，直接返回 ZIP 文件路径
                if (Path.GetFileName(zipFilePath).Contains("AutoStakkert", StringComparison.OrdinalIgnoreCase))
                {
                    lblStatus.Text = "无需解压，直接运行 AutoStakkert。";
                    return zipFilePath;
                }

                string tempDirectory = Path.Combine(Directory.GetCurrentDirectory(), "temp", "installer");
                if (Directory.Exists(tempDirectory)) Directory.Delete(tempDirectory, true);
                Directory.CreateDirectory(tempDirectory);

                ZipFile.ExtractToDirectory(zipFilePath, tempDirectory);

                // 搜索所有可能的安装程序
                var exeFiles = Directory.GetFiles(tempDirectory, "*.exe", SearchOption.AllDirectories);
                if (exeFiles.Length > 0)
                {
                    lblStatus.Text = "解压完成，找到安装程序。";
                    return exeFiles[0];
                }

                lblStatus.Text = "解压失败，未找到安装程序。";
                return null;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"解压时出错：{ex.Message}";
                return null;
            }
        }


        private async Task<string> DownloadSoftwareAsync(string downloadUrl, string installerName, string originalUrl)
        {
            try
            {
                lblStatus.Text = $"正在下载 {installerName}...";
                progressBar.Value = 0;

                string tempDirectory = Path.Combine(Directory.GetCurrentDirectory(), "temp");
                Directory.CreateDirectory(tempDirectory);
                string tempPath = Path.Combine(tempDirectory, installerName);

                string arguments = GetAria2cArguments(downloadUrl, installerName, tempDirectory);

                // 第一次尝试使用代理下载
                bool success = await RunAria2c(arguments, tempPath);


                if (!success)
                {
                    // 检查下载是否被中止
                    if (isDownloadAborted)
                    {
                        return null;
                    }
                    else
                    {
                        lblStatus.Text = "下载失败，尝试使用原始链接下载。";
                    }
                    // 如果代理下载失败，使用原始URL重新下载
                    lblStatus.Text = "代理链接出现错误，将采用原速下载";
                    arguments = GetAria2cArguments(originalUrl, installerName, tempDirectory);
                    success = await RunAria2c(arguments, tempPath);
                }

                return success ? tempPath : null;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"下载 {installerName} 时出错：{ex.Message}";
                return null;
            }
        }

        private async Task<bool> RunAria2c(string arguments, string expectedPath)
        {
            try
            {
                using (downloadProcess = new Process())
                {
                    downloadProcess.StartInfo = new ProcessStartInfo
                    {
                        FileName = aria2cPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    downloadProcess.OutputDataReceived += (sender, args) =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                            {
                                txtOutput.AppendText(args.Data + Environment.NewLine);
                                UpdateProgressBar(args.Data);
                            }
                        }));
                    };

                    downloadProcess.ErrorDataReceived += (sender, args) =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!string.IsNullOrEmpty(args.Data))
                                txtOutput.AppendText("错误: " + args.Data + Environment.NewLine);
                        }));
                    };

                    downloadProcess.Start();
                    downloadProcess.BeginOutputReadLine();
                    downloadProcess.BeginErrorReadLine();
                    await downloadProcess.WaitForExitAsync();

                    // 检查下载是否被中止
                    if (isDownloadAborted)
                    {
                        return false;
                    }
                    return File.Exists(expectedPath);
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> RunWgetAsync(string wgetPath, string arguments)
        {
            try
            {
                using (downloadProcess = new Process())
                {
                    downloadProcess.StartInfo = new ProcessStartInfo
                    {
                        FileName = wgetPath,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    };

                    // 合并标准输出和错误输出
                    downloadProcess.OutputDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            this.Invoke(new Action(() =>
                            {
                                txtOutput.AppendText(args.Data + Environment.NewLine);
                                UpdateProgressBarFromWgetOutput(args.Data);
                            }));
                        }
                    };

                    downloadProcess.ErrorDataReceived += (sender, args) =>
                    {
                        if (!string.IsNullOrEmpty(args.Data))
                        {
                            this.Invoke(new Action(() =>
                            {
                                txtOutput.AppendText(args.Data + Environment.NewLine);
                                UpdateProgressBarFromWgetOutput(args.Data);
                            }));
                        }
                    };

                    downloadProcess.Start();
                    downloadProcess.BeginOutputReadLine();
                    downloadProcess.BeginErrorReadLine();

                    await downloadProcess.WaitForExitAsync();

                    // 确保异步输出读取已完成
                    downloadProcess.CancelOutputRead();
                    downloadProcess.CancelErrorRead();

                    // 等待进程完全退出
                    downloadProcess.WaitForExit();

                    // 检查下载是否被中止
                    if (isDownloadAborted)
                    {
                        return false;
                    }

                    return downloadProcess.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"运行 wget 时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void UpdateProgressBarFromWgetOutput(string output)
        {
            // 解析 wget 输出，更新进度条
            var match = Regex.Match(output, @"(?<percent>\d+)%");
            if (match.Success && int.TryParse(match.Groups["percent"].Value, out int percent))
            {
                progressBar.Value = Math.Min(Math.Max(percent, 0), 100);
            }
        }


        private void UpdateProgressBar(string output)
        {
            Match match = progressRegex.Match(output);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int percent))
            {
                progressBar.Value = Math.Min(Math.Max(percent, 0), 100);
            }
        }

        private async void btnDownloadNINA_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestNINADownloadUrlAsync("https://nighttime-imaging.eu/download/");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl), true);
            }
        }

        private async void btnDownloadGSServer_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestGSServerDownloadUrlAsync("https://greenswamp.org/?page_id=834");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl), true);
            }
        }
        private async void btnDownloadOnStep_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestOnStepDownloadUrlAsync("http://stellarjourney.com/main/onstep-ascom-driver-software/");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl), true);
            }
        }
        private async void btnDownloadDSLRAscom_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://github.com/FearL0rd/ASCOM.DSLR/raw/master/DSLR.Camera%20Setup.exe";
            await DownloadAndInstallSoftware(downloadUrl, "DSLRCamera20Setup.exe");
        }
        private async void btnDownloadRegistax_Click(object sender, EventArgs e)
        {
            string downloadUrl = "http://www.astronomie.be/registax/updateregistax6.exe";
            await DownloadAndInstallSoftware(downloadUrl, "updateregistax6.exe");
        }
        private async void btnDownloadPIPP_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"PIPP已经停止维护数年，将下载存储桶缓存的最新版");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/pipp_install_x64_2.5.9.exe";
            await DownloadAndInstallSoftware(downloadUrl, "pipp_install_x64_2.5.9.exe");
        }
        private async void btnDownloadWinJUPOS_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestWinJUPOSDownloadUrlAsync("https://jupos.org/gh/download.htm");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl));
            }
        }
        private async void btnDownloadAutoStakkert_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestAutoStakkertDownloadUrlAsync("https://www.autostakkert.com/wp/download/");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl), true);
            }
            else
            {
                MessageBox.Show("未能获取 AutoStakkert 的下载链接。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnDownloadSiril_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestSirilDownloadUrlAsync("https://siril.org/");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl), false);
            }
            else
            {
                MessageBox.Show("未能获取 Siril 的下载链接。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDownloadCanon_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://gdlp01.c-wss.com/gds/7/0200006717/01/EU-Installset-W3.16.0.7.zip";
            await DownloadAndInstallSoftware(downloadUrl, "EOS_Utility.zip", true);
        }
        private async void btnDownloadSONY_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://support.d-imaging.sony.co.jp/disoft_DL/desktop_DL/win?fm=cn";
            await DownloadAndInstallSoftware(downloadUrl, "ied_setup.exe");
        }
        private async void btnDownloadASCOM_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestASCOMDownloadUrlAsync("https://www.ascom-standards.org/");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl));
            }
        }

        private async void btnDownloadStellarium_Click(object sender, EventArgs e)
        {
            string downloadUrl = await GetLatestStellariumDownloadUrlAsync("https://stellarium.org/");
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl));
            }
        }

        private async void btnDownloadPHD2_Click(object sender, EventArgs e)
        {
            string pageUrl = "https://openphdguiding.org/";
            string downloadUrl = await GetLatestPHD2DownloadUrlAsync(pageUrl);

            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl));
            }
        }
        private async void btnDownloadSharpCap_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://d.sharpcap.co.uk/file/a486ae2b-152f-1a4c-35de-1303380c6240_x64";
            await DownloadAndInstallSoftware(downloadUrl, "SharpCapSetup.exe");
        }

        private async void btnDownloadQHYCCD_Click(object sender, EventArgs e)
        {
            string pageUrl = "https://www.qhyccd.cn/download/";
            string downloadUrl = await GetLatestQHYCCDDownloadUrlAsync(pageUrl);

            if (!string.IsNullOrEmpty(downloadUrl))
            {
                await DownloadAndInstallSoftware(downloadUrl, Path.GetFileName(downloadUrl));
            }
        }
        private async void btnDownloadASIASCOM_Click(object sender, EventArgs e)
        {
            // 下载页面 URL

            string pageUrl = "https://www.zwoastro.cn/downloads/";
            if (isTaskRunning)
            {
                MessageBox.Show("下载任务正在执行中，请等待当前任务完成", "任务执行中", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isTaskRunning = true; // 设置任务正在执行
            isDownloadAborted = false; // 重置下载中止标志

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 添加请求头，模拟浏览器
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
                    client.DefaultRequestHeaders.Add("Referer", "https://www.zwoastro.cn/");

                    // 获取下载页面的 HTML 内容
                    string html = await client.GetStringAsync(pageUrl);

                    // 使用正则表达式提取包含 'ASCOMDriver' 的下载链接
                    string pattern = @"<a\s+href=""(?<href>//dl\.zwoastro\.com/software\?app=ASCOMDrive[^""]+)"".*?class=""download"".*?>\s*<span.*?>下载</span>\s*</a>";
                    var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string href = match.Groups["href"].Value;

                        // 对链接进行 HTML 解码，去除 &amp;
                        string decodedHref = WebUtility.HtmlDecode(href);

                        // 确保链接以 https: 开头
                        string downloadUrl = decodedHref.StartsWith("http") ? decodedHref : "https:" + decodedHref;

                        // 下载链接中包含 & 符号，需要用双引号括起来
                        string quotedDownloadUrl = $"\"{downloadUrl}\"";

                        // 下载的文件名
                        string installerName = "ASI_ASCOM.exe";
                        string tempDirectory = Path.Combine(Application.StartupPath, "temp");
                        Directory.CreateDirectory(tempDirectory);
                        string savePath = Path.Combine(tempDirectory, installerName);

                        // wget.exe 的路径
                        string wgetPath = Path.Combine(Application.StartupPath, "bin", "wget.exe");

                        if (!File.Exists(wgetPath))
                        {
                            MessageBox.Show("未找到 wget.exe 文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 构建 wget 的命令参数
                        string arguments = $"{quotedDownloadUrl} -O \"{savePath}\"";

                        lblStatus.Text = "正在下载 ASI ASCOM驱动...";
                        progressBar.Value = 0;

                        // 使用 wget 下载
                        bool success = await RunWgetAsync(wgetPath, arguments);

                        if (success)
                        {
                            lblStatus.Text = $"ASI ASCOM驱动下载成功";
                            isTaskRunning = false;
                            // 执行安装程序
                            InstallSoftware(savePath);
                        }
                        else
                        {
                            isTaskRunning = false;
                            lblStatus.Text = "ASI ASCOM驱动下载失败。";
                        }
                    }
                    else
                    {
                        MessageBox.Show("未找到 ASI ASCOM驱动的下载链接。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"下载 ASI ASCOM驱动时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDownloadZWO_Camera_Driver_Click(object sender, EventArgs e)
        {
            if (isTaskRunning)
            {
                MessageBox.Show("下载任务正在执行中，请等待当前任务完成", "任务执行中", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isTaskRunning = true; // 设置任务正在执行
            isDownloadAborted = false; // 重置下载中止标志

            // 下载页面 URL
            string pageUrl = "https://www.zwoastro.cn/downloads/";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 添加请求头，模拟浏览器
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
                    client.DefaultRequestHeaders.Add("Referer", "https://www.zwoastro.cn/");

                    // 获取下载页面的 HTML 内容
                    string html = await client.GetStringAsync(pageUrl);

                    // 使用正则表达式提取包含 'AsiCameraDriver' 的下载链接
                    string pattern = @"<a\s+href=""(?<href>//dl\.zwoastro\.com/software\?app=AsiCameraDriver[^""]+)"".*?class=""download"".*?>\s*<span.*?>下载</span>\s*</a>";
                    var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string href = match.Groups["href"].Value;

                        // 对链接进行 HTML 解码，去除 &amp;
                        string decodedHref = WebUtility.HtmlDecode(href);

                        // 确保链接以 https: 开头
                        string downloadUrl = decodedHref.StartsWith("http") ? decodedHref : "https:" + decodedHref;

                        // 下载链接中包含 & 符号，需要用双引号括起来
                        string quotedDownloadUrl = $"\"{downloadUrl}\"";

                        // 下载的文件名
                        string installerName = "ZWO_CAM.exe";
                        string tempDirectory = Path.Combine(Application.StartupPath, "temp");
                        Directory.CreateDirectory(tempDirectory);
                        string savePath = Path.Combine(tempDirectory, installerName);

                        // wget.exe 的路径
                        string wgetPath = Path.Combine(Application.StartupPath, "bin", "wget.exe");

                        if (!File.Exists(wgetPath))
                        {
                            MessageBox.Show("未找到 wget.exe 文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 构建 wget 的命令参数
                        string arguments = $"{quotedDownloadUrl} -O \"{savePath}\"";

                        lblStatus.Text = "正在下载 ZWO 相机驱动...";
                        progressBar.Value = 0;

                        // 使用 wget 下载
                        bool success = await RunWgetAsync(wgetPath, arguments);

                        if (success)
                        {
                            lblStatus.Text = $"ZWO相机驱动下载成功";
                            isTaskRunning = false;
                            // 执行安装程序
                            InstallSoftware(savePath);
                        }
                        else
                        {
                            isTaskRunning = false;
                            lblStatus.Text = "ZWO相机驱动下载失败。";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"下载 ZWO 相机驱动时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnDownloadD80_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"即将开始下载D80数据库，如果遇到下载速度骤减请中止下载进程并重新打开程序下载，程序会自动读取下载进度！");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/d80_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "d80_star_database.exe");
        }

        private async void btnDownloadD50_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"即将开始下载D50数据库，如果遇到下载速度骤减请中止下载进程并重新打开程序下载，程序会自动读取下载进度！");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/d50_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "d50_star_database.exe");
        }

        private async void btnDownloadD20_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"即将开始下载D20数据库，如果遇到下载速度骤减请中止下载进程并重新打开程序下载，程序会自动读取下载进度！");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/d20_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "d20_star_database.exe");
        }
        private async void btnDownloadG05_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"即将开始下载G05数据库，如果遇到下载速度骤减请中止下载进程并重新打开程序下载，程序会自动读取下载进度！");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/g05_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "g05_star_database.exe");
        }
        private async void btnDownloadASTAP_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://sourceforge.net/projects/astap-program/files/windows_installer/astap_setup.exe";
            await DownloadAndInstallSoftware(downloadUrl, "astap_setup.exe");
        }
        private async void btnDownloadTouptek_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://dl.touptek-astro.com.cn/download/ToupTekASCOMSetup.exe";
            await DownloadAndInstallSoftware(downloadUrl, "ToupTekASCOMSetup.exe");
        }
        private async void btnDownloadKstars_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"请注意，Kstars由我们的存储桶缓存，有可能不是最新版本");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/kstars-3.7.5.exe";
            await DownloadAndInstallSoftware(downloadUrl, "kstars.exe");
        }
        private async void btnDownloadtodesk_Click(object sender, EventArgs e)
        {
            string downloadUrl = "https://dl.todesk.com/windows/ToDesk_Setup.exe";
            await DownloadAndInstallSoftware(downloadUrl, "ToDesk_Setup.exe");
        }
        private async Task<string> GetLatestNINADownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    Match match = Regex.Match(html, @"<a\s+class\s*=\s*""download_button""\s+href\s*=\s*""([^""]+)""");
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取下载地址失败：{ex.Message}");
                return null;
            }
        }
        private async Task<string> GetLatestGSServerDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    // 使用正则表达式匹配下载链接
                    Match match = Regex.Match(html, @"<a[^>]*href=""(?<url>https://github\.com/rmorgan001/GSServer/releases/download/[^""]+\.zip)""[^>]*>\s*<u>Download GS Server v[\d\.]+</u>\s*</a>", RegexOptions.IgnoreCase);
                    return match.Success ? match.Groups["url"].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 GS Server 下载地址失败：{ex.Message}");
                return null;
            }
        }

        private async Task<string> GetLatestWinJUPOSDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    // 使用正则表达式匹配下载链接
                    Match match = Regex.Match(html, @"<a\s+href=""(?<url>winjupos_setup_[^""]+\.exe)"">WinJUPOS\s+setup\s+file[^<]*</a>", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        // 构建完整的下载链接
                        string baseUrl = "https://jupos.org/gh/";
                        string downloadUrl = baseUrl + match.Groups["url"].Value;
                        return downloadUrl;
                    }
                    else
                    {
                        MessageBox.Show("未找到 WinJUPOS 的下载链接。");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 WinJUPOS 下载地址失败：{ex.Message}");
                return null;
            }
        }
        private async Task<string> GetLatestSirilDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);

                    // 使用正则表达式匹配新的下载链接格式
                    Match match = Regex.Match(html, @"href\s*=\s*""(?<url>https?://[^""]+\.exe)""", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        return match.Groups["url"].Value;
                    }
                    else
                    {
                        MessageBox.Show("未找到 Siril 的下载链接。");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 Siril 下载地址失败：{ex.Message}");
                return null;
            }
        }


        private async Task<string> GetLatestAutoStakkertDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    // 使用正则表达式匹配下载链接
                    Match match = Regex.Match(html, @"<a\s+href=""(?<url>https://www\.astrokraai\.nl/software/AutoStakkert_\d+\.\d+\.\d+_x64\.zip)"">\s*AutoStakkert\s*\d+\.\d+\.\d+\s*</a>", RegexOptions.IgnoreCase);
                    return match.Success ? match.Groups["url"].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 AutoStakkert 下载地址失败：{ex.Message}");
                return null;
            }
        }

        private async Task<string> GetLatestOnStepDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    // 使用正则表达式匹配下载链接
                    Match match = Regex.Match(html, @"<a\s+href=""(?<url>http://www\.stellarjourney\.com/assets/downloads/OnStep-Setup-[^""]+\.zip)"">download here</a>", RegexOptions.IgnoreCase);
                    return match.Success ? match.Groups["url"].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 ONSTEP 驱动下载地址失败：{ex.Message}");
                return null;
            }
        }


        private async Task<string> GetLatestASCOMDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    // 使用正则表达式匹配下载链接
                    Match match = Regex.Match(html, @"<a\s+href\s*=\s*""(https://github.com/ASCOMInitiative/ASCOMPlatform/releases/download/[^""]+)""\s+target\s*=\s*""_self""");
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取ASCOM下载地址失败：{ex.Message}");
                return null;
            }
        }

        private async Task<string> GetLatestStellariumDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);
                    // 使用正则表达式匹配下载链接
                    string pattern = @"<a\s+href=""(?<url>https://github\.com/Stellarium/stellarium/releases/download/[^""]+qt6-win64\.exe)"">Windows<span>x86_64; Qt6; Windows 10\+</span></a>";
                    Match match = Regex.Match(html, pattern);
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取ASCOM下载地址失败：{ex.Message}");
                return null;
            }
        }
        private async Task<string> GetLatestPHD2DownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);

                    // 匹配 Windows 版本的下载链接
                    Match match = Regex.Match(html, @"<a\s+href\s*=\s*""([^""]+\.exe)""\s+class\s*=\s*""button"">\s*Download.*Windows\s*</a>", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string relativeUrl = match.Groups[1].Value;
                        // 拼接完整的下载链接
                        string downloadUrl = new Uri(new Uri(pageUrl), relativeUrl).ToString();
                        return downloadUrl;
                    }
                    else
                    {
                        lblStatus.Text = "未找到 PHD2 的下载链接。";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 PHD2 下载地址失败：{ex.Message}");
                return null;
            }
        }

        private async Task<string> GetLatestQHYCCDDownloadUrlAsync(string pageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string html = await client.GetStringAsync(pageUrl);

                    // 使用正则表达式匹配下载链接
                    Match match = Regex.Match(html, @"<a\s+href=""([^""]+\.exe)"">Latest Ver\.\d+（最新稳定版）</a>");

                    if (match.Success)
                    {
                        string downloadUrl = match.Groups[1].Value;
                        return downloadUrl;
                    }
                    else
                    {
                        lblStatus.Text = "未找到 QHYCCD 驱动的下载链接。";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取 QHYCCD 下载地址失败：{ex.Message}");
                return null;
            }
        }

        private void btnAbortDownload_Click(object sender, EventArgs e)
        {
            if (downloadProcess != null)
            {
                try
                {
                    if (!downloadProcess.HasExited)
                    {
                        downloadProcess.Kill(); // 强制中止下载进程
                    }

                    // 取消异步读取
                    downloadProcess.CancelOutputRead();
                    downloadProcess.CancelErrorRead();

                    // 释放资源
                    downloadProcess.Close();
                    downloadProcess = null;

                    lblStatus.Text = "任务已中止。";
                    isTaskRunning = false;
                    isDownloadAborted = true; // 设置下载已中止的标志
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"中止任务时发生错误：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("没有正在执行的任务。", "无任务", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string wubPath = Path.Combine(Application.StartupPath, "bin", "wub.exe");

            if (File.Exists(wubPath))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(wubPath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                    lblStatus.Text = "已启动 wub.exe。";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"启动 wub.exe 时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("未找到 wub.exe 文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 编辑Aria2下载器配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string configFilePath = Path.Combine(Application.StartupPath, "config", "downloadcfg.ini");
            var configForm = new ConfigForm(configFilePath);
            configForm.ShowDialog();

            // 重新加载配置
            aria2cConfig.Clear();
            LoadAria2cConfig();
        }
        private async void 手动进行Aria2下载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 弹出输入框让用户输入下载链接
            string downloadUrl = Prompt.ShowDialog("请输入下载链接：", "手动进行Aria2下载");
            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                MessageBox.Show("未进行下载", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 获取文件名
            string installerName = Path.GetFileName(new Uri(downloadUrl).AbsolutePath);
            if (string.IsNullOrEmpty(installerName))
            {
                MessageBox.Show("无法从链接中获取文件名。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 定义保存路径为 UserDownload 目录
            string userDownloadPath = Path.Combine(Application.StartupPath, "UserDownload");
            Directory.CreateDirectory(userDownloadPath);
            string savePath = Path.Combine(userDownloadPath, installerName);

            // 生成 aria2c 参数
            string arguments = GetAria2cArguments(downloadUrl, installerName, userDownloadPath);

            // 执行下载
            bool success = await RunAria2c(arguments, savePath);

            if (success)
            {
                lblStatus.Text = $"文件下载成功";
                MessageBox.Show($"文件已成功下载到：{savePath}", "下载完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = "下载失败。";
                MessageBox.Show("下载过程中出现错误。", "下载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 唤起NINA插件目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取当前用户的本地应用数据路径
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // 构建插件目录的完整路径
                string pluginsPath = Path.Combine(localAppData, "NINA", "Plugins");

                // 检查目录是否存在
                if (Directory.Exists(pluginsPath))
                {
                    // 打开目录
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = pluginsPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("未找到 NINA 插件目录。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开 NINA 插件目录时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 唤起Siril插件目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取当前用户的漫游应用数据路径
                string roamingAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // 构建 Siril 插件目录的完整路径
                string scriptsPath = "C:\\Program Files\\Siril\\scripts";

                // 检查目录是否存在
                if (Directory.Exists(scriptsPath))
                {
                    // 打开目录
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = scriptsPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("未找到 Siril 插件目录。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开 Siril 插件目录时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 木星拍摄时长计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JupiterCalculationForm calcForm = new JupiterCalculationForm();
            calcForm.ShowDialog();
        }

        private void 关于软件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        //引用天文摄影计算器，已经获得原作者@Garth授权，项目地址：https://github.com/GarthTB/AstrophotoCalculator
        private void 天文摄影计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string wubPath = Path.Combine(Application.StartupPath, "bin", "AstrophotoCalculator.exe");

            if (File.Exists(wubPath))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(wubPath)
                    {
                        UseShellExecute = true
                    };
                    Process.Start(startInfo);
                    lblStatus.Text = "已启动 AstrophotoCalculator.exe。";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"启动 AstrophotoCalculator.exe 时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("未找到 AstrophotoCalculator.exe 文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}