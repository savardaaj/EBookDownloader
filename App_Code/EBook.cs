using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Book
/// </summary>
public class EBook
{
    public string fileType { get; set; }
    public string fileLocation { get; set; }
    public string bookTitle { get; set; }
    public string uriBookTitle { get; set; }
    public string author { get; set; }
    public string coverImage { get; set; }
    public string coverImageLocation { get; set; }
    public int fileSize { get; set; }

    public EBook()
    {
        //
        // TODO: Add constructor logic here
        //
    }

}