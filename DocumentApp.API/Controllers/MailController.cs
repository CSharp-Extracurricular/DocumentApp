using MimeKit;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly Mailer _mailer = new("", "");

    [HttpPost]
    public async Task Send(MailboxAddress receiverAddress, Guid publicationId)
    {
        string link = $"https://localhost:7204/api/View/{publicationId}";
        await _mailer.SendImportLinkTo(receiverAddress, new Uri(link));
    }
}