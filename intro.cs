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
        public intro()
        {
            InitializeComponent();
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}