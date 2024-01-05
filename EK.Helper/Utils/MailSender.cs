using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EK.Helper.Utils
{
    internal class MailSender
    {
        public const string SENDERMAIL = "omer.oksuz2004@outlook.com";
        public const string SENDERPASSWORD = "Omeromur2004";

        public static void Send(IEnumerable<string> mailAddresses, string title, string message)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp-mail.outlook.com";
            client.EnableSsl = true;
            client.Timeout = 50000;

            string senderMail = SENDERMAIL;
            string senderPassword = SENDERPASSWORD;
            client.Credentials = new NetworkCredential(senderMail, senderPassword);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(senderMail, "EMRULLAH KOÇ");

            foreach (string mailAddress in mailAddresses)
            {
                mail.To.Add(mailAddress);
            }

            mail.Subject = title;
            mail.Body = message;
            mail.IsBodyHtml = true;

            client.Send(mail);
        }
    }
}
