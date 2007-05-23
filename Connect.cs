using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FeedLaunch.NET;
using FeedCreator.NET;
using FtpLib;

namespace FeedCreator.NET
{
    public partial class UploadForm : Form
    {
        public string strFileName = "";
        int uploadTry = 1;
        FTPConnect UploadConnection;
        public UploadForm()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {
            UploadConnection = new FTPConnect();
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool run = true;
            if(serverTextBox.Text == "")
            {
                errorProvider1.SetError(serverTextBox, "Server name cannot be empty!");
                run = false;
            }
            if(usernameTextBox.Text == "")
            {
                errorProvider1.SetError(usernameTextBox, "Username cannot be empty!");
            run = false;
            }
            if(passwordTextBox.Text == "")
            {
                errorProvider1.SetError(passwordTextBox, "Password cannot be empty!");
                run = false;
            }
            try
            {
                int i = Convert.ToInt32(portTextBox.Text);
            }
            catch(Exception ex)
            {
                errorProvider1.SetError(portTextBox, "Port number is invalid!");
                run = false;
            }
            /*if(fileName.Text.Contains("rss") == false && fileName.Text.Contains("xml") == false && fileName.Text.Contains(""))
            {
                errorProvider1.SetError(fileName, "Invalid Feed filename. Extension must be \"xml\" or \"rss\"!");
                run = false;
            }*/
            if(run == true)
            {
                upload(sender, e);
            }
                
                
        }
        private void upload(object sender, EventArgs e)
        {
            try
            {
                string strTMP = directoryPath.Text.Trim();
                progressBar1.Value = 0;
                progressBar1.Increment(15);
                UploadConnection = new FTPConnect();
                UploadConnection.setRemoteHost(serverTextBox.Text);
                UploadConnection.setRemoteUser(usernameTextBox.Text);
                UploadConnection.setRemotePass(passwordTextBox.Text);
                UploadConnection.setRemotePort(Convert.ToInt32(portTextBox.Text));
                UploadConnection.login();
                if (directoryPath.Text != "" || strTMP != "/")
                {
                    UploadConnection.chdir(directoryPath.Text);
                }
                progressBar1.Increment(10);
                UploadConnection.upload(strFileName);
                progressBar1.Increment(25);
                UploadConnection.close();
            }
            catch (Exception ex)
            {
                if (uploadTry < 11)
                {
                    uploadTry = uploadTry + 1;
                    upload(sender, e);
                }
                else
                {
                    MessageBox.Show("Connection Failed! It is possible that the FTP server you specified is unable to handle more connections currently, in which case try again later. Also verify your username, password, connection port, and desired output directory.", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                }
            }
            if (uploadTry < 11)
            {
                MessageBox.Show("Feed successfully uploaded!", "Upload Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Dispose();
            }
            uploadTry = 1;
            progressBar1.Value = 0;
            
        }
    }
}