using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Expro.Common;
using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Expro.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        //private readonly IHostingEnvironment _env;

        public AccountController(
            //IHostingEnvironment env,
            ILogger<AttachmentController> logger,
            IUserService userService)
        {
            //_env = env;
            _logger = logger;
            _userService = userService;
        }

        public IActionResult IsBlocked()
        {
            var curUser = accountUtil.GetCurrentUser(User);
            var appUser = _userService.GetByID(curUser.ID);

            bool isBlocked = _userService.IsBlocked(appUser);

            return Ok(new { isBlocked = isBlocked });
        }

        public IActionResult Block()
        {
            return View();
        }
    }
}