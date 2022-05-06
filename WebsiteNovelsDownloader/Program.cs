using WebsiteNovelsDownloader.Downloaders;

var url = "";
var urlDownloader = new UrlDownloader(
    "",
    "chapter-",
    "/",
    new List<string>{ "//div[contains(@class, \"{new-content}\")]", "//div[contains(@class, \"text-left\")]" });

var divs = await urlDownloader.getAllChapters(5, 15);
string fileName = "Novel.html";

File.WriteAllText(fileName, divs);