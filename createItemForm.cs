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
        public event CustomEventDelegate EditFeedItem;
        private string titleOnLoad = null;
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
            this.Dispose();
        }

        private void createItemForm_Load(object sender, EventArgs e)
        {
            titleOnLoad = this.titleBox.Text;
        }
    }
}