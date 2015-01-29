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
        //ddb0b5
        private string link = "";
        private WebBrowser webBrowser;
        private string goVoteCode;

        public BrowserAutomation(WebBrowser webBrowser, string link, string goVoteCode)
        {
            this.webBrowser = webBrowser;
            this.link = link;
            this.goVoteCode = goVoteCode;
        }

        public void navigateToGovote()
        {
            webBrowser.Navigate(link + "/" + goVoteCode);
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

        public void chooseRadioButton(int radiobuttonNumber)
        {
            int radioButtonsEncountered = 0;
            waitForPageToLoad();
            HtmlElementCollection radioElems = webBrowser.Document.GetElementsByTagName("input");

            foreach (HtmlElement element in radioElems)
            {
                if (element.Name == "vote")
                {
                    radioButtonsEncountered++;

                    if (radioButtonsEncountered == radiobuttonNumber)
                    {
                        element.InvokeMember("click");
                    }
                }
            }
        }

        public void submitAnswerRadioButton()
        {
            var elements = webBrowser.Document.GetElementsByTagName("input");
            foreach (HtmlElement element in elements)
            {
                if (element.GetAttribute("value").Equals("Submit answer"))
                {
                    element.InvokeMember("click");
                }
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
