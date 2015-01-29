using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MentimeterBot
{
    class BrowserAutomation
    {
        private string link = "";
        private WebBrowser webBrowser;
        private int goVoteCode;

        public BrowserAutomation(WebBrowser webBrowser, string link, int goVoteCode)
        {
            this.webBrowser = webBrowser;
            this.link = link;
            this.goVoteCode = goVoteCode;
        }

        public void navigateToGovote()
        {
            webBrowser.Navigate(link);
            waitForPageToLoad();
        }

        public void enterCodeAndValidate()
        {
            var codeElem = webBrowser.Document.GetElementsByTagName("input")["id"];
            codeElem.SetAttribute("value", goVoteCode.ToString());

            var buttonSubmit = webBrowser.Document.GetElementById("getQuestion");
            buttonSubmit.InvokeMember("click");
            waitForPageToLoad();
        }

        public void chooseRadioButton()
        {
            webBrowser.Document.InvokeScript("applyChanges");
            waitForPageToLoad();
            HtmlElementCollection radioElems = webBrowser.Document.GetElementsByTagName("input");

            foreach (HtmlElement element in radioElems)
            {
                MessageBox.Show(element.Id);
                /*
                if (element.InnerHtml.Contains("Send Invitations"))
                {
                    element.InvokeMember("click");
                }*/
            }
        }

        private void waitForPageToLoad() 
        {
            while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
        }

    }
}
