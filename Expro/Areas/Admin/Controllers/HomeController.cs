using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}