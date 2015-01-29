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
            string goVoteCode = textBoxCode.Text;
            BrowserAutomation browserAutomation = new BrowserAutomation(webBrowser1, link, goVoteCode);
            webBrowser1.Navigate("javascript:void((function(){var a,b,c,e,f;f=0;a=document.cookie.split('; ');for(e=0;e<a.length&&a[e];e++){f++;for(b='.'+location.host;b;b=b.replace(/^(?:%5C.|[^%5C.]+)/,'')){for(c=location.pathname;c;c=c.replace(/.$/,'')){document.cookie=(a[e]+'; domain='+b+'; path='+c+'; expires='+new Date((new Date()).getTime()-1e11).toGMTString());}}}})())");
            browserAutomation.navigateToGovote();
            browserAutomation.chooseRadioButton(1);
            browserAutomation.submitAnswerRadioButton();
        }


    }
}
