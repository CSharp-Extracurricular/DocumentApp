using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly Mailer _mailer = new("", "", true);

        [HttpPost]
        public async Task Send(MailAddress receiverAddress, Guid publicationId)
        {
            string publicationUri = $"https://localhost:7204/api/View/{publicationId}";
            string link = $"https://localhost:7204/api/Import/{publicationUri}";
            await _mailer.SendImportLinkTo(receiverAddress, new Uri(link));
        }
    }
}
