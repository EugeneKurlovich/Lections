using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Lections.Services
{
    public class EmailSender
    {
        public void ConfirmEmail(string Email, string url)
        {

            Email email = JsonConvert.DeserializeObject<Email>(File.ReadAllText(@"emailconfig.json"));

            MailAddress from = new MailAddress(email.login, "Web Registration");
            MailAddress to = new MailAddress(Email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Email confirmation";

            m.Body = string.Format("To complete the registration, click " +
            "<a href=\"{0}\" title=\"here\">{0}</a>",
            url);
            m.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;

           
            smtp.Credentials = new System.Net.NetworkCredential(email.login, email.password);
            smtp.Send(m);
        }
    }
}
