/*---------------------------------------------------------------------------------
   This source file is a part of Feed Launch .NET
   
   For the latest information, please visit http://feedlaunch.sourceforge.net/
    
   Copyright (C) 2007 The Feed Launch .NET Team

   This program is free software; you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 2 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License along
   with this program; if not, write to the Free Software Foundation, Inc.,
   51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
---------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FeedLaunch.NET;
using FeedCreator.NET;

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
            //We will now add an event to locationbox which will call an event which checks
            //if the user hit "return" or "enter." In either case, this indicates that the
            //browser control's url should be updated/refreshed
            this.locationBox.KeyPress +=new KeyPressEventHandler(locationBox_KeyPress);
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
            try
            {
                webBrowser1.Url = new System.Uri(locationBox.Text, UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
            }
        }
        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            //Update the form's title text
            this.Text = this.webBrowser1.DocumentText + " - FeedLaunch Explorer";
        }
        private void locationBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    webBrowser1.Url = new System.Uri(locationBox.Text, UriKind.RelativeOrAbsolute);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}