using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for WebClientHandler
/// </summary>
public class WebClientHandler
{
    public WebClientHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void Download(string downloadPage, string filePath, EBook book)
    {
        WebClient wc = new WebClient();
        HTMLScraper scraper = new HTMLScraper();
        wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
        wc.DownloadProgressChanged += (sender, ex) =>
        {
            //updateProgress.Attributes. = ex.ProgressPercentage;
            //lblDownloadPercent.Text = "Downloaded " + ex.BytesReceived + " of " + ex.TotalBytesToReceive + " " + book.fileType;
        };
        wc.DownloadFileCompleted += (sender, ex) =>
        {
            if (ex.Cancelled)
            {
                //TODO log error
            }
        };
        wc.DownloadFileAsync(new Uri(downloadPage), filePath);
    }
}