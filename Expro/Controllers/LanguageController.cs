using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expro.Controllers.API
{
    public class LanguageController : ControllerBase
    {
        private readonly IStringLocalizer<Resources.ResourceTexts> _localizer;
        public LanguageController(IStringLocalizer<Resources.ResourceTexts> localizer)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var guid = Guid.NewGuid();
            return Ok(_localizer["RandomGUID", guid.ToString()].Value);
        }

        [HttpPost]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
    }
}
