using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteNovelsDownloader.Models
{
    public class Chapter
    {
        public int Number { get; set; }
        public HtmlNode? Content { get; set; }
    }
}
