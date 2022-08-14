using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteNovelsDownloader.Models;

namespace WebsiteNovelsDownloader.Downloaders
{
    public class DownloadChapter
    {
        public Chapter? Chapter { get; private set; }

        public string? NextChapterUrl { get; private set; }

        public void CreateChapterData(HtmlDocument websiteContent, int chapterNumber, List<Rules> extractRules, HyperlinkRules href)
        {
        }

    }
}
