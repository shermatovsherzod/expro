using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Expro.Models;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace Expro.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userType = User.Claims.FirstOrDefault(c => c.Type == "UserType");
                if (userType != null)
                {
                    if (int.Parse(userType.Value) == 1)
                    {
                        return Redirect("/Admin/Home");
                    }
                    else if (int.Parse(userType.Value) == 2)
                    {
                        return Redirect("/Expert/Home");
                    }
                    else if (int.Parse(userType.Value) == 3)
                    {
                        return Redirect("/User/Home");
                    }
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Foobar()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
