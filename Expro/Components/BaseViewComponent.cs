using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class BaseViewComponent : ViewComponent
    {
        public AccountUtil accountUtil = new AccountUtil();

        public BaseViewComponent()
        {
        }

        public virtual IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
