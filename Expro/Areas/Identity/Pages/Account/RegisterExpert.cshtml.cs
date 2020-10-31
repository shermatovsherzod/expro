using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Expro.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterExpertModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly ILawAreaService _lawAreaService;
        private readonly IRegionService _regionService;
        private readonly ICityService _cityService;
        //private readonly IEmailSender _emailSender;
        private readonly IUserBalanceService _userBalanceService;

        public RegisterExpertModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            ILawAreaService lawAreaService,
            IRegionService regionService,
            ICityService cityService,

            //IEmailSender emailSender
            IUserBalanceService userBalanceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _lawAreaService = lawAreaService;
            _regionService = regionService;
            _cityService = cityService;
            _userBalanceService = userBalanceService;
        }

        [BindProperty]
        public InputExpertModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputExpertModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Почтовый адрес")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Подтверждение пароля")]
            [Compare("Password", ErrorMessage = "Введенные пароли не совпадают.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Имя")]
            [StringLength(256)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Фамилия")]
            [StringLength(256)]
            public string LastName { get; set; }

            [Display(Name = "Отчество")]
            [StringLength(256)]
            public string PatronymicName { get; set; }

            //[Display(Name = "Тип пользователя")]
            //public int UserType { get; set; }

            [Required]
            [Display(Name = "Телефон")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Регион")]
            public int RegionID { get; set; }

            [Display(Name = "Город")]
            public int? CityID { get; set; }

            [Display(Name = "Другой город")]
            [StringLength(256)]
            //[Remote("ValidateFrom", "VideoRequest", ErrorMessage = "Введите город", AdditionalFields = "TypeID")]
            public string CityOther { get; set; }

            [Required]
            [Display(Name = "Направление")]
            public List<int> LawAreas { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PatronymicName = Input.PatronymicName,
                    PhoneNumber = Input.PhoneNumber,
                    UserType = (int)UserTypesEnum.Expert,
                    RegionID = Input.RegionID,
                    CityID = Input.CityID == 0 ? null : Input.CityID,
                    CityOther = Input.CityID == null ? Input.CityOther : null
                };
                _userBalanceService.AssignAccountNumber(user);
                _lawAreaService.UpdateUserLawAreas(user, Input.LawAreas);

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //string emailSubject = "Confirm your email";
                    string emailBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                    //await _emailSender.SendEmailAsync(Input.Email, emailSubject, emailBody);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["lawAreas"] = _lawAreaService.GetAsSelectList();
            ViewData["regions"] = _regionService.GetAsSelectList();

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
