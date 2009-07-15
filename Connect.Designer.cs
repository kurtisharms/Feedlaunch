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
namespace FeedCreator.NET
{
    partial class UploadForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
        	this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        	this.serverTextBox = new System.Windows.Forms.TextBox();
        	this.usernameTextBox = new System.Windows.Forms.TextBox();
        	this.fileName = new System.Windows.Forms.TextBox();
        	this.uploadButton = new System.Windows.Forms.Button();
        	this.cancelButton = new System.Windows.Forms.Button();
        	this.directoryPath = new System.Windows.Forms.TextBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.label3 = new System.Windows.Forms.Label();
        	this.label4 = new System.Windows.Forms.Label();
        	this.label5 = new System.Windows.Forms.Label();
        	this.progressBar1 = new System.Windows.Forms.ProgressBar();
        	this.label6 = new System.Windows.Forms.Label();
        	this.label7 = new System.Windows.Forms.Label();
        	this.portTextBox = new System.Windows.Forms.TextBox();
        	this.passwordTextBox = new System.Windows.Forms.MaskedTextBox();
        	((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// errorProvider1
        	// 
        	this.errorProvider1.ContainerControl = this;
        	// 
        	// toolTip1
        	// 
        	this.toolTip1.AutoPopDelay = 10000;
        	this.toolTip1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
        	this.toolTip1.InitialDelay = 500;
        	this.toolTip1.IsBalloon = true;
        	this.toolTip1.ReshowDelay = 100;
        	this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
        	this.toolTip1.ToolTipTitle = "Feed Launch .NET Interactive Help";
        	// 
        	// serverTextBox
        	// 
        	this.serverTextBox.Location = new System.Drawing.Point(109, 27);
        	this.serverTextBox.Name = "serverTextBox";
        	this.serverTextBox.Size = new System.Drawing.Size(136, 20);
        	this.serverTextBox.TabIndex = 4;
        	this.toolTip1.SetToolTip(this.serverTextBox, "Enter the name/IP address or the FTP server.");
        	// 
        	// usernameTextBox
        	// 
        	this.usernameTextBox.Location = new System.Drawing.Point(109, 54);
        	this.usernameTextBox.Name = "usernameTextBox";
        	this.usernameTextBox.Size = new System.Drawing.Size(136, 20);
        	this.usernameTextBox.TabIndex = 5;
        	this.toolTip1.SetToolTip(this.usernameTextBox, "Enter your username here.");
        	// 
        	// fileName
        	// 
        	this.fileName.Enabled = false;
        	this.fileName.Location = new System.Drawing.Point(15, 144);
        	this.fileName.Name = "fileName";
        	this.fileName.Size = new System.Drawing.Size(336, 20);
        	this.fileName.TabIndex = 6;
        	this.toolTip1.SetToolTip(this.fileName, "Enter the desired upload filename. Leave empty and the feed will retain its origi" +
        	        	"nal name.");
        	// 
        	// uploadButton
        	// 
        	this.uploadButton.Location = new System.Drawing.Point(79, 301);
        	this.uploadButton.Name = "uploadButton";
        	this.uploadButton.Size = new System.Drawing.Size(75, 23);
        	this.uploadButton.TabIndex = 8;
        	this.uploadButton.Text = "Upload";
        	this.toolTip1.SetToolTip(this.uploadButton, "Uploads feed to FTP server");
        	this.uploadButton.UseVisualStyleBackColor = true;
        	this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
        	// 
        	// cancelButton
        	// 
        	this.cancelButton.Location = new System.Drawing.Point(160, 301);
        	this.cancelButton.Name = "cancelButton";
        	this.cancelButton.Size = new System.Drawing.Size(75, 23);
        	this.cancelButton.TabIndex = 9;
        	this.cancelButton.Text = "Cancel";
        	this.toolTip1.SetToolTip(this.cancelButton, "Cancels upload and closes this window");
        	this.cancelButton.UseVisualStyleBackColor = true;
        	this.cancelButton.Click += new System.EventHandler(this.button2_Click);
        	// 
        	// directoryPath
        	// 
        	this.directoryPath.Location = new System.Drawing.Point(15, 211);
        	this.directoryPath.Name = "directoryPath";
        	this.directoryPath.Size = new System.Drawing.Size(336, 20);
        	this.directoryPath.TabIndex = 11;
        	this.toolTip1.SetToolTip(this.directoryPath, "Enter the directory path");
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(12, 27);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(61, 13);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "FTP Server";
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(12, 54);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(55, 13);
        	this.label2.TabIndex = 1;
        	this.label2.Text = "Username";
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(15, 86);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(53, 13);
        	this.label3.TabIndex = 3;
        	this.label3.Text = "Password";
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Location = new System.Drawing.Point(15, 195);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(114, 13);
        	this.label4.TabIndex = 7;
        	this.label4.Text = "Directory Path(relative)";
        	// 
        	// label5
        	// 
        	this.label5.AutoSize = true;
        	this.label5.Location = new System.Drawing.Point(12, 128);
        	this.label5.Name = "label5";
        	this.label5.Size = new System.Drawing.Size(51, 13);
        	this.label5.TabIndex = 10;
        	this.label5.Text = "FileName";
        	// 
        	// progressBar1
        	// 
        	this.progressBar1.Location = new System.Drawing.Point(18, 273);
        	this.progressBar1.Name = "progressBar1";
        	this.progressBar1.Size = new System.Drawing.Size(333, 10);
        	this.progressBar1.TabIndex = 12;
        	// 
        	// label6
        	// 
        	this.label6.AutoSize = true;
        	this.label6.Location = new System.Drawing.Point(15, 257);
        	this.label6.Name = "label6";
        	this.label6.Size = new System.Drawing.Size(85, 13);
        	this.label6.TabIndex = 13;
        	this.label6.Text = "Upload Progress";
        	// 
        	// label7
        	// 
        	this.label7.AutoSize = true;
        	this.label7.Location = new System.Drawing.Point(273, 30);
        	this.label7.Name = "label7";
        	this.label7.Size = new System.Drawing.Size(26, 13);
        	this.label7.TabIndex = 14;
        	this.label7.Text = "Port";
        	// 
        	// portTextBox
        	// 
        	this.portTextBox.Location = new System.Drawing.Point(305, 27);
        	this.portTextBox.MaxLength = 12;
        	this.portTextBox.Name = "portTextBox";
        	this.portTextBox.Size = new System.Drawing.Size(46, 20);
        	this.portTextBox.TabIndex = 15;
        	this.portTextBox.Text = "21";
        	// 
        	// passwordTextBox
        	// 
        	this.passwordTextBox.Location = new System.Drawing.Point(109, 86);
        	this.passwordTextBox.Name = "passwordTextBox";
        	this.passwordTextBox.Size = new System.Drawing.Size(136, 20);
        	this.passwordTextBox.TabIndex = 16;
        	// 
        	// UploadForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(363, 336);
        	this.ControlBox = false;
        	this.Controls.Add(this.passwordTextBox);
        	this.Controls.Add(this.portTextBox);
        	this.Controls.Add(this.label7);
        	this.Controls.Add(this.label6);
        	this.Controls.Add(this.progressBar1);
        	this.Controls.Add(this.directoryPath);
        	this.Controls.Add(this.label5);
        	this.Controls.Add(this.cancelButton);
        	this.Controls.Add(this.uploadButton);
        	this.Controls.Add(this.label4);
        	this.Controls.Add(this.fileName);
        	this.Controls.Add(this.usernameTextBox);
        	this.Controls.Add(this.serverTextBox);
        	this.Controls.Add(this.label3);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.label1);
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "UploadForm";
        	this.ShowIcon = false;
        	this.ShowInTaskbar = false;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "Upload Feed";
        	this.TopMost = true;
        	this.Load += new System.EventHandler(this.UploadForm_Load);
        	((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.TextBox usernameTextBox;
        public System.Windows.Forms.TextBox serverTextBox;
        public System.Windows.Forms.TextBox fileName;
        public System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.TextBox directoryPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox passwordTextBox;
    }
}