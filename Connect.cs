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
using System.Threading;
using FeedLaunch.NET;
using FeedCreator.NET;
using System.IO;
using FtpLib;

namespace FeedCreator.NET
{
    public partial class UploadForm : Form
    {
        public string strFileName = "";
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
            //Now we check to load the data file. If the data file does not exist,
            //then there is no password stores. 
   
            try
            {
                FileStream testfs = new FileStream("store.dat", FileMode.Open, FileAccess.Read);
      			// HERE WE SHOULD LOAD SAVED PASSWORDS
 
      			StreamReader sr = new StreamReader(testfs);
      			string line = "";
      			bool passedOnce = false;
      			while ((line = sr.ReadLine()) != null) 
                {
      				if(!passedOnce)
      				{
      					usernameTextBox.Text = line.Trim();
      					passedOnce = true;
      				}
      				else
      				{
      					passwordTextBox.Text = line.Trim();
      				}
      					
                    //HERE WE NEED TO LOAD USERNAME AND PASSWORD
                }
      			sr.Close();
                testfs.Close();
            }
            //If the file does NOT exist, then create it and open the intro window
            catch (System.IO.FileNotFoundException ex)
            {
                //Note that we set FileMode.OpenOrCreate.
                FileStream fs = new FileStream("store.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs);
                //Finally, close both the file and writer streams
                sw.Close();
                fs.Close();
            }
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
            
            try
            {
                FileStream testfs = new FileStream("store.dat", FileMode.Open, FileAccess.Read);
      			// HERE WE SHOULD LOAD SAVED PASSWORDS
 
      			StreamReader sr = new StreamReader(testfs);
      			string line = "";
      			bool passedOnce = false;
      			bool creditSame = true;
      			while ((line = sr.ReadLine()) != null) 
                {
      				if(!passedOnce)
      				{
      					if(usernameTextBox.Text.Trim() != line.Trim())
      						creditSame = false;
      				}
      				else
      				{
      					if(passwordTextBox.Text.Trim() != line.Trim())
      						creditSame = false;
      				}
      					
      				if(creditSame == false)
      				{
      					DialogResult result = MessageBox.Show("Do you wish to save your username and password?", "Username and Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    	if (result == DialogResult.Yes)
                    	{
                    		try {
                    			//Note that we set FileMode.OpenOrCreate.
                				FileStream fs = new FileStream("store.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                				StreamWriter sw = new StreamWriter(fs);
                				//We write out the application data
                				sw.WriteLine(usernameTextBox.Text.Trim());
                				sw.WriteLine(passwordTextBox.Text.Trim());
                				//Finally, close both the file and writer streams
                				sw.Close();
                				fs.Close();
                    		}
                    		catch (Exception ex)
                    		{
                    		}

                    	}
      				}
                }
      			sr.Close();
                testfs.Close();
            }
            catch (Exception ex)
            {
            }
            
            
            if(run == true)
            {
                upload();
            }
                
                
        }

            //Thread uploadThread = new Thread(new ThreadStart(upload));
            //uploadThread.Start();

        private void upload()
        {
            string strTMP = directoryPath.Text.Trim();
            try
            {
                progressBar1.Value = 0;
                progressBar1.Increment(15);
                UploadConnection = new FTPConnect();
                UploadConnection.setRemoteHost(serverTextBox.Text);
                UploadConnection.setRemoteUser(usernameTextBox.Text);
                UploadConnection.setRemotePass(passwordTextBox.Text);
                UploadConnection.setRemotePort(Convert.ToInt32(portTextBox.Text));
                UploadConnection.login();
                progressBar1.Increment(20);
                if (strTMP != "" && strTMP != "/")
                {
                    UploadConnection.chdir(strTMP);
                }
                UploadConnection.upload(strFileName);
                progressBar1.Increment(35);
                UploadConnection.close();
                MessageBox.Show("Feed successfully uploaded!", "Upload Successful!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Dispose();
            }
            catch (Exception ex)
            {
                try
                {
                    UploadConnection = new FTPConnect();
                    UploadConnection.setRemoteHost(serverTextBox.Text);
                    UploadConnection.setRemoteUser(usernameTextBox.Text);
                    UploadConnection.setRemotePass(passwordTextBox.Text);
                    UploadConnection.setRemotePort(Convert.ToInt32(portTextBox.Text));
                    UploadConnection.login();
                    if (strTMP != "" && strTMP != "/")
                    {
                        UploadConnection.chdir(strTMP);
                    }
                    UploadConnection.deleteRemoteFile(strFileName);
                    UploadConnection.upload(strFileName);
                    UploadConnection.close();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Connection Failed! It is possible that the FTP server you specified is unable to handle more connections currently, in which case try again later. Also verify your username, password, connection port, and desired output directory.\n\nNOTE: Feed Launch .NET can't overwrite existing feeds with the same filename and location already uploaded to the FTP server!", String.Concat("Error encountered- ", ex2.ToString()), MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                }
            }
            progressBar1.Value = 0;

        }
    }
}