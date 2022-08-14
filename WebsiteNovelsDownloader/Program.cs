using System.Xml.XPath;
using WebsiteNovelsDownloader.Downloaders;
using System;
using WebsiteNovelsDownloader.Models;
using Attribute = WebsiteNovelsDownloader.Models.Attribute;

var url = "https://singletranslations.blogspot.com/2021/11/adaa-chapter-30.html";
int chaptersCount = 30;
var extractRules = new List<Rules>
    {
        new DivRules(Attribute.Id, "post-body"),
    };
var nextUrlRule = new HyperlinkRules(Attribute.Href, "Next&gt;");
var novelName = "AADA.html";

var chapters = new List<DownloadChapter>();

File.Create(novelName).Dispose();
while (chaptersCount < 100)
{
    if (url == null)
    {
        throw new Exception("Url is null");
    }
    var websiteContent = await Downloader.WebsiteContentAsync(url);
    if(websiteContent == null)
    {
        throw new Exception("Content is null");
    }

    if (websiteContent == null)
    {
        return;
    }

    File.AppendAllText(
        novelName,
        Downloader.ChapterToString(Downloader.CreateChapter(websiteContent, chaptersCount, extractRules))
    );
    url = Downloader.NextUrlByHyperlink(websiteContent, extractRules, nextUrlRule);
    chaptersCount++;
}



//string fileName = "Novel.html";

////File.WriteAllText(fileName, divs);

//var url = "https://singletranslations.blogspot.com/2021/10/adaa-chapter-0.html";
//var query = $"//manifest/item[@id='post-body']";

//var contentUriDownloader = new ContentUrlDownloader(
//    url,
//    new List<string>
//    {
//        "//div[contains(@id, \"post-body\")]",
//        query
//        //"//div[contains(@id, \"text-left\")]"
//    });


////var urlDownloader = new UrlDownloader(
////    url,
////    "chapter-",
////    "/",
////    new List<string>{ "//div[contains(@class, \"{entry-content}\")]", "//div[contains(@class, \"text-left\")]" });

//var divs = await contentUriDownloader.getAllChapters(120);
//string fileName = "Novel.html";

//File.WriteAllText(fileName, divs);