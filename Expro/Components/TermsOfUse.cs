using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class TermsOfUse : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("TermsOfUse");
        }
    }
}
