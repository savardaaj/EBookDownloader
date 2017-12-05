using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for EmailHandler
/// </summary>
public class EmailHandler
{
    public EmailHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void SendEmail(EBook book, string toEmail)
    {
        try
        {
            //put these in a config file. Encrypt?
            NetworkCredential credentials = new NetworkCredential("", "");
            FileTypeConverter ftc = new FileTypeConverter();
            MailMessage mail = new MailMessage(new MailAddress(""), new MailAddress(toEmail));
            mail.Subject = "ebook";
            mail.Body = "";
            if (book.fileType != ".mobi")
            {
                book.fileLocation = ftc.ConvertToMobi(book.fileLocation, book.coverImageLocation);
                if(book.fileLocation == "")
                {
                    return;
                }
            }
            mail.Attachments.Add(new Attachment(book.fileLocation));

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";

            client.Credentials = credentials;

            client.Send(mail);

        }
        catch (Exception ex)
        {
            //TODO handle error
            return;
        }
    }
}