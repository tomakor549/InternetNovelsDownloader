﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using WebsiteNovelsDownloader.Models;

namespace WebsiteNovelsDownloader.Downloaders
{
    internal static class Downloader
    {
        public static string? NextUrlByHyperlink(HtmlDocument websiteContent, List<Rules> parentNodeRules, HyperlinkRules linkRules)
        {
            var parentNode = Extract(websiteContent, parentNodeRules);
            if(parentNode == null)
            {
                return null;
            }

            foreach (var hyperlink in parentNode.SelectNodes(linkRules.AttributeRule()))
            {
                if (hyperlink.InnerText == linkRules.TextValue)
                    return hyperlink.Attributes["href"].Value;
            }

            return null;
        }

        public static Chapter CreateChapter(HtmlDocument websiteContent, int chapterNumber, List<Rules> extractRules)
        {
            Console.WriteLine("Downloader.CreateChapterAsync()");
            var extractNode = Extract(websiteContent, extractRules);

            return new Chapter()
            {
                Number = chapterNumber,
                Content = extractNode,
            };
        }

        private static HtmlNode? Extract(HtmlDocument websiteContent, List<Rules> rules)
        {
            HtmlNodeCollection? nodes;
            foreach(var rule in rules)
            {
                var ruleString = rule.ContainsRule();
                nodes = websiteContent.DocumentNode.SelectNodes(ruleString);
                if (nodes != null)
                {
                    if (nodes.Count == 1)
                    {
                        return nodes.LastOrDefault();
                    }
                }
            }
            return null;
        }

        public static async Task<HtmlDocument?> WebsiteContentAsync(string url)
        {
            Console.WriteLine("Downloader.getMessage(): download with " + url);

            if (!ValidateUrl(url))
            {
                Console.WriteLine("Downloader.getMessage(): The given argument is not a link.");
                //throw new ArgumentException("Downloader.getMessage(): The given argument is not a link.");
                return null;
            }

            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument document = new();
                document.LoadHtml(responseBody);
                return document;
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Downloader.getMessage(): You cannot download the content from the link ");
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Downloader.getMessage(): Exception " + ex.Message);
                return null;
            }
        }

        public static string? ChapterToString(Chapter chapter)
        {
            if (chapter.Content == null)
            {
                return null;
            }
            return $"<h2>Rozdział {chapter.Number}</h2>\n" + chapter.Content.OuterHtml;
        }

        public static bool ValidateUrl(string url)
        {
            Uri uriResult;
            bool create = Uri.TryCreate(url, UriKind.Absolute, out uriResult!);
            bool scheme = uriResult.Scheme != Uri.UriSchemeHttp;

            if (!create || !scheme)
            {
                return false;
            }
            return true;
        }
    }
}
