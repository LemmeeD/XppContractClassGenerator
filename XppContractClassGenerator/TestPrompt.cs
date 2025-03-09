using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XppContractClassGenerator
{
    public static class TestPrompt
    {
        public static DialogResult ShowDialog(string text)
        {
            Form prompt = new Form()
            {
                Width = 2000,
                Height = 2000,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Prompt",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 50, Text = text, Height = 1900, Width = 1900 };
            prompt.Controls.Add(textLabel);

            return prompt.ShowDialog();
        }
    }
}
