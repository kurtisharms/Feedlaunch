using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FeedCreator.NET
{
    public partial class explorer : Form
    {
        //this public Uri is updated by the mainform before the explorer
        //form is displayed. It should always contain a valid URL
        public Uri urlLocation;
        public explorer()
        {
            InitializeComponent();
        }

        private void explorer_Load(object sender, EventArgs e)
        {
            //update the browser location
            this.webBrowser1.Url = this.urlLocation;
            //set the title text
            this.Text = "FeedLaunch Explorer";
            //now we will wire an even handler so that whenever the browser's
            //status text changes, the event call backButton_Click which updates
            //the status label
            this.webBrowser1.StatusTextChanged += new EventHandler(webBrowser1_StatusTextChanged);
            //This event will trigger whenever the document is finished loading
            //We will use it for indicating that the form's title needs to be
            //updated
            this.webBrowser1.DocumentTitleChanged +=new EventHandler(webBrowser1_DocumentTitleChanged);
        }
        private void webBrowser1_StatusTextChanged(object sender, EventArgs e)
        {
            //Update the status text
            this.statusLabel.Text = this.webBrowser1.StatusText;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            //Navigate the web page "Back"
            webBrowser1.GoBack();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            //Navigate the web page "Forward"
            webBrowser1.GoForward();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            //the user clicked "Go" so update the webbrowser control
            webBrowser1.Url = new System.Uri(locationBox.Text, UriKind.RelativeOrAbsolute);
        }
        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            //Update the form's title text
            this.Text = this.webBrowser1.DocumentText + " - FeedLaunch Explorer";
        }
    }
}