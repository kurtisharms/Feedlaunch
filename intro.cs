using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace FeedLaunch.NET
{
    public partial class intro : Form
    {
        int page;

        public delegate void CustomEventDelegate(object sender, EventArgs e);
        public event CustomEventDelegate introClosed;
        public intro()
        {
            InitializeComponent();
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            if (this.introClosed != null)
            {
                introClosed(sender, e);
            }
            this.Dispose();
        }

        private void intro_Load(object sender, EventArgs e)
        {
            page = 1;
            webBrowser1.Visible = false;
            label1.Visible = true;
            label2.Visible = false;
            backButton.Enabled = false;
            skipButton.Text = "Skip";
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            if (page == 1)
            {
                label1.Visible = false;
                webBrowser1.Visible = true;
                string s = Application.StartupPath;
                s = s.Replace("\\", "/");
                s = "file:///" + s;
                try
                {
                    Uri url = new Uri(s + "/feedlaunch.html");
                    webBrowser1.Url = url;
                }
                catch
                {
                }
                backButton.Enabled = true;
                page = 2;
            }
            else if (page == 2)
            {
                webBrowser1.Visible = false;
                label1.Visible = false;
                forwardButton.Enabled = false;
                skipButton.Text = "Done";
                label2.Visible = true;
                page = 3;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (page == 2)
            {
                label1.Visible = true;
                label2.Visible = false;
                webBrowser1.Visible = false;
                backButton.Enabled = false;
                forwardButton.Enabled = true;
                skipButton.Text = "Skip";
                page = 1;
            }
            if (page == 3)
            {
                label1.Visible = false;
                label2.Visible = false;
                webBrowser1.Visible = true;
                string s = Application.StartupPath;
                s = s.Replace("\\", "/");
                s = "file:///" + s;
                try
                {
                    Uri url = new Uri(s + "/feedlaunch.html");
                    webBrowser1.Url = url;
                }
                catch
                {
                }
                backButton.Enabled = true;
                forwardButton.Enabled = true;
                skipButton.Text = "Skip";
                page = 2;
            }
        }
    }
}