using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailSendingController : Controller
    {
        private readonly IEmailService _emailService;
        public EmailSendingController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            ViewBag.Message = false;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string emailaddress, string subject, string emailBody)
        {
            ViewBag.Message = true;
            await _emailService.SendEmailAsync(emailaddress, subject, emailBody);
            return View();
        }
    }
}