using Microsoft.AspNetCore.Mvc;
using QioskAPI.Interfaces;
using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    { 
        IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost, Route("SendEmail")]
        public async void SendEmail(Mail mail, string Link)
        {
            try
            {
               
                mail.Message += "</br>" +
                    "<div>"+
                    "<p>Prettige dag verder!</p>" +
                    "<p>Team Qiosk</p>" +
                  "</div>";
                _mailService.SendEmailAsync(mail.RecipientEmail, mail.RecipientName, mail.Subject, mail.Message);
            }

            catch (Exception ex)
            {
                BadRequest(ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message);
            }
        }
    }
}
