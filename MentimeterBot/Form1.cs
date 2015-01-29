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

        private static int voted = 0;
        public Form1()
        {
            InitializeComponent();

            if (!webBrowser1.Visible)
            {
                richTextBoxLog.Location = new System.Drawing.Point(12, 12);
                richTextBoxLog.Size = new System.Drawing.Size(543, 315);
            }
            Log log = new Log(richTextBoxLog);
            log.addLog(Color.Black, "------------------------------------------------------------------------");
            log.addLog(Color.Black, "Hello, welcome to MentimeterBot, the automatic voting bot for Mentimeter");
            log.addLog(Color.Black, "------------------------------------------------------------------------");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }


        unsafe private void buttonStart_Click(object sender, EventArgs e)
        {
            Log log = new Log(richTextBoxLog);
            string status = buttonStart.Text;

            string goVoteCode = textBoxCode.Text;
            int nbrOfVotes = 0;
            string link = "https://www.govote.at";

            bool isNumeric = int.TryParse(textBoxNbrOfVotes.Text, out nbrOfVotes);


            if (goVoteCode.Equals(""))
            {
                log.addLog(Color.Red, "Please enter the code obtained in the url!");
                log.addLog(Color.Red, "To obtain this code go to govote.at, enter the code given by the presenter and click submit. Now copy the ending of the new URL.");
            }
            else if (textBoxNbrOfVotes.Text.Equals(""))
            {
                log.addLog(Color.Red, "Please enter the number of times you would like to vote!");

            }
            else if (!isNumeric)
            {
                log.addLog(Color.Red, "The number entered for the number of votes is not valid!");
            }
            else
            {

                if (status.Equals("Start"))
                {
                    log.addLog(Color.Green, "Starting program.");
                    log.addLog(Color.Green, "Going to vote " + nbrOfVotes + " times.");
                    log.addLog(Color.Green, "Voting at " + link + "/" + goVoteCode + ".");
                    

                    BrowserAutomation browserAutomation = new BrowserAutomation(webBrowser1, link, goVoteCode);

                    buttonStart.Text = "Stop";
                    progressBar1.Value = 0;
                    progressBar1.Maximum = nbrOfVotes;
                    progressBar1.Minimum = 0;
                    progressBar1.Step = 1;
                    for (voted = 0; voted < nbrOfVotes; voted++)
                    {
                        //delete cookies
                        int option = (int)3/* INTERNET_SUPPRESS_COOKIE_PERSIST*/;
                        int* optionPtr = &option;

                        bool success = InternetSetOption(0, 81/*INTERNET_OPTION_SUPPRESS_BEHAVIOR*/, new IntPtr(optionPtr), sizeof(int));
                        if (!success)
                        {
                            log.addLog(Color.Red, "Error: Could not clear cookies!");
                            log.addLog(Color.Red, "Stopping!");
                            buttonStart.Text = "Start";
                            break;
                        }

                        //start voting
                        browserAutomation.navigateToGovote();
                        browserAutomation.chooseRadioButton(1);
                        browserAutomation.submitAnswerRadioButton();

                        //stop when we click the stop button
                        status = buttonStart.Text;
                        if (status.Equals("Start"))
                        {
                            break;
                        }

                        progressBar1.PerformStep();
                    }
                    log.addLog(Color.Green, "Done voting! :)");
                    log.addLog(Color.Green, "Total number of votes was: " + voted);
                    buttonStart.Text = "Start";
                }
                else if (status.Equals("Stop"))
                {
                    buttonStart.Text = "Start";
                    log.addLog(Color.Green, "Stopping program.");
                    log.addLog(Color.Green, "Total number of votes was: " + voted);
                }
            }

        }

        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (webBrowser1.Visible)
            {
                webBrowser1.Visible = false;
                richTextBoxLog.Location = new System.Drawing.Point(12, 12);
                richTextBoxLog.Size = new System.Drawing.Size(543, 315);
            }
            else if (!webBrowser1.Visible)
            {
                webBrowser1.Visible = true;
                this.webBrowser1.Location = new System.Drawing.Point(12, 12);
                this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
                this.webBrowser1.Size = new System.Drawing.Size(543, 143);

                this.richTextBoxLog.Location = new System.Drawing.Point(12, 161);
                this.richTextBoxLog.Size = new System.Drawing.Size(543, 172);
            }
        }

    }
}
