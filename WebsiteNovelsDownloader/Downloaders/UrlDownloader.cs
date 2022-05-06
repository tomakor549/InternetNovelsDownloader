using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteNovelsDownloader.Models;

namespace WebsiteNovelsDownloader.Downloaders
{
    internal class UrlDownloader : Downloader
    {
        private string _startPattern;

        private string _endPattern;
        public UrlDownloader(string url, string startPattern, string endPattern, List<string> rules): base(url, rules)
        {
            _startPattern = startPattern;
            _endPattern = endPattern;
        }

        public async Task<string> getAllChapters(int chapterFirst, int chapterLast)
        {
            Queue<Chapter> queue = new Queue<Chapter>();
            Chapter chapter;

            if (chapterFirst > chapterLast)
            {
                (chapterFirst,chapterLast) = (chapterLast,chapterFirst);
            }
            for (int i = chapterFirst; i <= chapterLast; i++)
            {
                await getMessage(_url + _startPattern + i + _endPattern);
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
