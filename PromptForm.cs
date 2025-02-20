using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    public partial class PromptForm : Form
    {
        public PromptForm(string text, string caption)
        {
            InitializeComponent();
            lblPrompt.Text = text;
            this.Text = caption;
        }

        public string InputText
        {
            get { return txtInput.Text.Trim(); }
        }

        public static string ShowPrompt(string text, string caption)
        {
            using (var prompt = new PromptForm(text, caption))
            {
                var result = prompt.ShowDialog();
                return result == DialogResult.OK ? prompt.InputText : string.Empty;
            }
        }
    }
}
