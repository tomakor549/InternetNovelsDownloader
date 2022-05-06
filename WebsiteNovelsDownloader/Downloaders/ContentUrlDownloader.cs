using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteNovelsDownloader.Models;

namespace WebsiteNovelsDownloader.Downloaders
{
    internal class ContentUrlDownloader : Downloader
    {
        public ContentUrlDownloader(string url, List<string> rules) : base(url, rules)
        {
        }

        public string getNextUrlByHyperlink(string parentRules, string InnerText)
        {
            foreach (var node in _document.DocumentNode.SelectNodes(parentRules))
            {
                foreach(var hyperlink in node.SelectNodes("//a[@href]"))
                {
                    if (hyperlink.InnerText == InnerText)
                        return hyperlink.Attributes["href"].Value;
                }
            }
            return null;
        }

        public async Task<string> getAllChapters(int chaptersCount)
        {
            Queue<Chapter> queue = new Queue<Chapter>();
            Chapter chapter;
            string url = _url;

            for (int i = 1; i <= chaptersCount; i++)
            {
                await getMessage(url);
                chapter = getChapterByClass(_rules, i);
                queue.Enqueue(chapter);
            }

            string all = string.Empty;

            var getChapter = new Chapter();
            while (queue.TryDequeue(out getChapter!))
            {
                all += getChapter.Content + "\n";
            }
            return all;
        }
    }
}
