using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MentimeterBot
{
    class Log
    {

        private RichTextBox richTextBox;

        public Log(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
        }

        public void addLog(Color TextColor, string EventText)
        {
            if (richTextBox.InvokeRequired)
            {
                richTextBox.BeginInvoke(new Action(delegate
                {
                    addLog(TextColor, EventText);
                }));
                return;
            }

            string nDateTime = DateTime.Now.ToString("hh:mm:ss") + " - ";

            // color text.
            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.SelectionColor = TextColor;

            // newline if first line, append if else.
            if (richTextBox.Lines.Length == 0)
            {
                richTextBox.AppendText(nDateTime + EventText);
                richTextBox.ScrollToCaret();
                richTextBox.AppendText(System.Environment.NewLine);
            }
            else
            {
                richTextBox.AppendText(nDateTime + EventText + System.Environment.NewLine);
                richTextBox.ScrollToCaret();
            }
        }
    }
}
