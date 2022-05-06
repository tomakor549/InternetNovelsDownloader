using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WebsiteNovelsDownloader.Models;

namespace WebsiteNovelsDownloader.Downloaders
{
    internal class Downloader
    {
        protected HtmlDocument _document = new();

        protected string _url;

        protected List<string> _rules;

        public Downloader(string url, List<string> rules)
        {
            Uri uriResult;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uriResult!) || uriResult.Scheme != Uri.UriSchemeHttps)
                throw new ArgumentException("The given url is not a link");
            _url = url;
            _rules = rules;
        }

        protected async Task getMessage(string url)
        {
            Uri uriResult;
            bool create = Uri.TryCreate(url, UriKind.Absolute, out uriResult!);
            bool scheme = uriResult.Scheme != Uri.UriSchemeHttp;
            if (!create || !scheme)
                throw new ArgumentException("The given argument is not a link.");

            HttpClient client = new HttpClient();
            string responseBody = "";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            _document.LoadHtml(responseBody);
        }

        protected Chapter getChapterByClass(List<string> rules, int chapterNumber)
        {
            HtmlNodeCollection? nodes = null;
            foreach (string rule in rules)
            {
                nodes = _document.DocumentNode.SelectNodes(rule);
                if (nodes?.Count > 0)
                {
                    if (nodes.Count > 1)
                        throw new ArgumentException("This class name is more than one.");
                }
            }

            if (nodes == null)
                throw new ArgumentException("there are no such classes");

            var node = nodes[0];

            //if (!IsClearChapterActive)
                return new Chapter()
                {
                    Number = chapterNumber,
                    Content = node.OuterHtml,
                };
        }
    }
}
