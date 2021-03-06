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

//If this program is being debugged, then define the following proprocessor variable:
//#define APP_DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Media;
using System.Net;
using Microsoft.Win32;
using FeedLaunch.NET;
using FeedCreator.NET;

namespace FeedCreator.NET
{
    public partial class Form1 : Form
    {
        //Here is the class to store the channel info
        ChannelClass ChannelInfo;
        //here are our forms
        UploadForm uploadFeedForm;
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
        const string mainTitle = "Feed Launch .NET- ";
        //the filename
        string fileName = "Feed1.xml";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if(APP_DEBUG)
#warning The proprossor "APP_DEBUG" is defined. Feed Launch .NET will be compiled with optimizations for debugging. If you plan to distribute this app, please consider undefining this preprocessor variable before recompiling.
#endif

		

			//There are no items in the itemList yet
			itemList.Enabled = false;
            //Here we set the new instances of our classes/forms
            ChannelInfo = new ChannelClass();
            newChannel = new newChannelForm();
            createItemForm1 = new createItemForm();
            FeedItemList = new List<FeedItem>();
            //When the form opens, there won't be any feed items. Therefore, fill the 
            //itemList with "EMPTY"
            itemList.Items.Add("EMPTY");
            feedList.SetSelected(1, true);
            feedList.SelectedValueChanged += new EventHandler(feedList_SelectedValueChanged);
            feedButton.Text = "Create RSS 2.0 Feed";
            createItemForm1.CreateFeedItem += new createItemForm.CustomEventDelegate(addNewItem);
            newChannel.CreateFeed += new newChannelForm.CustomEventDelegate(manage_Channel);
            feedList.SelectedIndexChanged +=new EventHandler(feedList_SelectedIndexChanged);
            linkLabel.Click +=new EventHandler(linkLabel_Click);
            feedList.SelectedIndex = 0;

            //Check for updates every time we start!
            try
            {
                System.Net.WebClient wc = new WebClient();
                Stream wcStream = wc.OpenRead("http://feedlaunch.sourceforge.net/version.txt");
                StreamReader sr = new StreamReader(wcStream);
                string line;
                line = sr.ReadLine().Trim();
                if (line != Application.ProductVersion)
                {
                    DialogResult result = MessageBox.Show("There are updates available for Feed Launch .NET. Do you wish to download and install them now?", "Updates found!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        line = sr.ReadLine().Trim();
                        Process p = new Process();
                        p.EnableRaisingEvents = false;
                        p.StartInfo.FileName = "iexplore";
                        p.StartInfo.Arguments = line;
                        p.Start();
                        this.Dispose();
                    }
                }
            }
            catch
            {
            }

            //Now we check for the registry key "intro". If the key does not exist(null),
            //then this must be the first time that Feed Launch .NET is running. Therefore,
            //we display the welcome and introduction wizard.
            /*this.Text = "Feed Launch .NET- Feed1.xml";
            this.Text = string.Concat(this.Text, "*");
            RegistryManager appRegistry = new RegistryManager();
			appRegistry.BaseRegistryKey = Registry.CurrentUser;
			if(appRegistry.Read("intro") == null)
			{
				//Create a new instance of the intro form
                intro welcomeForm = new intro();
                //Create a callback
                welcomeForm.introClosed +=new intro.CustomEventDelegate(welcomeForm_introClosed);
                //Make the main form "invisible" and change the title
                this.Width = 1;
                this.Height = 1;
                this.Opacity = 0;
                this.Text = "Feed Launch .NET Intro";
                //Ensure that this welcome form isn't displayed on startup in the future.
                appRegistry.Write("intro","shown");
                //Display the form's instance
                welcomeForm.Show();
			}*/

            // Setup layout 
            itemList.Height = downItemButton.Top + downItemButton.Height - itemList.Top;
            feedButton.Height = feedList.Height;
            feedButton.Top = feedList.Top;
        }

        private void channelsBox_Enter(object sender, EventArgs e)
        {
            //Nothing here
        }

