using System.Xml.XPath;
using WebsiteNovelsDownloader.Downloaders;
using System;
using WebsiteNovelsDownloader.Models;
using Attribute = WebsiteNovelsDownloader.Models.Attribute;
using System.Net;

var title = "Hard Carry Support";
int actualChapter = 1;
var url = $@"https://reaperscans.com/novels/9881-hard-carry-support/chapters/19880403-chapter-1";
int finalChapter = 150;
//prose dark:prose-invert max-w-none my-4
var extractRules = new List<Rules>
    {
        new ArticleRules(Attribute.Class, "my-4"),
        new ArticleRules(Attribute.Class, "max-w-none"),
        new ArticleRules(Attribute.Class, "prose-invert"),
        //new ArticleRules(Attribute.Class, "category-tcf"),
    };


var hrefExtractRules = new List<Rules>
    {
       new DivRules(Attribute.Class, "mb-2"),
    };
var nextUrlRule = new HyperlinkRules(Attribute.Class, "ml-2");
var novelName = $"{title} {actualChapter}-{finalChapter}.html";

File.Create(novelName).Dispose();
while (actualChapter <= finalChapter)
{
    Console.WriteLine("Chapter " + actualChapter);
    if (url == null)
    {
        throw new Exception("Url is null");
    }
    var websiteContent = await Downloader.WebsiteContentAsync(url);
    if(websiteContent == null)
    {
        throw new Exception("Content is null");
    }

    File.AppendAllText(
        novelName,
        Downloader.ChapterToString(Downloader.CreateChapter(websiteContent, actualChapter, extractRules))
    );
    url = Downloader.NextUrlByHyperlink(websiteContent, nextUrlRule);
    actualChapter++;
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