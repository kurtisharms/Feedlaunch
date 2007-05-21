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
    }
}