using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentimeterBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string link = "https://www.govote.at";
            int goVoteCode = Convert.ToInt32(textBoxCode.Text);
            BrowserAutomation browserAutomation = new BrowserAutomation(webBrowser1, link, goVoteCode);
            browserAutomation.navigateToGovote();
            browserAutomation.enterCodeAndValidate();
            browserAutomation.chooseRadioButton(); //we only handle the radiobuttons for now
        }


    }
}
