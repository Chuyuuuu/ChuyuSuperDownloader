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
        private readonly Regex progressRegex = new Regex(@"\((\d+)%\)"); // ����ƥ��������ʽ
        private bool isTaskRunning = false; // ���ڱ�ʶ�����Ƿ�����ִ��
        private Process downloadProcess = null; // ���ڴ洢��ǰ���صĽ���
        private bool isDownloadAborted = false; // ��������Ƿ���ֹ


        public DownloaderMainFrom()
        {
            this.AutoScaleMode = AutoScaleMode.Dpi;
            InitializeComponent();
            CheckAria2c();
            LoadAria2cConfig();
        }

        // �����ļ�·��
        private string configFilePath = Path.Combine(Application.StartupPath, "config", "downloadcfg.ini");
        // ���ڴ洢���õ��ֵ�
        private Dictionary<string, string> aria2cConfig = new Dictionary<string, string>();

        // ��������
        private void LoadAria2cConfig()
        {
            if (!File.Exists(configFilePath))
            {
                MessageBox.Show("δ��⵽��׼��������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                // ��ȡ�����ļ�
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

        // ����ʱ��� aria2c �Ƿ���ڣ�����ȡ�汾��
        private void CheckAria2c()
        {
            if (!File.Exists(aria2cPath))
            {
                MessageBox.Show("aria2c.exe �ļ�δ�ҵ������ؽ��޷����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        // ���� aria2c �����ķ���
        private string GetAria2cArguments(string downloadUrl, string installerName, string tempDirectory)
        {
            // �����Ĳ����б�
            var arguments = new List<string>
    {
        $"-o \"{installerName}\"",
        $"-d \"{tempDirectory}\"",
        $"\"{downloadUrl}\""
    };

            // �������ж�ȡ���������
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
                    // ������Ҫ��Ӹ������
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
                MessageBox.Show("������������ִ���У���ȴ���ǰ�������", "����ִ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isTaskRunning = true; // ������������ִ��
            isDownloadAborted = false; // ����������ֹ��־

            // Check if the URL is from GitHub and apply the proxy
            string originalUrl = downloadUrl; // ����ԭʼURL
            if (downloadUrl.Contains("github.com"))
            {
                downloadUrl = "https://gh-proxy.com/" + downloadUrl;
            }

            string installerPath = await DownloadSoftwareAsync(downloadUrl, installerName, originalUrl);
            if (isDownloadAborted)
            {
                lblStatus.Text = "��������ֹ��ȡ����װ��";
                isTaskRunning = false;
                return;
            }
            if (installerPath != null && File.Exists(installerPath))
            {
                lblStatus.Text = $"�ļ����سɹ���{installerPath}";

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

            isTaskRunning = false; // �������
        }

        // ִ�а�װ
        private void InstallSoftware(string installerPath)
        {
            try
            {
                if (File.Exists(installerPath))
                {
                    lblStatus.Text = "���ڰ�װ...";

                    string extension = Path.GetExtension(installerPath).ToLower();

                    if (extension == ".exe")
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo(installerPath)
                        {
                            UseShellExecute = true
                        };

                        Process process = Process.Start(startInfo);
                        process.WaitForExit();

                        lblStatus.Text = "��װ������������";
                    }
                    else if (extension == ".zip")
                    {
                        // ֱ�Ӵ� ZIP �ļ����ڵ�Ŀ¼
                        ProcessStartInfo startInfo = new ProcessStartInfo("explorer.exe", $"/select,\"{installerPath}\"")
                        {
                            UseShellExecute = true
                        };
                        Process.Start(startInfo);

                        lblStatus.Text = "�Ѵ� AutoStakkert �����ļ��У����ֶ���ѹ�����С�";
                    }
                    else
                    {
                        lblStatus.Text = $"�޷�������ļ����ͣ�{extension}";
                    }
                }
                else
                {
                    lblStatus.Text = $"��װ�ļ� {installerPath} �����ڡ�";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"��װʱ����{ex.Message}";
            }
        }


        private async Task<string> ExtractZip(string zipFilePath)
        {
            try
            {
                // ����ļ������� "AutoStakkert"���򲻽�ѹ��ֱ�ӷ��� ZIP �ļ�·��
                if (Path.GetFileName(zipFilePath).Contains("AutoStakkert", StringComparison.OrdinalIgnoreCase))
                {
                    lblStatus.Text = "�����ѹ��ֱ������ AutoStakkert��";
                    return zipFilePath;
                }

                string tempDirectory = Path.Combine(Directory.GetCurrentDirectory(), "temp", "installer");
                if (Directory.Exists(tempDirectory)) Directory.Delete(tempDirectory, true);
                Directory.CreateDirectory(tempDirectory);

                ZipFile.ExtractToDirectory(zipFilePath, tempDirectory);

                // �������п��ܵİ�װ����
                var exeFiles = Directory.GetFiles(tempDirectory, "*.exe", SearchOption.AllDirectories);
                if (exeFiles.Length > 0)
                {
                    lblStatus.Text = "��ѹ��ɣ��ҵ���װ����";
                    return exeFiles[0];
                }

                lblStatus.Text = "��ѹʧ�ܣ�δ�ҵ���װ����";
                return null;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"��ѹʱ����{ex.Message}";
                return null;
            }
        }


        private async Task<string> DownloadSoftwareAsync(string downloadUrl, string installerName, string originalUrl)
        {
            try
            {
                lblStatus.Text = $"�������� {installerName}...";
                progressBar.Value = 0;

                string tempDirectory = Path.Combine(Directory.GetCurrentDirectory(), "temp");
                Directory.CreateDirectory(tempDirectory);
                string tempPath = Path.Combine(tempDirectory, installerName);

                string arguments = GetAria2cArguments(downloadUrl, installerName, tempDirectory);

                // ��һ�γ���ʹ�ô�������
                bool success = await RunAria2c(arguments, tempPath);


                if (!success)
                {
                    // ��������Ƿ���ֹ
                    if (isDownloadAborted)
                    {
                        return null;
                    }
                    else
                    {
                        lblStatus.Text = "����ʧ�ܣ�����ʹ��ԭʼ�������ء�";
                    }
                    // �����������ʧ�ܣ�ʹ��ԭʼURL��������
                    lblStatus.Text = "�������ӳ��ִ��󣬽�����ԭ������";
                    arguments = GetAria2cArguments(originalUrl, installerName, tempDirectory);
                    success = await RunAria2c(arguments, tempPath);
                }

                return success ? tempPath : null;
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"���� {installerName} ʱ����{ex.Message}";
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
                                txtOutput.AppendText("����: " + args.Data + Environment.NewLine);
                        }));
                    };

                    downloadProcess.Start();
                    downloadProcess.BeginOutputReadLine();
                    downloadProcess.BeginErrorReadLine();
                    await downloadProcess.WaitForExitAsync();

                    // ��������Ƿ���ֹ
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

                    // �ϲ���׼����ʹ������
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

                    // ȷ���첽�����ȡ�����
                    downloadProcess.CancelOutputRead();
                    downloadProcess.CancelErrorRead();

                    // �ȴ�������ȫ�˳�
                    downloadProcess.WaitForExit();

                    // ��������Ƿ���ֹ
                    if (isDownloadAborted)
                    {
                        return false;
                    }

                    return downloadProcess.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���� wget ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void UpdateProgressBarFromWgetOutput(string output)
        {
            // ���� wget ��������½�����
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
            MessageBox.Show($"PIPP�Ѿ�ֹͣά�����꣬�����ش洢Ͱ��������°�");
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
                MessageBox.Show("δ�ܻ�ȡ AutoStakkert ���������ӡ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("δ�ܻ�ȡ Siril ���������ӡ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // ����ҳ�� URL

            string pageUrl = "https://www.zwoastro.cn/downloads/";
            if (isTaskRunning)
            {
                MessageBox.Show("������������ִ���У���ȴ���ǰ�������", "����ִ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isTaskRunning = true; // ������������ִ��
            isDownloadAborted = false; // ����������ֹ��־

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // �������ͷ��ģ�������
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
                    client.DefaultRequestHeaders.Add("Referer", "https://www.zwoastro.cn/");

                    // ��ȡ����ҳ��� HTML ����
                    string html = await client.GetStringAsync(pageUrl);

                    // ʹ��������ʽ��ȡ���� 'ASCOMDriver' ����������
                    string pattern = @"<a\s+href=""(?<href>//dl\.zwoastro\.com/software\?app=ASCOMDrive[^""]+)"".*?class=""download"".*?>\s*<span.*?>����</span>\s*</a>";
                    var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string href = match.Groups["href"].Value;

                        // �����ӽ��� HTML ���룬ȥ�� &amp;
                        string decodedHref = WebUtility.HtmlDecode(href);

                        // ȷ�������� https: ��ͷ
                        string downloadUrl = decodedHref.StartsWith("http") ? decodedHref : "https:" + decodedHref;

                        // ���������а��� & ���ţ���Ҫ��˫����������
                        string quotedDownloadUrl = $"\"{downloadUrl}\"";

                        // ���ص��ļ���
                        string installerName = "ASI_ASCOM.exe";
                        string tempDirectory = Path.Combine(Application.StartupPath, "temp");
                        Directory.CreateDirectory(tempDirectory);
                        string savePath = Path.Combine(tempDirectory, installerName);

                        // wget.exe ��·��
                        string wgetPath = Path.Combine(Application.StartupPath, "bin", "wget.exe");

                        if (!File.Exists(wgetPath))
                        {
                            MessageBox.Show("δ�ҵ� wget.exe �ļ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // ���� wget ���������
                        string arguments = $"{quotedDownloadUrl} -O \"{savePath}\"";

                        lblStatus.Text = "�������� ASI ASCOM����...";
                        progressBar.Value = 0;

                        // ʹ�� wget ����
                        bool success = await RunWgetAsync(wgetPath, arguments);

                        if (success)
                        {
                            lblStatus.Text = $"ASI ASCOM�������سɹ�";
                            isTaskRunning = false;
                            // ִ�а�װ����
                            InstallSoftware(savePath);
                        }
                        else
                        {
                            isTaskRunning = false;
                            lblStatus.Text = "ASI ASCOM��������ʧ�ܡ�";
                        }
                    }
                    else
                    {
                        MessageBox.Show("δ�ҵ� ASI ASCOM�������������ӡ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���� ASI ASCOM����ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDownloadZWO_Camera_Driver_Click(object sender, EventArgs e)
        {
            if (isTaskRunning)
            {
                MessageBox.Show("������������ִ���У���ȴ���ǰ�������", "����ִ����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isTaskRunning = true; // ������������ִ��
            isDownloadAborted = false; // ����������ֹ��־

            // ����ҳ�� URL
            string pageUrl = "https://www.zwoastro.cn/downloads/";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // �������ͷ��ģ�������
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8");
                    client.DefaultRequestHeaders.Add("Referer", "https://www.zwoastro.cn/");

                    // ��ȡ����ҳ��� HTML ����
                    string html = await client.GetStringAsync(pageUrl);

                    // ʹ��������ʽ��ȡ���� 'AsiCameraDriver' ����������
                    string pattern = @"<a\s+href=""(?<href>//dl\.zwoastro\.com/software\?app=AsiCameraDriver[^""]+)"".*?class=""download"".*?>\s*<span.*?>����</span>\s*</a>";
                    var match = Regex.Match(html, pattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string href = match.Groups["href"].Value;

                        // �����ӽ��� HTML ���룬ȥ�� &amp;
                        string decodedHref = WebUtility.HtmlDecode(href);

                        // ȷ�������� https: ��ͷ
                        string downloadUrl = decodedHref.StartsWith("http") ? decodedHref : "https:" + decodedHref;

                        // ���������а��� & ���ţ���Ҫ��˫����������
                        string quotedDownloadUrl = $"\"{downloadUrl}\"";

                        // ���ص��ļ���
                        string installerName = "ZWO_CAM.exe";
                        string tempDirectory = Path.Combine(Application.StartupPath, "temp");
                        Directory.CreateDirectory(tempDirectory);
                        string savePath = Path.Combine(tempDirectory, installerName);

                        // wget.exe ��·��
                        string wgetPath = Path.Combine(Application.StartupPath, "bin", "wget.exe");

                        if (!File.Exists(wgetPath))
                        {
                            MessageBox.Show("δ�ҵ� wget.exe �ļ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // ���� wget ���������
                        string arguments = $"{quotedDownloadUrl} -O \"{savePath}\"";

                        lblStatus.Text = "�������� ZWO �������...";
                        progressBar.Value = 0;

                        // ʹ�� wget ����
                        bool success = await RunWgetAsync(wgetPath, arguments);

                        if (success)
                        {
                            lblStatus.Text = $"ZWO����������سɹ�";
                            isTaskRunning = false;
                            // ִ�а�װ����
                            InstallSoftware(savePath);
                        }
                        else
                        {
                            isTaskRunning = false;
                            lblStatus.Text = "ZWO�����������ʧ�ܡ�";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���� ZWO �������ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnDownloadD80_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"������ʼ����D80���ݿ⣬������������ٶ��������ֹ���ؽ��̲����´򿪳������أ�������Զ���ȡ���ؽ��ȣ�");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/d80_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "d80_star_database.exe");
        }

        private async void btnDownloadD50_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"������ʼ����D50���ݿ⣬������������ٶ��������ֹ���ؽ��̲����´򿪳������أ�������Զ���ȡ���ؽ��ȣ�");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/d50_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "d50_star_database.exe");
        }

        private async void btnDownloadD20_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"������ʼ����D20���ݿ⣬������������ٶ��������ֹ���ؽ��̲����´򿪳������أ�������Զ���ȡ���ؽ��ȣ�");
            string downloadUrl = "https://pub-39919e88d00748698d43cf477d7d7626.r2.dev/d20_star_database.exe";
            await DownloadAndInstallSoftware(downloadUrl, "d20_star_database.exe");
        }
        private async void btnDownloadG05_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"������ʼ����G05���ݿ⣬������������ٶ��������ֹ���ؽ��̲����´򿪳������أ�������Զ���ȡ���ؽ��ȣ�");
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
            MessageBox.Show($"��ע�⣬Kstars�����ǵĴ洢Ͱ���棬�п��ܲ������°汾");
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
                MessageBox.Show($"��ȡ���ص�ַʧ�ܣ�{ex.Message}");
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
                    // ʹ��������ʽƥ����������
                    Match match = Regex.Match(html, @"<a[^>]*href=""(?<url>https://github\.com/rmorgan001/GSServer/releases/download/[^""]+\.zip)""[^>]*>\s*<u>Download GS Server v[\d\.]+</u>\s*</a>", RegexOptions.IgnoreCase);
                    return match.Success ? match.Groups["url"].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ GS Server ���ص�ַʧ�ܣ�{ex.Message}");
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
                    // ʹ��������ʽƥ����������
                    Match match = Regex.Match(html, @"<a\s+href=""(?<url>winjupos_setup_[^""]+\.exe)"">WinJUPOS\s+setup\s+file[^<]*</a>", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        // ������������������
                        string baseUrl = "https://jupos.org/gh/";
                        string downloadUrl = baseUrl + match.Groups["url"].Value;
                        return downloadUrl;
                    }
                    else
                    {
                        MessageBox.Show("δ�ҵ� WinJUPOS ���������ӡ�");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ WinJUPOS ���ص�ַʧ�ܣ�{ex.Message}");
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

                    // ʹ��������ʽƥ���µ��������Ӹ�ʽ
                    Match match = Regex.Match(html, @"href\s*=\s*""(?<url>https?://[^""]+\.exe)""", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        return match.Groups["url"].Value;
                    }
                    else
                    {
                        MessageBox.Show("δ�ҵ� Siril ���������ӡ�");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ Siril ���ص�ַʧ�ܣ�{ex.Message}");
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
                    // ʹ��������ʽƥ����������
                    Match match = Regex.Match(html, @"<a\s+href=""(?<url>https://www\.astrokraai\.nl/software/AutoStakkert_\d+\.\d+\.\d+_x64\.zip)"">\s*AutoStakkert\s*\d+\.\d+\.\d+\s*</a>", RegexOptions.IgnoreCase);
                    return match.Success ? match.Groups["url"].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ AutoStakkert ���ص�ַʧ�ܣ�{ex.Message}");
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
                    // ʹ��������ʽƥ����������
                    Match match = Regex.Match(html, @"<a\s+href=""(?<url>http://www\.stellarjourney\.com/assets/downloads/OnStep-Setup-[^""]+\.zip)"">download here</a>", RegexOptions.IgnoreCase);
                    return match.Success ? match.Groups["url"].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ ONSTEP �������ص�ַʧ�ܣ�{ex.Message}");
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
                    // ʹ��������ʽƥ����������
                    Match match = Regex.Match(html, @"<a\s+href\s*=\s*""(https://github.com/ASCOMInitiative/ASCOMPlatform/releases/download/[^""]+)""\s+target\s*=\s*""_self""");
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡASCOM���ص�ַʧ�ܣ�{ex.Message}");
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
                    // ʹ��������ʽƥ����������
                    string pattern = @"<a\s+href=""(?<url>https://github\.com/Stellarium/stellarium/releases/download/[^""]+qt6-win64\.exe)"">Windows<span>x86_64; Qt6; Windows 10\+</span></a>";
                    Match match = Regex.Match(html, pattern);
                    return match.Success ? match.Groups[1].Value : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡASCOM���ص�ַʧ�ܣ�{ex.Message}");
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

                    // ƥ�� Windows �汾����������
                    Match match = Regex.Match(html, @"<a\s+href\s*=\s*""([^""]+\.exe)""\s+class\s*=\s*""button"">\s*Download.*Windows\s*</a>", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string relativeUrl = match.Groups[1].Value;
                        // ƴ����������������
                        string downloadUrl = new Uri(new Uri(pageUrl), relativeUrl).ToString();
                        return downloadUrl;
                    }
                    else
                    {
                        lblStatus.Text = "δ�ҵ� PHD2 ���������ӡ�";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ PHD2 ���ص�ַʧ�ܣ�{ex.Message}");
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

                    // ʹ��������ʽƥ����������
                    Match match = Regex.Match(html, @"<a\s+href=""([^""]+\.exe)"">Latest Ver\.\d+�������ȶ��棩</a>");

                    if (match.Success)
                    {
                        string downloadUrl = match.Groups[1].Value;
                        return downloadUrl;
                    }
                    else
                    {
                        lblStatus.Text = "δ�ҵ� QHYCCD �������������ӡ�";
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"��ȡ QHYCCD ���ص�ַʧ�ܣ�{ex.Message}");
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
                        downloadProcess.Kill(); // ǿ����ֹ���ؽ���
                    }

                    // ȡ���첽��ȡ
                    downloadProcess.CancelOutputRead();
                    downloadProcess.CancelErrorRead();

                    // �ͷ���Դ
                    downloadProcess.Close();
                    downloadProcess = null;

                    lblStatus.Text = "��������ֹ��";
                    isTaskRunning = false;
                    isDownloadAborted = true; // ������������ֹ�ı�־
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"��ֹ����ʱ��������{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("û������ִ�е�����", "������", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    lblStatus.Text = "������ wub.exe��";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"���� wub.exe ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("δ�ҵ� wub.exe �ļ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void �༭Aria2����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string configFilePath = Path.Combine(Application.StartupPath, "config", "downloadcfg.ini");
            var configForm = new ConfigForm(configFilePath);
            configForm.ShowDialog();

            // ���¼�������
            aria2cConfig.Clear();
            LoadAria2cConfig();
        }
        private async void �ֶ�����Aria2����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ������������û�������������
            string downloadUrl = Prompt.ShowDialog("�������������ӣ�", "�ֶ�����Aria2����");
            if (string.IsNullOrWhiteSpace(downloadUrl))
            {
                MessageBox.Show("δ��������", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ��ȡ�ļ���
            string installerName = Path.GetFileName(new Uri(downloadUrl).AbsolutePath);
            if (string.IsNullOrEmpty(installerName))
            {
                MessageBox.Show("�޷��������л�ȡ�ļ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ���屣��·��Ϊ UserDownload Ŀ¼
            string userDownloadPath = Path.Combine(Application.StartupPath, "UserDownload");
            Directory.CreateDirectory(userDownloadPath);
            string savePath = Path.Combine(userDownloadPath, installerName);

            // ���� aria2c ����
            string arguments = GetAria2cArguments(downloadUrl, installerName, userDownloadPath);

            // ִ������
            bool success = await RunAria2c(arguments, savePath);

            if (success)
            {
                lblStatus.Text = $"�ļ����سɹ�";
                MessageBox.Show($"�ļ��ѳɹ����ص���{savePath}", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblStatus.Text = "����ʧ�ܡ�";
                MessageBox.Show("���ع����г��ִ���", "����ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ����NINA���Ŀ¼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ��ȡ��ǰ�û��ı���Ӧ������·��
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // �������Ŀ¼������·��
                string pluginsPath = Path.Combine(localAppData, "NINA", "Plugins");

                // ���Ŀ¼�Ƿ����
                if (Directory.Exists(pluginsPath))
                {
                    // ��Ŀ¼
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = pluginsPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("δ�ҵ� NINA ���Ŀ¼��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�� NINA ���Ŀ¼ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ����Siril���Ŀ¼ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // ��ȡ��ǰ�û�������Ӧ������·��
                string roamingAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // ���� Siril ���Ŀ¼������·��
                string scriptsPath = "C:\\Program Files\\Siril\\scripts";

                // ���Ŀ¼�Ƿ����
                if (Directory.Exists(scriptsPath))
                {
                    // ��Ŀ¼
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = scriptsPath,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show("δ�ҵ� Siril ���Ŀ¼��", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�� Siril ���Ŀ¼ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ľ������ʱ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JupiterCalculationForm calcForm = new JupiterCalculationForm();
            calcForm.ShowDialog();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        //����������Ӱ���������Ѿ����ԭ����@Garth��Ȩ����Ŀ��ַ��https://github.com/GarthTB/AstrophotoCalculator
        private void ������Ӱ������ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    lblStatus.Text = "������ AstrophotoCalculator.exe��";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"���� AstrophotoCalculator.exe ʱ����{ex.Message}", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("δ�ҵ� AstrophotoCalculator.exe �ļ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}