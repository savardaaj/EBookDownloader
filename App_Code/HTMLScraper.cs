using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for HTMLScraper
/// </summary>
public class HTMLScraper
{
    public HTMLScraper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetBookLinks(string htmlData, EBook book)
    {
        string mobiLink = "";
        string epubLink = "";
        string pdfLink = "";
        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        //Loads entire page
        htmlDoc.LoadHtml(htmlData);

        //All tables elements in the document
        foreach (HtmlNode table in htmlDoc.DocumentNode.SelectNodes("//table"))
        {
            foreach (HtmlNode row in table.SelectNodes("tr"))
            {
                foreach (HtmlNode cell in row.SelectNodes("th|td"))
                {
                    //7th td has language
                    //8th td is size
                    //9th td is extension
                    //10th td is link [1]
                    
                    if(cell.InnerText == "English")
                    {
                        //Get the fileType td
                        HtmlNode tdFileType = cell;
                        while (tdFileType.NextSibling != null)
                        {
                            tdFileType = tdFileType.NextSibling;

                            if (tdFileType.InnerText == "mobi")
                            {
                                book.fileType = ".mobi";
                                mobiLink = tdFileType.NextSibling.NextSibling.FirstChild.Attributes["href"].Value;
                                //Mobi is the most desired format so return if we find it
                                return mobiLink;
                            }
                            else if (tdFileType.InnerText == "epub")
                            {
                                epubLink = tdFileType.NextSibling.NextSibling.FirstChild.Attributes["href"].Value;
                            }
                            else if (tdFileType.InnerText == "pdf")
                            {
                                pdfLink = tdFileType.NextSibling.NextSibling.FirstChild.Attributes["href"].Value;
                            }
                        }
                    }
                }
            }
        }
        if (epubLink != "")
        {
            book.fileType = ".epub";
            return epubLink;
        }
        else if (pdfLink != "")
        {
            book.fileType = ".pdf";
            return pdfLink;
        }
        return "";
    }

    public string GetCoverImageLink(string htmlData, EBook book)
    {
        Regex reg = new Regex(@"(\/book\/show\/[0-9]*." + (book.bookTitle.Replace(' ', '_')) + "?)");
        Match match;

        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        //Loads entire page
        htmlDoc.LoadHtml(htmlData);

        //All tables elements in the document
        foreach (HtmlNode table in htmlDoc.DocumentNode.SelectNodes("//table"))
        {
            foreach (HtmlNode row in table.SelectNodes("tr"))
            {
                foreach (HtmlNode cell in row.SelectNodes("th|td"))
                {
                    match = reg.Match(cell.InnerHtml);
                    if (match.Success)
                    {
                        return match.ToString() + "?";
                    }
                    //var temp = cell.SelectNodes("//*[contains(., 'href=\"/book/show/ ')]"); //download link on next page

                }
            }
        }
        return "No Link Found";
    }

    public void GetCoverImage(EBook book)
    {
        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
        WebRequestHandler wrh = new WebRequestHandler();
        WebClientHandler wch = new WebClientHandler();        
        EmailHandler eh = new EmailHandler();
        
        //Search URL: https://www.goodreads.com/search?q=
        //Search Result URL: /book/show/77566.Hyperion
        //has the link to the page with the image we want
        string hostSiteSearchPage = "https://www.goodreads.com/search?q=";
        
        string htmlData = wrh.ProcessWebRequest(hostSiteSearchPage, book.uriBookTitle);

        string coverImageLink = GetCoverImageLink(htmlData, book);

        //Page that has the image we want
        hostSiteSearchPage = "https://www.goodreads.com";

        htmlData = wrh.ProcessWebRequest(hostSiteSearchPage, coverImageLink);
        htmlDoc.LoadHtml(htmlData);
        var link = htmlDoc.DocumentNode.Descendants("img")
            .First(x => x.Attributes["id"] != null
                     && x.Attributes["id"].Value == "coverImage");
        string coverImageHref = link.Attributes["src"].Value;

        if (coverImageHref != "")
        {
            var temp = coverImageHref.Split('.');
            book.coverImageLocation += "." + temp[temp.Length -1];
            wch.Download(coverImageHref, book.coverImageLocation, book);
            eh.SendEmail(book.coverImageLocation, book);
        }
    }
}