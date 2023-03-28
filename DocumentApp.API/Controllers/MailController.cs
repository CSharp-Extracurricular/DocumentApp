using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace DocumentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly Mailer _mailer = new("", "", true, 587);

        [HttpPost]
        public void Send(MailAddress receiverAddress)
        {

        }
    }
}
