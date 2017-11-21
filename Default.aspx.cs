using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Diagnostics;
using HtmlAgilityPack;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnDownload_Click(object sender, System.EventArgs e)
    {
        DownloadBook();
    }

    public void DownloadBook() {
        EBook book = new EBook();
        WebRequestHandler wrh = new WebRequestHandler();
        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
        HTMLScraper scraper = new HTMLScraper();
        EmailHandler emailHandler = new EmailHandler();
        WebClientHandler wch = new WebClientHandler();
        try
        {
            
            tbLog.Text += "Initializing";
            lblDownloadPercent.Text = "Downloading";

            book.bookTitle = tbBookName.Text;
            book.author = tbAuthor.Text;
            string downloadFolder = tbDownloadFolder.Text;

            string hostSiteSearchPage = "http://libgen.io/search.php?req=";
            book.uriBookTitle = System.Text.RegularExpressions.Regex.Replace(book.bookTitle +" - " + book.author, @"\s+", "+");

            //returns htmlData page
            string htmlData = wrh.ProcessWebRequest(hostSiteSearchPage, book.uriBookTitle);

            string filePath = downloadFolder + "\\" + book.bookTitle + " - " + book.author + book.fileType; //C:\users\Alex\Downloads\Outliers-Malcolm Gladwell.mobi
            book.fileLocation = filePath;
            book.coverImageLocation = downloadFolder + "\\" + book.bookTitle + "Cover";
            //Parse htmldata with XPath and return link for file types mobi, epub, pdf
            string downloadPage = scraper.GetBookLinks(htmlData, book);
            lblDownloadPercent.Text = "Downloading " + book.fileType;
            if (downloadPage != "")
            {
                wch.Download(downloadPage, book.fileLocation, book);
                if (book.fileType == ".pdf" || book.fileType == ".epub")
                {
                    scraper.GetCoverImage(book);
                }
            }
            else
            {
                tbLog.Text += "No link results";
            }

            
        }
        catch (Exception s)
        {
            //TODO Log error
        }
    }
}