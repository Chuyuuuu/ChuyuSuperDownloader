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

                // �������õ��ؼ�
                txtMaxNumCon.Text = aria2cConfig.ContainsKey("MaxNumCon") ? aria2cConfig["MaxNumCon"] : "16";
                txtSplit.Text = aria2cConfig.ContainsKey("Split") ? aria2cConfig["Split"] : "16";
                txtMinSplitSize.Text = aria2cConfig.ContainsKey("MinSplitSize") ? aria2cConfig["MinSplitSize"] : "1M";
            }
            else
            {
                // ��������ļ������ڣ�ʹ��Ĭ��ֵ
                txtMaxNumCon.Text = "16";
                txtSplit.Text = "16";
                txtMinSplitSize.Text = "1M";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // ��������
            aria2cConfig["MaxNumCon"] = txtMaxNumCon.Text.Trim();
            aria2cConfig["Split"] = txtSplit.Text.Trim();
            aria2cConfig["MinSplitSize"] = txtMinSplitSize.Text.Trim();

            // ���浽�ļ�
            var lines = new List<string>();
            foreach (var kvp in aria2cConfig)
            {
                lines.Add($"{kvp.Key} = {kvp.Value}");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(configFilePath));
            File.WriteAllLines(configFilePath, lines);

            MessageBox.Show("�����ѱ���", "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
