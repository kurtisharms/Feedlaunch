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
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;

namespace FeedCreator.NET
{
    class FeedItem
    {
        public string title = null;
        public string description = null;
        public string link = null;
        public int order = 0;
        public DateTime pubDate;
        public string authorEmail = null;
        public string sourceText = null;
        public string sourceURL = null;
        public FeedItem(string refTitle, string refDescription, string refLink, int refOrder, string refAuthor, DateTime refDate, string refText, string refURL)
        {
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
        }

    }
}
