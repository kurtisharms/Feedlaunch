 /*---------------------------------------------------------------------------------
    This source file is a part of FeedLaunch .NET
   
    For the latest information, please visit http://feedlaunch.sourceforge.net/
    
    Copyright (C) 2007 The FeedLaunch Team

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
using System.Xml;
using System.Xml.Schema;
using System.Threading;
using System.IO;
using System.Media;

namespace FeedCreator.NET
{
    public partial class Form1 : Form
    {
        //Here is the class to store the channel info
        ChannelClass ChannelInfo;
        //here are our forms
        newChannelForm newChannel;
        createItemForm createItemForm1;
        explorer ExplorerForm1;
        explorer HelpExplorerForm1;
        //Here is the list which is used to store all the feed items
        //in memory
        List<FeedItem> FeedItemList;
        //This is our XmlTextWriter which we will
        //use to write our feed with
        XmlTextWriter writer;
        //This is our Xml Read which we will read the feeds with
        XmlTextReader reader;
        //this variable is true if the item is saved; other false
        bool saved = false;
        //this string stores the beggining title text
        const string title = "FeedLaunch .NET- ";
        //the filename
        string fileName = "Feed1.xml";
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
            feedList.SelectedIndexChanged +=new EventHandler(feedList_SelectedIndexChanged);
            this.Text = "FeedLaunch .NET- Feed1.xml";
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
            
        }
        private void manage_Channel(object sender, EventArgs e)
        {
            ChannelInfo = new ChannelClass();
            try {
                ChannelInfo.title = newChannel.titleBox.Text;
                ChannelInfo.description = newChannel.descriptionBox.Text;
                ChannelInfo.link = newChannel.linkBox.Text;
                if (newChannel.dateTimePicker1.Value != null)
                {
                    ChannelInfo.pubDate = newChannel.dateTimePicker1.Value.ToString();
                }
                else
                {
                    ChannelInfo.pubDate = "";
                }
                if (newChannel.dateTimePicker2.Value != null)
                {
                    ChannelInfo.buildDate = newChannel.dateTimePicker2.Value.ToString();
                }
                else
                {
                    ChannelInfo.buildDate = "";
                }
                ChannelInfo.copyright = newChannel.copyrightBox.Text;
                if (newChannel.listBox1.SelectedValue != null)
                {
                    ChannelInfo.language = newChannel.listBox1.SelectedValue.ToString();
                }
                else
                {
                    ChannelInfo.language = "en";
                }
                ChannelInfo.webmaster = newChannel.webmasterBox.Text;
                ChannelInfo.ttl = newChannel.numericUpDown1.Value;
                ChannelInfo.imageText = newChannel.imageText.Text;
                ChannelInfo.imageWidth = newChannel.numericUpDown2.Value;
                ChannelInfo.imageHeight = newChannel.numericUpDown3.Value;
                titleLabel.Text = ChannelInfo.title;
                linkLabel.Text = ChannelInfo.link;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has been encountered. Please submit a bug report if this problem persists!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.saved = false;
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
            string txt = createItemForm1.titleBox.Text;
            while (itemList.Items.Contains(txt))
            {
                txt = String.Concat(txt, " ");
            }
  FeedItemList.Add(new FeedItem(txt, 
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
                saved = false;
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
                    saved = false;
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
                        saved = false;
                    }
                }
            }
            catch(Exception ex)
            {
            }


        }

        private void editItemButton_Click(object sender, EventArgs e)
        {
            //Make sure we aren't selecting an empty(null) item
            if(itemList.SelectedItem != null)
            {
            //create the form
            createItemForm1 = new createItemForm();
            //assign the form's event handler which is called when the user hits
            //"Create" or "OK"
            createItemForm1.EditFeedItem += new createItemForm.CustomEventDelegate(editFeedItem);
            //Assign the correct title text
            createItemForm1.Text = "Edit Feed Item";
            //Display our form
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
                List<FeedItem> SortedList = new List<FeedItem>();
                int originalSelectedIndex;

                //Environment.GetEnvironmentVariable("TEMP");
                writer = new XmlTextWriter(this.fileName, System.Text.Encoding.UTF8);
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
                writer.WriteElementString("language", ChannelInfo.language);
                writer.WriteElementString("copyright", ChannelInfo.copyright);
                writer.WriteElementString("managingEditor", "");
                writer.WriteElementString("webMaster", ChannelInfo.webmaster);
                writer.WriteElementString("pubDate", ChannelInfo.pubDate);
                writer.WriteElementString("lastBuildDate", ChannelInfo.buildDate);
                writer.WriteElementString("category", "");
                writer.WriteElementString("generator", "FeedLaunch .NET- http://feedlaunch.sourceforge.net/ or http://www.sourceforge.net/feedlaunch");
                writer.WriteElementString("docs", "http://cyber.law.harvard.edu/rss/rss.html");
                writer.WriteElementString("ttl", ChannelInfo.ttl.ToString());
                if (ChannelInfo.imageText != null || ChannelInfo.imageText != "")
                {
                    writer.WriteStartElement("image");
                    writer.WriteElementString("url", ChannelInfo.imageText);
                    writer.WriteElementString("width", ChannelInfo.imageWidth.ToString());
                    writer.WriteElementString("height", ChannelInfo.imageHeight.ToString());
                    writer.WriteEndElement();
                }
                
                TMPopened = FeedItemList.FindAll(delegate(FeedItem f)
                {
                    return itemList.Items.Contains(f.title) == true;
                });
                itemList.Select();
                originalSelectedIndex = itemList.SelectedIndex;
                if (itemList.Items.Count > 0)
                {
                    int i = 0;
                    while(i < itemList.Items.Count)
                    {
                        itemList.SelectedIndex = i;
                        TMPopened.ForEach(delegate(FeedItem f)
                        {
                            
                            if (f.title == itemList.SelectedItem.ToString())
                            {
                                f.order = i;
                            }
                        });
                        i = i + 1;
                    }
                }
                itemList.SelectedIndex = originalSelectedIndex;
                TMPopened.Sort(delegate(FeedItem f1, FeedItem f2)
                {
                    return f1.order.CompareTo(f2.order);
                });

                //Now begin writing the feed items
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

        private void visitCommToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new explorer form
            ExplorerForm1 = new explorer();
            //Assign the new url to the form- make sure to assign this
            //before the explorer form loads
            ExplorerForm1.urlLocation = new System.Uri("http://feedlaunch.sourceforge.net/");
            //And now just display our explorer form
            ExplorerForm1.Show();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create a new explorer form
            HelpExplorerForm1 = new explorer();
            //Set the form's title text
            HelpExplorerForm1.Text = "FeedLaunch On-line Help and Documentation";
            //Assign the new url to the form- make sure to assign this
            //before the help form loads
            HelpExplorerForm1.urlLocation = new System.Uri("http://feedlaunch.sourceforge.net/docs/");
            //And now just display our help form
            HelpExplorerForm1.Show();
        }
        private void feedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selection is changed, so we'll just set the "saved" variable as false
            saved = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.ProductName + "\n" + "Version " + Application.ProductVersion + "\n" + Application.CompanyName, "About FeedLaunch", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            bool run = true;
            try
            {

                if (feedList.SelectedItem.ToString() != "RSS 2.0")
                {
                    DialogResult result;
                    result = MessageBox.Show("ATOM Feeds are currently unsupported. Do you wish to save your feed as RSS instead?", "Save as RSS Feed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (result == DialogResult.No)
                    {
                        run = false;
                    }
                }
                if (run == true)
                {
                    if (fileName == "Feed1.xml")
                    {

                        MessageBox.Show("You have not selected a destination for this feed. The next dialog will allow you to choose one.", "No Destination Found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        saveFileDialog1.Filter = "Rss Feed (*.rss)|*.rss"; //|ATOM Feed (*.atom)|*.atom";
                        saveFileDialog1.FilterIndex = 1;
                        saveFileDialog1.Title = "Select a destination folder and filename-";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (saveFileDialog1.FileName != null)
                            {
                                fileName = saveFileDialog1.FileName;
                                saved = true;
                                feedButton_Click(sender, e);
                                statusLabel.Text = "Feed successfully saved";
                            }
                        }
                    }
                    if (fileName == "_SaveAsFeed1.system")
                    {
                        saveFileDialog1.Filter = "Rss Feed (*.rss)|*.rss"; // |ATOM Feed (*.atom)|*.atom";
                        saveFileDialog1.FilterIndex = 1;
                        saveFileDialog1.Title = "Save As...";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (saveFileDialog1.FileName != null)
                            {
                                fileName = saveFileDialog1.FileName;
                                feedButton_Click(sender, e);
                                saved = true;
                                statusLabel.Text = "Feed successfully saved";
                            }
                        }
                    }
                    else
                    {
                        if (fileName != null)
                        {
                            feedButton_Click(sender, e);
                            saved = true;
                            statusLabel.Text = "Feed successfully saved";
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error has been encountered. Please submit a bug report if this problem persists!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileName = "_SaveAsFeed1.system";
            toolStripButton3_Click(sender, e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FeedItemList.Clear();
            ChannelInfo = null;
            itemList.Items.Clear();
            fileName = "Feed1.xml";
            this.Text = title + fileName + "*";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itemList.SelectedValue != null)
            {
                itemList.Select();
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
                    saved = false;
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

    }
}