using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            ProcessStartInfo startInfo = new ProcessStartInfo(@"C:\Program Files\Calibre2\ebook-convert.exe");
            startInfo.UseShellExecute = false;
            startInfo.FileName = (@"ebook-convert.exe");
            startInfo.WorkingDirectory = Path.GetDirectoryName(@"C:\Program Files\Calibre2\ebook-convert.exe");
            startInfo.ErrorDialog = true;
            if (coverImage == "")
            {
                startInfo.Arguments = "\"" + oldFile + "\"" + " \"" + newFile + "\"";
            }
            else
            {
                startInfo.Arguments = "\"" + oldFile + "\"" + " \"" + newFile + "\"" + " --cover " + "\"" + coverImage + "\"";
            }
            //converter.StartInfo.CreateNoWindow = true;
            try
            {
                Process.Start(startInfo);
                //startInfo.WaitForExit();
            }
            catch(Exception e)
            {
                //Converter threw exception
            }
            return newFile;
        }
        catch (Exception ex)
        {
            return "";
        }
    }
}