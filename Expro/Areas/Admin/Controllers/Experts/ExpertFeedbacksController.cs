using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers.Experts
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ExpertFeedbacksController : Controller
    {
        public IActionResult Index(string userID = "")
        {
            return View();
        }
    }
}