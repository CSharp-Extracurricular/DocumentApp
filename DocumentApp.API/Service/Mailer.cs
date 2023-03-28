using System.Net;
using System.Net.Mail;

namespace DocumentApp.API
{
    public class Mailer
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _senderAddress = new("example@example.com");

        public Mailer(string username, string password, bool useSsl, int port = 587)
        {
            _smtpClient = new SmtpClient()
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = useSsl
            };
        }

        public void SendImportLinkTo(MailAddress receiverAddress, Uri link)
        {
            MailMessage message = new(_senderAddress, receiverAddress);
            message.Body = $"Hello, {receiverAddress.Address}, here is your link to import publication: {link}";
            _smtpClient.Send(message);
        }
    }
}
