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

        //dll loading for clearing cookies
        [System.Runtime.InteropServices.DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);


        public Form1()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        
        unsafe private void buttonStart_Click(object sender, EventArgs e)
        {
            string link = "https://www.govote.at";
            string goVoteCode = textBoxCode.Text;
            BrowserAutomation browserAutomation = new BrowserAutomation(webBrowser1, link, goVoteCode);

            int option = (int)3/* INTERNET_SUPPRESS_COOKIE_PERSIST*/;
            int* optionPtr = &option;

            bool success = InternetSetOption(0, 81/*INTERNET_OPTION_SUPPRESS_BEHAVIOR*/, new IntPtr(optionPtr), sizeof(int));
            if (!success)
            {
                MessageBox.Show("Could not clear cookies");
            }
            
            browserAutomation.navigateToGovote();
            browserAutomation.chooseRadioButton(1);
            browserAutomation.submitAnswerRadioButton();

        }


    }
}
