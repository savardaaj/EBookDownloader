using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for WebRequestHandler
/// </summary>
public class WebRequestHandler
{
    public WebRequestHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //Returns html link to download page
    public string ProcessWebRequest(string hostSiteSearchPage, string target)
    {
        Stream stream;
        StreamReader reader;
        Uri websiteURI = new Uri(hostSiteSearchPage + target); //declare page to search
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(websiteURI);
        HTMLScraper htmlScraper = new HTMLScraper();

        request.UserAgent = "A .NET Web Crawler";

        string htmlData = "";

        //Get the HTML text / Load it into parser
        using (WebResponse response = request.GetResponse())
        {
            stream = response.GetResponseStream();
            reader = new StreamReader(stream);

            htmlData = reader.ReadToEnd();
        }

        return htmlData;
    }

}