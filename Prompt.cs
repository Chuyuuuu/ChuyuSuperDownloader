using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AstronomySoftwareDownloader
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            return PromptForm.ShowPrompt(text, caption);
        }
    }
}
