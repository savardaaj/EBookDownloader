using System;
using System.Web.UI;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

public partial class _Default : Page
{

    public volatile bool _completed = false;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnDownload_Click(object sender, System.EventArgs e)
    {
        DownloadBook();
    }

    /*
     * Main function for program. 
     * TODO: init 
     */
    public void DownloadBook() {
        EBook book = new EBook();
        WebRequestHandler wrh = new WebRequestHandler();
        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
        HTMLScraper scraper = new HTMLScraper();
        EmailHandler emailHandler = new EmailHandler();
        WebClientHandler wch = new WebClientHandler();
        string downloadFolder = "";
        string coverImageHref = "";
        string hostSiteSearchPage = "http://libgen.io/search.php?req=";
        string htmlData = "";
        string filePath = "";
        string downloadPage = "";
        string downloadLink = "";
        try
        {
            tbLog.Text += "Initializing";
            lblDownloadPercent.Text = "Downloading";

            book.bookTitle = tbBookName.Text;
            book.author = tbAuthor.Text;
            downloadFolder = tbDownloadFolder.Text;

            Directory.CreateDirectory(downloadFolder);

            book.uriBookTitle = Regex.Replace(book.bookTitle + " - " + book.author, @"\s+", "+");
            filePath = downloadFolder + "\\" + book.bookTitle + " - " + book.author + book.fileType; //C:\users\Alex\Downloads\Outliers-Malcolm Gladwell.mobi
            
            book.coverImageLocation = downloadFolder + "\\" + book.bookTitle + " Cover";

            //Parse htmldata with XPath and return link for file types mobi, epub, pdf
            //returns htmlData page for initial search
            htmlData = wrh.ProcessWebRequest(hostSiteSearchPage, book.uriBookTitle);
            
            //Will set book.filetype, returns html with link to download
            downloadPage = scraper.GetBookLinks(htmlData, book);

            book.fileLocation = filePath + book.fileType;

            htmlData = wrh.ProcessWebRequest(downloadPage, "");

            downloadLink = scraper.GetDownloadLink(htmlData);


            if (downloadLink != "")
            {
                lblDownloadPercent.Text = "Downloading " + book.fileType;
                //Download the EBook
                Download(downloadLink, book.fileLocation, book);
                if (book.fileType == ".pdf" || book.fileType == ".epub")
                {
                    coverImageHref = scraper.GetCoverImage(book);
                    Download(coverImageHref, book.coverImageLocation, book);
                }
                emailHandler.SendEmail(book, tbKindleEmail.Text);
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

    public void Download(string downloadPage, string filePath, EBook book)
    {
        try
        {
            WebClient wc = new WebClient();
            HTMLScraper scraper = new HTMLScraper();
            wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
            wc.DownloadProgressChanged += (sender, ex) =>
            {
                lblDownloadPercent.Text = "Downloaded " + ex.BytesReceived + "b of " + ex.TotalBytesToReceive + "b " + book.fileType;
            };
            wc.DownloadFileCompleted += (sender, ex) =>
            {
                if (ex.Cancelled)
                {
                    //TODO log error
                }
                else
                {
                    _completed = true;
                }
            };
            wc.DownloadFileAsync(new Uri(downloadPage), filePath);
            Thread.Sleep(10000);
            //while (!_completed)
            //    Thread.Sleep(1000);
        }
        catch (Exception e)
        {

        }
    }
        
}