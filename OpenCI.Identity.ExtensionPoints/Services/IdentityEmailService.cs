using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.ExtensionPoints.Services
{
    public class IdentityEmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var host = ConfigurationManager.AppSettings["mailjet:host"];
            var port = int.Parse(ConfigurationManager.AppSettings["mailjet:port"]);
            var username = ConfigurationManager.AppSettings["mailjet:username"];
            var password = ConfigurationManager.AppSettings["mailjet:password"];
            var fromAddress = ConfigurationManager.AppSettings["mailjet:from"];

            var mailMessage = new MailMessage();
            mailMessage.To.Add(message.Destination);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.From = new MailAddress(fromAddress);

            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}