        private void itemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Nothing here
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //This function manages the "Exit" command on the File menu
            //If the value "saved" is true, then the currently opened feed is saved
            if (saved == true)
            {
                this.Dispose();
            }
            // ...Otherwise, we have to ask the user if he/she wishes to save the opened feed
            else
            {
                if (saved == false)
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
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("Are you sure that you want to exit FeedLaunch?", "Exit?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        this.Dispose();
                    }
                }
            }

        }

        private void saveFeed()
        {
            //Nothing here
        }

        private void newChannelButton_Click(object sender, EventArgs e)
        {
           newChannel = new newChannelForm();
           newChannel.CreateFeed += new newChannelForm.CustomEventDelegate(manage_Channel);
           if (ChannelInfo.empty == false)
           {
               newChannel.titleBox.Text = ChannelInfo.title;
               newChannel.linkBox.Text = ChannelInfo.link;
               newChannel.descriptionBox.Text = ChannelInfo.description;
               try
               {
                   newChannel.dateTimePicker1.Value = System.Convert.ToDateTime(ChannelInfo.pubDate);
               }
               catch
               {
                   newChannel.dateTimePicker1.Value = System.DateTime.Today;
               }
               try
               {
                   newChannel.dateTimePicker2.Value = System.Convert.ToDateTime(ChannelInfo.buildDate);
               }
               catch
               {
                   newChannel.dateTimePicker2.Value = System.DateTime.Today;
               }
               newChannel.copyrightBox.Text = ChannelInfo.copyright;
               newChannel.listBox1.SelectedValue = ChannelInfo.language;
               newChannel.webmasterBox.Text = ChannelInfo.webmaster;
               newChannel.numericUpDown1.Value = ChannelInfo.ttl;
               newChannel.imageText.Text = ChannelInfo.imageText;
               newChannel.numericUpDown2.Value = ChannelInfo.imageWidth;
               newChannel.numericUpDown3.Value = ChannelInfo.imageHeight;
           }
            newChannel.Show();
        }

        private void deleteChannelButton_Click(object sender, EventArgs e)
        {
            
        }
        private void manage_Channel(object sender, EventArgs e)
        {
                ChannelInfo.title = newChannel.titleBox.Text;
                ChannelInfo.description = newChannel.descriptionBox.Text;
                ChannelInfo.link = newChannel.linkBox.Text;
                ChannelInfo.pubDate = newChannel.dateTimePicker1.Value.ToString();
                ChannelInfo.buildDate = newChannel.dateTimePicker2.Value.ToString();
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
                try
                {
                    if (ChannelInfo.imageText != "" && ChannelInfo.imageText != null)
                    {
                        pictureBox1.Image = Image.FromFile(ChannelInfo.imageText);
                    }
                }
                catch (Exception ex)
                {
                }
                pictureBox1.Width = System.Convert.ToInt32(ChannelInfo.imageWidth);
                pictureBox1.Height = System.Convert.ToInt32(ChannelInfo.imageHeight);
            //We update the "empty" variable to signify that the ChannelInfo class contains data
            //This is used by the function which opens the newChannel form.
            //If the ChannelInfo class is "empty," then the form is obviously opened for
            //the first time.
                ChannelInfo.empty = false;
            //    MessageBox.Show("An Error has been encountered. Please submit a bug report if this problem persists!", ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.saved = false;
        }

        private void editChannelButton_Click(object sender, EventArgs e)
        {
            newChannelButton_Click(sender, e);
        }

        private void newItemButton_Click(object sender, EventArgs e)
        {
            itemList.Enabled = false;
            createItemForm1 = new createItemForm();
            createItemForm1.CreateFeedItem += new createItemForm.CustomEventDelegate(addNewItem);
            createItemForm1.CancelCreateFeedItem +=new createItemForm.CustomEventDelegate(createItemForm1_CancelCreateFeedItem);
            createItemForm1.Text = "Create New Feed Item";
            createItemForm1.Show();
        }
        private void createItemForm1_CancelCreateFeedItem(object sender, EventArgs e)
        {
            itemList.Enabled = true;
        }


        private void addNewItem(object sender, EventArgs e)
        {
            string txt = createItemForm1.titleBox.Text;
            itemList.Enabled = true;
            while (itemList.Items.Contains(txt))
            {
                txt = String.Concat(txt, " ");
            }
  FeedItemList.Add(new FeedItem(txt, 
                createItemForm1.descriptionBox.Text,
                createItemForm1.linkBox.Text, itemList.Items.Count + 1, createItemForm1.authorEmail.Text, createItemForm1.dateTimePicker1.Value, createItemForm1.sourceText.Text, createItemForm1.sourceURL.Text));
            itemList.Items.Clear();
            FeedItemList.ForEach(delegate(FeedItem f)
            {
                itemList.Items.Add(f.title);
            });

        }

        private void deleteItemButton_Click(object sender, EventArgs e)
        {
            object tmpObj = "EMPTY";
            object tmpObj2 = "";
            if (itemList.SelectedItem != tmpObj && itemList.SelectedItem != tmpObj2 && itemList.SelectedItem != null)
            {
                DialogResult result;
                result = MessageBox.Show("Are you sure that you want to delete this feed item?", "Delete Feed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Select an item to delete!", "No Item Selected!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            if (itemList.Items.Count == 0)
            {
                itemList.Items.Add("EMPTY");
                itemList.Enabled = false;
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
                    itemList.SelectedIndex = tmpIndex - 1;
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
                    if (itemList.SelectedIndex < itemList.Items.Count && itemList.SelectedIndex < itemList.Items.Count - 1)
                    {

                        object tmpObject = itemList.SelectedItem;
                        int tmpIndex = itemList.SelectedIndex;
                        itemList.Items.Remove(tmpObject);
                        itemList.Items.Insert(tmpIndex + 1, tmpObject);
                        itemList.SelectedIndex = tmpIndex + 1;
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
            FeedItem fi = new FeedItem();
            //Make sure we aren't selecting an empty(null) item
            if (itemList.SelectedItem != null)
            {
                if (itemList.SelectedItem.ToString() != "EMPTY")
                {
                    //create the form
                    createItemForm1 = new createItemForm();
                    //assign the form's event handler which is called when the user hits
                    //"Create" or "OK"
                    createItemForm1.EditFeedItem += new createItemForm.CustomEventDelegate(editFeedItem);
                    createItemForm1.CancelCreateFeedItem += new createItemForm.CustomEventDelegate(createItemForm1_CancelCreateFeedItem);
                    FeedItemList.ForEach(delegate(FeedItem f)
                    {
                        if (f.title == itemList.SelectedItem.ToString())
                        {
                            createItemForm1.titleBox.Text = f.title;
                            createItemForm1.descriptionBox.Text = f.description;
                            createItemForm1.linkBox.Text = f.link;
                            createItemForm1.authorEmail.Text = f.authorEmail;
                            try
                            {
                                createItemForm1.dateTimePicker1.Value = f.pubDate;
                            }
                            catch
                            {
                                createItemForm1.dateTimePicker1.Value = DateTime.Today;
                            }
                            createItemForm1.sourceText.Text = f.sourceText;
                            createItemForm1.sourceURL.Text = f.sourceURL;
                        }
                    });
                    //Assign the correct title text
                    createItemForm1.Text = "Edit Feed Item";
                    itemList.Enabled = false;
                    //Display our form
                    createItemForm1.Show();
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("There is no item selected to edit!", "No item selected!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("There is no item selected to edit!", "No item selected!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

        }
        private void editFeedItem(object sender, EventArgs e)
        {
            FeedItemList.ForEach(delegate(FeedItem f)
            {
                if (f.title != null && createItemForm1.titleOnLoad != null)
                {
                    if (f.title == createItemForm1.titleOnLoad)
                    {
                        f.title = createItemForm1.titleBox.Text;
                        f.description = createItemForm1.descriptionBox.Text;
                        f.link = createItemForm1.linkBox.Text;
                        f.authorEmail = createItemForm1.authorEmail.Text;
                        f.pubDate = createItemForm1.dateTimePicker1.Value;
                        f.sourceText = createItemForm1.sourceText.Text;
                        f.sourceURL = createItemForm1.sourceURL.Text;
                        itemList.Enabled = true;
                        itemList.SelectedItem = f.title;
                    }
                }
            });
            itemList.Enabled = true;
  
        }
        private void feedList_SelectedValueChanged(object sender, EventArgs e)
        {
            feedButton.Text = String.Concat("Create ", feedList.SelectedItem.ToString(), " Feed");
        }

        private void feedButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (feedList.SelectedItem.ToString() == "ATOM 1.0")
                {
                    List<FeedItem> TMPopened = new List<FeedItem>();
                    List<FeedItem> SortedList = new List<FeedItem>();
                    int originalSelectedIndex;
                    //
                    //
                    if (fileName == "Feed1.xml")
                    {
                        MessageBox.Show("You have not selected a destination for this feed. The next dialog will allow you to choose one.", "No Destination Found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        saveFileDialog1.Filter = "Rss Feed (*.rss)|*.rss|ATOM/RSS Xml Feed Files (*.xml)|*.xml|All Files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 2;
                        saveFileDialog1.Title = "Select a destination folder and filename-";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (saveFileDialog1.FileName != null)
                            {
                                fileName = saveFileDialog1.FileName;
                                saved = true;
                            }
                        }
                    }
                    //Prepare to write the ATOM feed
                    //First, create a new copy of XmlTextWriter
                    writer = new XmlTextWriter(this.fileName, System.Text.Encoding.UTF8);
                    //We'll use nice, indented formatting for readability
                    writer.Formatting = Formatting.Indented;
                    //Open the copy of XmlTextWriter for writing
                    writer.WriteStartDocument();
                    writer.WriteStartElement("feed");
                    writer.WriteAttributeString("xlmns", "http://www.w3.org/2005/Atom");
                    writer.WriteElementString("title", ChannelInfo.title);

                    writer.WriteStartElement("link");
                    writer.WriteAttributeString("href", ChannelInfo.link);
                    writer.WriteEndElement();

                    writer.WriteElementString("updated", ChannelInfo.buildDate);
                    writer.WriteElementString("id", ChannelInfo.link);

                    //Start the "author" element
                    writer.WriteStartElement("author");
                    writer.WriteElementString("name", ChannelInfo.webmaster);
                    //End the "author" element
                    writer.WriteEndElement();

                    writer.WriteElementString("rights", ChannelInfo.copyright);
                    writer.WriteStartElement("generator");
                    writer.WriteAttributeString("uri", "http://feedlaunch.sourceforge.net/");
                    writer.WriteAttributeString("version", "1.0.0");
                    writer.WriteAttributeString("name", "Feed Launch .NET");
                    writer.WriteEndElement();


                    TMPopened = FeedItemList.FindAll(delegate(FeedItem f)
                    {
                        return itemList.Items.Contains(f.title) == true;
                    });
                    itemList.Select();
                    originalSelectedIndex = itemList.SelectedIndex;
                    if (itemList.Items.Count > 0)
                    {
                        int i = 0;
                        while (i < itemList.Items.Count)
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

                    TMPopened.ForEach(delegate(FeedItem f)
                    {
                        writer.WriteStartElement("entry");
                        writer.WriteElementString("title", f.title);

                        writer.WriteStartElement("link");
                        writer.WriteAttributeString("href", f.link);
                        writer.WriteEndElement();

                        writer.WriteElementString("id", ChannelInfo.link + "::" + f.title.Trim());
                        writer.WriteElementString("published", f.pubDate.ToString());

                        writer.WriteStartElement("author");
                        writer.WriteElementString("email", f.authorEmail);
                        writer.WriteEndElement();

                        writer.WriteStartElement("content");
                        writer.WriteAttributeString("type", "xhtml");
                        writer.WriteAttributeString("xml:lang", ChannelInfo.language);
                        writer.WriteAttributeString("xml:base", ChannelInfo.link);
                        writer.WriteStartElement("div");
                        writer.WriteAttributeString("xmlns", "http://www.w3.org/1999/xhtml");
                        writer.WriteAttributeString("description", f.description);
                        writer.WriteEndElement();
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    });

                    //This writes the end of the feed by completint the Element "</feed>"
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                    //Update the status label, notifying the user that
                    //the operation was successful
                    statusLabel.Text = "Finished writing ATOM 1.0 Feed";
                }
                else
                {
                    //Create a temporary List which will store the currently open
                    //items in the "items" listview
                    List<FeedItem> TMPopened = new List<FeedItem>();
                    List<FeedItem> SortedList = new List<FeedItem>();
                    int originalSelectedIndex;
                    //
                    //
                    if (fileName == "Feed1.xml")
                    {
                        MessageBox.Show("You have not selected a destination for this feed. The next dialog will allow you to choose one.", "No Destination Found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        saveFileDialog1.Filter = "Rss Feed (*.rss)|*.rss|ATOM/RSS Xml Feed Files (*.xml)|*.xml|All Files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 2;
                        saveFileDialog1.Title = "Select a destination folder and filename-";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            if (saveFileDialog1.FileName != null)
                            {
                                fileName = saveFileDialog1.FileName;
                                saved = true;
                            }
                        }
                    }
                    //
                    //
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
                    writer.WriteElementString("generator", "Feed Launch .NET- http://feedlaunch.sourceforge.net/ or http://www.sourceforge.net/feedlaunch");
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
                        while (i < itemList.Items.Count)
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
                        writer.WriteElementString("author", f.authorEmail);
                        writer.WriteElementString("pubDate", f.pubDate.ToString());
                        writer.WriteStartElement("source");
                        writer.WriteAttributeString("url", f.sourceURL);
                        writer.WriteRaw(f.sourceText);
                        writer.WriteEndElement();
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
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("File not found! Please click SAVE AS and specify a new name and location for this feed!", "File not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while trying to write this feed! If this problem persists, please contact the Feed Launch .Net team!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MessageBox.Show(Application.ProductName + "\n" + "Version " + Application.ProductVersion + "\n" + Application.CompanyName + "\nOpen Source - Released under the GPL(Gnu Public License)", "About FeedLaunch", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileName == "Feed1.xml")
                    {

                        MessageBox.Show("You have not selected a destination for this feed. The next dialog will allow you to choose one.", "No Destination Found!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        saveFileDialog1.Filter = "Rss Feed (*.rss)|*.rss|ATOM/RSS Xml Feed Files (*.xml)|*.xml|All Files (*.*)|*.*";
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
                                this.Text = mainTitle + fileName;
                            }
                        }
                    }
                    if (fileName == "_SaveAsFeed1.system")
                    {
                        saveFileDialog1.Filter = "Rss Feed (*.rss)|*.rss|ATOM/RSS Xml Feed Files (*.xml)|*.xml|All Files (*.*)|*.*";
                        saveFileDialog1.FilterIndex = 2;
                        saveFileDialog1.Title = "Save As...";
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            saveFileDialog1.FileName.Trim();
                            if (saveFileDialog1.FileName != null)
                            {
                                if (saveFileDialog1.FileName.EndsWith(".rss"))
                                    feedList.SelectedIndex = 0;
                                else
                                    feedList.SelectedIndex = 1;
                                fileName = saveFileDialog1.FileName;
                                feedButton_Click(sender, e);
                                saved = true;
                                statusLabel.Text = "Feed successfully saved";
                                this.Text = mainTitle + fileName;
                            }
                        }
                    }
                    else if(fileName != "Feed1.xml")
                    {
                        if (fileName != null)
                        {
                            if (fileName.EndsWith(".rss"))
                                feedList.SelectedIndex = 0;
                            else
                                feedList.SelectedIndex = 1;
                            feedButton_Click(sender, e);
                            saved = true;
                            statusLabel.Text = "Feed successfully saved";
                            this.Text = mainTitle + fileName;
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
            ChannelInfo = new ChannelClass();
            titleLabel.Text = "No Title Specified";
            linkLabel.Text = "No Link Specified";
            itemList.Items.Clear();
            fileName = "Feed1.xml";
            itemList.Items.Add("EMPTY");
            this.Text = mainTitle + fileName + "*";
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
#if(APP_DEBUG)
#else
            try
            {
#endif
                string strTMP;
                itemList.Select();
                object selectedObject = itemList.SelectedItem;
                if (selectedObject != null)
                {
                    strTMP = selectedObject.ToString();
                }
                else
                {
                    strTMP = "EMPTY";
                }
                if (strTMP != null && strTMP.ToLower() != "null" && strTMP.ToLower() != "empty")
                {
                    DialogResult result;
                    result = MessageBox.Show("Are you sure that you want to delete this feed item?", "Delete Feed Item?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                        if (this.Text.EndsWith("*") == false)
                        {
                            this.Text = this.Text + "*";
                        }
                    }
                }
#if(APP_DEBUG)
#else
            }
            catch
            {
            }
#endif
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.WebClient wc = new WebClient();
                Stream wcStream = wc.OpenRead("http://feedlaunch.sourceforge.net/version.txt");
                StreamReader sr = new StreamReader(wcStream);
                string line;
                line = sr.ReadLine().Trim();
                if (line == Application.ProductVersion)
                {
                    MessageBox.Show("You are running the latest version of Feed Launch .NET! There are no updates currently!", "No Updates!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult result = MessageBox.Show("There are updates available for Feed Launch .NET. Do you wish to download and install them now?", "Updates found!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        line = sr.ReadLine().Trim();
                        line = sr.ReadLine().Trim();
                        Process p = new Process();
                        p.EnableRaisingEvents = false;
                        p.StartInfo.FileName = "iexplore";
                        p.StartInfo.Arguments = line;
                        p.Start();
                        this.Dispose();
                    }
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Unable to connect to the internet and locate the Feed Launch .NET server. Check that your internet connection is properly working before retrying. If the automatic updater continues to fail, please check the Feed Launch .NET website directly for any updates at feedlaunch.sf.net", "Please try again later!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void openStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void uploadFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uploadFeedForm = new UploadForm();
            uploadFeedForm.strFileName = this.fileName;
            uploadFeedForm.uploadButton.Click += new EventHandler(uploadButtonForm_Click);
            uploadFeedForm.Show();
        }
        private void uploadButtonForm_Click(object sender, EventArgs e)
        {
            feedButton_Click(sender, e);
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
            uploadFeedToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Rss Feed (*.rss)|*.rss|ATOM/RSS Xml Feed Files (*.xml)|*.xml|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Title = "Select File to Open";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog1.FileName != null)
                    {
                        reader = new XmlTextReader(openFileDialog1.FileName);
                        reader.WhitespaceHandling = WhitespaceHandling.None;
                        while (reader.Read())
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    if (reader.Name.ToLower() == "rss")
                                    {
                                        readRSS(sender, e, openFileDialog1.FileName);
                                        saved = true;
                                        fileName = openFileDialog1.FileName;
                                        this.Text = String.Concat(mainTitle, fileName);
                                        break;
                                    }
                                    if (reader.Name.ToLower() == "feed")
                                    {
                                        readATOM(sender, e, openFileDialog1.FileName);
                                        saved = true;
                                        fileName = openFileDialog1.FileName;
                                        this.Text = String.Concat(mainTitle, fileName);
                                        break;
                                    }
                                    break;
                                case XmlNodeType.Text:
                                    break;
                            }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("The file you selected is not a proper RSS or ATOM feed file!", "Error encountered- " + ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        
        private void readRSS(object sender, EventArgs e, string openFileName)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(openFileName);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                bool isHeaderTitle = true;
                bool firstItem = true;
                FeedItem tmp = new FeedItem();
                bool isImage = false;
                //We need to clear the FeedItem List
                FeedItemList.Clear();
                itemList.Items.Clear();
                while(reader.Read())
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.ToLower() == "item")
                            {
                                isHeaderTitle = false;
                            }
                            if (isHeaderTitle == true)
                            {
                                if (reader.Name.ToLower() == "title")
                                {
                                    ChannelInfo.title = reader.ReadString();
                                    titleLabel.Text = ChannelInfo.title;
                                    ChannelInfo.empty = false;
                                }
                                else if (reader.Name.ToLower() == "link")
                                {
                                    ChannelInfo.link = reader.ReadString();
                                    linkLabel.Text = ChannelInfo.link;
                                    ChannelInfo.empty = false;
                                }
                                else if (reader.Name.ToLower() == "description")
                                {
                                    ChannelInfo.description = reader.ReadString();
                                    ChannelInfo.empty = false;
                                }
                                else if (reader.Name.ToLower() == "language")
                                {
                                    ChannelInfo.language = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "copyright")
                                {
                                    ChannelInfo.copyright = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "webmaster")
                                {
                                    ChannelInfo.webmaster = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "pubdate")
                                {
                                    ChannelInfo.pubDate = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "lastbuilddate")
                                {
                                    ChannelInfo.buildDate = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "ttl")
                                {
                                    ChannelInfo.ttl = reader.ReadElementContentAsDecimal();
                                }
                                else if (reader.Name.ToLower() == "image")
                                {
                                    isImage = true;
                                }
                                else if (reader.Name.ToLower() == "url")
                                {
                                    if (isImage == true)
                                    {
                                        ChannelInfo.imageText = reader.ReadString();
                                    }
                                }
                                else if (reader.Name.ToLower() == "width")
                                {
                                    if (isImage == true)
                                    {
                                        ChannelInfo.imageWidth = reader.ReadContentAsDecimal();
                                    }
                                }
                                else if (reader.Name.ToLower() == "height")
                                {
                                    if (isImage == true)
                                    {
                                        ChannelInfo.imageHeight = reader.ReadContentAsDecimal();
                                    }
                                }

                            }
                            if (isHeaderTitle == false)
                            {
                                if (reader.Name.ToLower() == "title")
                                {
                                    tmp.title = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "link")
                                {
                                    tmp.link = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "description")
                                {
                                    tmp.description = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "author")
                                {
                                    tmp.authorEmail = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "pubdate")
                                {
                                    try
                                    {
                                        tmp.pubDate = DateTime.Parse(reader.ReadString());
                                    }
                                    catch (Exception ex)
                                    {
#if(APP_DEBUG)
                                        throw ex;
#endif
                                    }
                                }
                                else if (reader.Name.ToLower() == "source")
                                {
                                    tmp.sourceText = reader.ReadString();
                                    tmp.sourceURL = reader.GetAttribute("url");
                                }
                                else if (reader.Name.ToLower() == "item")
                                {
                                    if (firstItem == false)
                                    {
                                        FeedItemList.Add(tmp);
                                        tmp = new FeedItem();
                                    }
                                    else
                                    {
                                        firstItem = false;
                                    }
                                }
                            }


                            break;
                    }
             
                if(isHeaderTitle == false)
                {
                }
                if (tmp.title != null && tmp.title != "")
                {
                    FeedItemList.Add(tmp);
                }
                itemList.Items.Clear();
                FeedItemList.ForEach(delegate(FeedItem f)
                {
                    itemList.Items.Add(f.title);
                });
                if (itemList.Items.Count == 0)
                {
                    itemList.Items.Add("EMPTY");
                }
                try
                {
                    if (ChannelInfo.imageText != "" && ChannelInfo.imageText != null)
                    {
                        pictureBox1.Image = Image.FromFile(ChannelInfo.imageText);
                    }
                    pictureBox1.Width = System.Convert.ToInt32(ChannelInfo.imageWidth);
                    pictureBox1.Height = System.Convert.ToInt32(ChannelInfo.imageHeight);
                }
                catch (Exception ex)
                {
                }
                if (ChannelInfo.title == "")
                    titleLabel.Text = "No Title Specified";
                if(ChannelInfo.link == "")
                    linkLabel.Text = "No Link Specified";
            }
         catch (Exception ex)
            {
#if(APP_DEBUG)
                reader.Close();
                throw ex;
#else
                SystemSounds.Beep.Play();
                MessageBox.Show("Error encountered while trying to open the feed!");
                reader.Close();
#endif
            }
        }

        private void readATOM(object sender, EventArgs e, string openFileName)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(openFileName);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                bool isHeaderTitle = true;
                bool firstItem = true;
                FeedItem tmp = new FeedItem();
                //We need to clear the FeedItem List
                FeedItemList.Clear();
                itemList.Items.Clear();
                while (reader.Read())
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.ToLower() == "entry")
                            {
                                isHeaderTitle = false;
                            }
                            if (isHeaderTitle == true)
                            {
                                if (reader.Name.ToLower() == "title")
                                {
                                    ChannelInfo.title = reader.ReadString();
                                    titleLabel.Text = ChannelInfo.title;
                                    ChannelInfo.empty = false;
                                }
                                else if (reader.Name.ToLower() == "link")
                                {
                                    try
                                    {
                                        ChannelInfo.link = reader.GetAttribute("href");
                                    }
                                    catch
                                    {
                                    }
                                    linkLabel.Text = ChannelInfo.link;
                                    ChannelInfo.empty = false;
                                }
                                else if (reader.Name.ToLower() == "rights")
                                {
                                    ChannelInfo.copyright = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "updated")
                                {
                                    ChannelInfo.buildDate = reader.ReadString();
                                }

                                else if (reader.Name.ToLower() == "name")
                                {
                                    ChannelInfo.webmaster = reader.ReadString();
                                }

                            }
                            if (isHeaderTitle == false)
                            {
                                if (reader.Name.ToLower() == "title")
                                {
                                    tmp.title = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "link")
                                {
                                    try
                                    {
                                        tmp.link = reader.GetAttribute("href");
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (reader.Name.ToLower() == "email")
                                {
                                    tmp.authorEmail = reader.ReadString();
                                }
                                else if (reader.Name.ToLower() == "published")
                                {
                                    try
                                    {
                                        tmp.pubDate = DateTime.Parse(reader.ReadString());
                                    }
                                    catch (Exception ex)
                                    {
#if(APP_DEBUG)
                                        throw ex;
#endif
                                    }
                                }
                                else if (reader.Name.ToLower() == "entry")
                                {
                                    if (firstItem == false)
                                    {
                                        FeedItemList.Add(tmp);
                                        tmp = new FeedItem();
                                    }
                                    else
                                    {
                                        firstItem = false;
                                    }
                                }
                            }


                            break;
                    }

                if (isHeaderTitle == false)
                {
                }
                if (tmp.title != null && tmp.title != "")
                {
                    FeedItemList.Add(tmp);
                }
                itemList.Items.Clear();
                FeedItemList.ForEach(delegate(FeedItem f)
                {
                    itemList.Items.Add(f.title);
                });
                if (itemList.Items.Count == 0)
                {
                    itemList.Items.Add("EMPTY");
                }
                if (ChannelInfo.title == "")
                    titleLabel.Text = "No Title Specified";
                if (ChannelInfo.link == "")
                    linkLabel.Text = "No Link Specified";
            }
            catch (Exception ex)
            {
#if(APP_DEBUG)
                reader.Close();
                throw ex;
#else
                SystemSounds.Beep.Play();
                MessageBox.Show("Error encountered while trying to open the feed!");
                reader.Close();
#endif
            }
        }
        private void linkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (linkLabel.Text.StartsWith("No") == false && linkLabel.Text != null)
                {
                    explorer ex1 = new explorer();
                    Uri url = new Uri(linkLabel.Text);
                    ex1.urlLocation = url;
                    ex1.StartPosition = FormStartPosition.CenterScreen;
                    ex1.TopMost = false;
                    ex1.Show();
                }
            }
            catch
            {
            }
        }
        private void welcomeForm_introClosed(object sender, EventArgs e)
        {
            this.Width = 635;
            this.Height = 545;
            this.Opacity = 100;
            this.Text = "Feed Launch .NET- Feed1.xml";
            this.Text = string.Concat(this.Text, "*");
        }

        private void introToolStripItem_Click(object sender, EventArgs e)
        {
            intro introForm = new intro();
            introForm.Show();
        }

        
        void LinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        	if(linkLabel.Text.Contains("http") || linkLabel.Text.Contains("www."))
        	{
        		explorer LinkExplorer = new explorer();
            	//Assign the new url to the form- make sure to assign this
            	//before the explorer form loads
            	try {
            		            	LinkExplorer.urlLocation = new System.Uri(linkLabel.Text.Trim());
				}
            	catch(Exception ex) { }
            	//And now just display our explorer form
            	LinkExplorer.Show();
        	}
        }
    }
}