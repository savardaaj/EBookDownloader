using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FileTypeConverter
/// </summary>
public class FileTypeConverter
{
    public FileTypeConverter()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string ConvertToMobi(string fileName, string coverImage)
    {
        try
        {
            string newFile = fileName.Split('.')[0] + ".mobi";
            string oldFile = fileName;
            Process converter = new Process();
            converter.StartInfo.UseShellExecute = true;
            converter.StartInfo.FileName = (@"C:\Program Files\Calibre2\ebook-convert.exe");
            if (coverImage == "")
            {
                converter.StartInfo.Arguments = "\"" + oldFile + "\"" + " \"" + newFile + "\"";
            }
            else
            {
                converter.StartInfo.Arguments = "\"" + oldFile + "\"" + " \"" + newFile + "\"" + " --cover " + "\"" + coverImage + "\"";
            }
            //converter.StartInfo.CreateNoWindow = true;
            converter.Start();
            return newFile;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}