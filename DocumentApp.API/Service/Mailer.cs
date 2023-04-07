using MimeKit;
using MailKit.Net.Smtp;

namespace DocumentApp.API;

public class Mailer
{
    private readonly MailboxAddress _senderAddress = new("Example", "example@example.com");
    private readonly MailerSettings _settings;

    public Mailer(string username, string password)
    {
        _settings = new MailerSettings(username, password);
    }

    public async Task SendImportLinkTo(MailboxAddress receiverAddress, Uri link)
    {
        MimeMessage message = new(_senderAddress, receiverAddress)
        {
            Subject = "Publication import link",
            Body = new TextPart("plain")
            {
                Text = $"Hello, {receiverAddress.Address}, here is your link to import publication: {link}"
            }
        };

        using (SmtpClient client = new())
        {
            await client.ConnectAsync(MailerSettings.Server, _settings.Port);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

public struct MailerSettings
{
    public const string Server = "smtp.gmail.com";
    public int Port = 587;
    public string Username;
    public string Password;

    public MailerSettings(string username, string password)
    {
        Username = username;
        Password = password;
    }
}