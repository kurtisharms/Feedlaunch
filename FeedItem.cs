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
        public FeedItem(string refTitle, string refDescription, string refLink, int refOrder)
        {
            title = refTitle;
            description = refDescription;
            link = refLink;
            order = refOrder;
        }

    }
}
