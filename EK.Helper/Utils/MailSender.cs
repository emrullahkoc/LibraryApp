using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EK.Helper.Utils
{
	public static class MailSender
	{

		//Solution sağ tıklayıp "new project" arama kısmına"Class Library" yazıp seçiyoruz.Oluşan klasörde oluşan class siliyoruz.sonrasında "utils" klasörü açıp istediğimiz bi class açıyoruz. Diğer projede kullana bilmek için projeye sağ tıklayıp "Add" ardından "Project Reference" tıklayıp oluşturduğumuz ve kullanmak istediğimiz kütüphaneyi seçip onaylıyoruz.

		public const string SENDERMAIL = "emrullahkocccc@outlook.com"; //Mailin yollanacağı mail adresi
        public const string SENDERPASSWORD = "dw30C6f-03IzrOpBj{9wdCYq&!i6.z~VQ2PuBED{Z}PRkrp^wz"; //Mailin yollanacağı mailin şifresi

        public static void Send(IEnumerable<string> mailAddresses, string title, string message)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587; //Outlook ayarları
            client.Host = "smtp-mail.outlook.com"; //Outlook ayarları
            client.EnableSsl = true;
            client.Timeout = 50000; //Mailin ne kadar süre içinde gitmesi gerektiği

            string senderMail = SENDERMAIL;
            string senderPassword = SENDERPASSWORD;
            client.Credentials = new NetworkCredential(senderMail, senderPassword);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(senderMail, "EMRULLAH KOÇ"); // Gönderici Mail , Gönderen Adı

            foreach (string mailAddress in mailAddresses) //Listedeki Mail Adreslerini Döner
            {
                mail.To.Add(mailAddress);
            }

            mail.Subject = title; //Mesaj Başlığı
            mail.Body = message; //Mesaj İçeriği
            mail.IsBodyHtml = true; //Mesajın Html formatında olduğunu belirtir.

            client.Send(mail);
        }
	}
}
