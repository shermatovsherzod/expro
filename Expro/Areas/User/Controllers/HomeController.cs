using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "SimpleUserOnly")]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}