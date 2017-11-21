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

    public void SendEmail(string attachment, EBook book)
    {
        try
        {
            //put these in a config file. Encrypt?
            NetworkCredential credentials = new NetworkCredential("dummymailsail@gmail.com", "1.13198824");
            FileTypeConverter ftc = new FileTypeConverter();
            //if (tbEmail.Text == "")
            //{
            //    textBoxToEmail.BackColor = System.Drawing.Color.PaleVioletRed;
            //}
            //else
            //{
            //    textBoxToEmail.BackColor = System.Drawing.Color.White;

                MailMessage mail = new MailMessage(new MailAddress("dummymailsail@gmail.com"), new MailAddress("savarda91_01@kindle.com"));
                mail.Body = "Doesn't matter";
                if (!(attachment.Split('.')[1].Equals("mobi"))) //If the attchment file is not a mobi, convert it
                {
                    attachment = ftc.ConvertToMobi(attachment, book.coverImage);
                    if (!File.Exists(attachment))
                    {
                        return;
                    }
                }
                mail.Attachments.Add(new Attachment(attachment));
                //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess;

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