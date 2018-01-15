using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace FeedCreator.NET
{
    public partial class newChannelForm : Form
    {
        public delegate void CustomEventDelegate(object sender, EventArgs e);
        public event CustomEventDelegate CreateFeed;
        public newChannelForm()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
         
            errorProvider1.Clear();
            bool exit = false;
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
            if (linkBox.Text.Contains("http://") == false && linkBox.Text.Contains("ftp:") == false && linkBox.Text.Contains("news:") == false && linkBox.Text.Contains("https://") == false && linkBox.Text.Contains("www.") == false && linkBox.Text.Contains(".com") == false && linkBox.Text.Contains(".org") == false && linkBox.Text.Contains(".net") == false && linkBox.Text.Contains(".ca") == false)
            {
                SystemSounds.Beep.Play();
                errorProvider1.SetError(linkBox, "Invalid Link!");
                MessageBox.Show("Please enter a valid web/link address!", "Invalid Link!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                exit = true;
            }
            else if(linkBox.Text == "")
            {
                errorProvider1.SetError(linkBox, "Link can't be empty!");
                exit = true;
            }
            if (exit == false)
            {
                if (this.CreateFeed != null)
                {
                    this.CreateFeed(sender, e);
                }
                this.Dispose();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void newChannelForm_Load(object sender, EventArgs e)
        {

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Gif Images (*.gif)|*.gif|JPEG Images (*.jpg)|*.jpg|PNG Images (*.png)|*.png";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Title = "Select an Image to add to feed-";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (openFileDialog1.FileName != null)
                {
                    this.imageText.Text = openFileDialog1.FileName;
                }
            }
        }
    }
}