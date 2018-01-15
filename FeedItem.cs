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
using System.Text;
using System.Collections;
using FeedLaunch.NET;
using FeedCreator.NET;

namespace FeedCreator.NET
{
    class FeedItem
    {
        //The item's title
        public string title = null;
        //the description
        public string description = null;
        //the linking url
        public string link = null;
        //this value is later used for sorting the List of items before creating the feed
        public int order = 0;
        //the publication date
        public DateTime pubDate;
        //the author's email address
        public string authorEmail = null;
        public string sourceText = null;
        public string sourceURL = null;
        public FeedItem(string refTitle, string refDescription, string refLink, int refOrder, string refAuthor, DateTime refDate, string refText, string refURL)
        {
            //Here we assign the variables each a value passed from the sender
            title = refTitle;
            description = refDescription;
            link = refLink;
            order = refOrder;
            pubDate = refDate;
            authorEmail = refAuthor;
            sourceText = refText;
            sourceURL = refURL;
        }
        public FeedItem()
        {
            //A second empty FeedItem class creator method. Useful for creating a new
            //FeedItem class but without assining any variable values
        }

    }
}
