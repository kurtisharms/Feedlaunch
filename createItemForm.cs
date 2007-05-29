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

namespace FeedCreator.NET
{
    public partial class createItemForm : Form
    {
        public delegate void CustomEventDelegate(object sender, EventArgs e);
        public event CustomEventDelegate CreateFeedItem;
        public event CustomEventDelegate CancelCreateFeedItem;
        public event CustomEventDelegate EditFeedItem;
        public string titleOnLoad = null;
        public createItemForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool exit = false;
            errorProvider1.Clear();
            if (titleBox.Text == "")
            {
                errorProvider1.SetError(titleBox, "Title can't be empty!");
                exit = true;
            }
            if (descriptionBox.Text == "")
            {
                errorProvider1.SetError(descriptionBox, "Description can't be empty!");
                exit = true;
            }
            if (linkBox.Text == "")
            {
                errorProvider1.SetError(linkBox, "Link can't be empty!");
                exit = true;
            }
            if (authorEmail.Text != "" && authorEmail.Text != null)
            {
                if (authorEmail.Text.Contains("@") != true)
                {
                    errorProvider1.SetError(authorEmail, "Invalid Email address!");
                    exit = true;
                }
            }
            
            if (exit != true)
            {
                if (this.CreateFeedItem != null)
                {
                    if (this.Text == "Edit Feed Item")
                    {
                        this.EditFeedItem(sender, e);
                    }
                    else
                    {
                        this.CreateFeedItem(sender, e);
                    }
                }
                this.Dispose();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            CancelCreateFeedItem(sender, e);
            this.Dispose();
        }

        private void createItemForm_Load(object sender, EventArgs e)
        {
            titleOnLoad = this.titleBox.Text;
        }
    }
}