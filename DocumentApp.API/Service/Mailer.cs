using System.Net;
using System.Net.Mail;

namespace DocumentApp.API
{
    // TODO: Use MailKit instead of System.Net.Mail.
    public class Mailer
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _senderAddress = new("example@example.com");

        public Mailer(string username, string password, bool useSsl, int port = 587) =>
            _smtpClient = new SmtpClient
            {
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = useSsl
            };

        public async Task SendImportLinkTo(MailAddress receiverAddress, Uri link)
        {
            MailMessage message = new(_senderAddress, receiverAddress)
            {
                Subject = "Publication import link",
                Body = $"Hello, {receiverAddress.Address}, here is your link to import publication: {link}"
            };
            
            await _smtpClient.SendMailAsync(message);
        }
    }
}
