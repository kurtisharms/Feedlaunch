using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Threading;
using System.IO;
using System.Media;

namespace FeedCreator.NET
{
    public partial class Form1 : Form
    {
        ChannelClass ChannelInfo;
           newChannelForm newChannel;
        createItemForm createItemForm1;
        List<FeedItem> FeedItemList;
        XmlTextWriter writer;
        bool saved = false;
        const string title = "FeedCreator .NET- ";
        string fileName = "Feed1.xml";
        public string[] channel1 = new string[8];
        public string[,] feedItems1 = new string[15,8];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChannelInfo = new ChannelClass();
            newChannel = new newChannelForm();
            createItemForm1 = new createItemForm();
            this.Disposed += new EventHandler(Form1_Disposed);
            FeedItemList = new List<FeedItem>();
            itemList.Items.Add("EMPTY");
            feedList.SetSelected(1, true);
            feedList.SelectedValueChanged += new EventHandler(feedList_SelectedValueChanged);
            feedButton.Text = "Create RSS 2.0 Feed";
            createItemForm1.CreateFeedItem += new createItemForm.CustomEventDelegate(addNewItem);
            newChannel.CreateFeed += new newChannelForm.CustomEventDelegate(manage_Channel);
            this.Text = "FeedCreator .NET- Feed1.xml";
            this.Text = string.Concat(this.Text, "*");

        }

        private void channelsBox_Enter(object sender, EventArgs e)
        {
            
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void Form1_Disposed(object sender, EventArgs e)
        {
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saved == true)
            {
                this.Dispose();
            }
            else
            {
                DialogResult result;
                result = MessageBox.Show("The current feed isn't saved. Save it?", "Unsaved Feed!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    this.Dispose();
                }
                if (result == DialogResult.Yes)
                {
                    saveFeed();
                    this.Dispose();
                }
            }

        }
        private void saveFeed()
        {

        }

        private void newChannelButton_Click(object sender, EventArgs e)
        {
           newChannel = new newChannelForm();
           newChannel.CreateFeed += new newChannelForm.CustomEventDelegate(manage_Channel);
            newChannel.Show();
        }

        private void deleteChannelButton_Click(object sender, EventArgs e)
        {
            channel1 = null;
        }
        private void manage_Channel(object sender, EventArgs e)
        {
            try {
                ChannelInfo.title = newChannel.titleBox.Text;
                ChannelInfo.description = newChannel.descriptionBox.Text;
                ChannelInfo.link = newChannel.linkBox.Text;
                ChannelInfo.pubDate = newChannel.dateTimePicker1.Value.ToString();
                ChannelInfo.buildDate = newChannel.dateTimePicker2.Value.ToString();
                titleLabel.Text = ChannelInfo.title;
                linkLabel.Text = ChannelInfo.link;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has been encountered. Please submit a bug report if this problem persists!", "Error Handled!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editChannelButton_Click(object sender, EventArgs e)
        {
            
            newChannelButton_Click(sender, e);
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            createItemForm1 = new createItemForm();
            createItemForm1.CreateFeedItem += new createItemForm.CustomEventDelegate(addNewItem);
            createItemForm1.Text = "Create New Feed Item";
            createItemForm1.Show();
        }


        private void addNewItem(object sender, EventArgs e)
        {
  FeedItemList.Add(new FeedItem(createItemForm1.titleBox.Text, 
                createItemForm1.descriptionBox.Text,
                createItemForm1.linkBox.Text, itemList.Items.Count + 1));
            itemList.Items.Clear();
            FeedItemList.ForEach(delegate(FeedItem f)
            {
                itemList.Items.Add(f.title);
            });


        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure that you want to delete this feed?", "Delete Feed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FeedItemList.ForEach(delegate(FeedItem f)
                {
                    if (f.title == itemList.SelectedItem.ToString())
                    {
                        itemList.Items.Remove(itemList.SelectedItem);
                        FeedItemList.Remove(f);
                    }
                });
            }
            }

        private void upItemButton_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedItem != null)
            {
                if (itemList.SelectedIndex > 0)
                {
                    object tmpObject = itemList.SelectedItem;
                    int tmpIndex = itemList.SelectedIndex;
                    itemList.Items.Remove(tmpObject);
                    itemList.Items.Insert(tmpIndex - 1, tmpObject);
                }
            }
        }

        private void downItemButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemList.SelectedItem != null)
                {
                    if (itemList.SelectedIndex < itemList.Items.Count)
                    {
                        object tmpObject = itemList.SelectedItem;
                        int tmpIndex = itemList.SelectedIndex;
                        itemList.Items.Remove(tmpObject);
                        itemList.Items.Insert(tmpIndex + 1, tmpObject);
                    }
                }
            }
            catch(Exception ex)
            {
            }


        }

        private void editItemButton_Click(object sender, EventArgs e)
        {
            if(itemList.SelectedItem != null)
            {

            createItemForm1 = new createItemForm();
            createItemForm1.EditFeedItem += new createItemForm.CustomEventDelegate(editFeedItem);
            createItemForm1.Text = "Edit Feed Item";
            createItemForm1.Show();
            }

        }
        private void editFeedItem(object sender, EventArgs e)
        {
            
        }
        private void feedList_SelectedValueChanged(object sender, EventArgs e)
        {
            feedButton.Text = String.Concat("Create ", feedList.SelectedItem.ToString(), " Feed");
        }

        private void feedButton_Click(object sender, EventArgs e)
        {
            
            if (feedList.SelectedItem.ToString() == "ATOM 3.0")
            {
                //ATOM 3.0 Feeds aren't currently supported
                //Therefore, play a system "Beep" on the speakers and 
                //display a message box
                SystemSounds.Beep.Play();
                MessageBox.Show("Sorry, support for ATOM feeds is a planned feature. Please use RSS 2.0 Feeds instead.", "Planned Feature", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //Create a temporary List which will store the currently open
                //items in the "items" listview
                List<FeedItem> TMPopened = new List<FeedItem>();
                int i = 0;

                //Environment.GetEnvironmentVariable("TEMP");
                writer = new XmlTextWriter("feed1.xml", System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                //Open Document
                writer.WriteStartDocument();
                //Write RSS Feed header
                writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");
                writer.WriteStartElement("channel");
                writer.WriteElementString("title", ChannelInfo.title);
                writer.WriteElementString("link", ChannelInfo.link);
                writer.WriteElementString("description", ChannelInfo.description);


                //Now begin writing the feed items
                TMPopened = FeedItemList.FindAll(delegate(FeedItem f)
                {
                    return itemList.Items.Contains(f.title) == true;
                });
                TMPopened.Sort(delegate(FeedItem f1, FeedItem f2) 
                {
                    return f1.order.CompareTo(f2.order);
                });


                TMPopened.ForEach(delegate(FeedItem f)
                {
                    writer.WriteStartElement("item");
                    writer.WriteElementString("title", f.title.ToString());
                    writer.WriteElementString("link", f.link.ToString());
                    writer.WriteElementString("description", f.description.ToString());
                    writer.WriteEndElement();
                });
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                //Update the status label, notifying the user that
                //the operation was successful
                statusLabel.Text = "Finished writing RSS 2.0 Feed";
            }

        }
        

    }
}