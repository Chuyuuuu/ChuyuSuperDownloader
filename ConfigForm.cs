using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    public partial class ConfigForm : Form
    {
        private string configFilePath;
        private Dictionary<string, string> aria2cConfig = new Dictionary<string, string>();

        public ConfigForm(string configFilePath)
        {
            InitializeComponent();
            this.configFilePath = configFilePath;
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (File.Exists(configFilePath))
            {
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

                // 加载配置到控件
                txtMaxNumCon.Text = aria2cConfig.ContainsKey("MaxNumCon") ? aria2cConfig["MaxNumCon"] : "16";
                txtSplit.Text = aria2cConfig.ContainsKey("Split") ? aria2cConfig["Split"] : "16";
                txtMinSplitSize.Text = aria2cConfig.ContainsKey("MinSplitSize") ? aria2cConfig["MinSplitSize"] : "1M";
            }
            else
            {
                // 如果配置文件不存在，使用默认值
                txtMaxNumCon.Text = "16";
                txtSplit.Text = "16";
                txtMinSplitSize.Text = "1M";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 更新配置
            aria2cConfig["MaxNumCon"] = txtMaxNumCon.Text.Trim();
            aria2cConfig["Split"] = txtSplit.Text.Trim();
            aria2cConfig["MinSplitSize"] = txtMinSplitSize.Text.Trim();

            // 保存到文件
            var lines = new List<string>();
            foreach (var kvp in aria2cConfig)
            {
                lines.Add($"{kvp.Key} = {kvp.Value}");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(configFilePath));
            File.WriteAllLines(configFilePath, lines);

            MessageBox.Show("配置已保存", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
