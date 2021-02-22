using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Resources;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Expro.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly IUserBalanceService _userBalanceService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            //IEmailSender emailSender
            IUserBalanceService userBalanceService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            //_emailSender = emailSender;
            _userBalanceService = userBalanceService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email", ResourceType = typeof(ResourceTexts))]
            public string Email { get; set; }

            [Required]           
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "lblConfirmPassword", ResourceType = typeof(ResourceTexts))]
            [Compare("Password", ErrorMessageResourceName = "lblConfirmPasswordError", ErrorMessageResourceType = typeof(ResourceTexts))]
            public string ConfirmPassword { get; set; }

            [Display(Name = "lblFirstName", ResourceType = typeof(ResourceTexts))]
            public string Name { get; set; }

            [Display(Name = "lblLastName", ResourceType = typeof(ResourceTexts))]
            public string Surname { get; set; }

            [Display(Name = "lblPatronymicName", ResourceType = typeof(ResourceTexts))]
            public string Patronymic { get; set; }

            [Display(Name = "lblDateOfBirth", ResourceType = typeof(ResourceTexts))]
            public DateTime DateOfBirth { get; set; }

            [Display(Name = "lblGender", ResourceType = typeof(ResourceTexts))]
            public string Gender { get; set; }

            [Display(Name = "Тип пользователя")]
            public string UserType { get; set; }

            [Display(Name = "Телефон")]
            public string PhoneNumber { get; set; }

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
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
                    FirstName = Input.Name,
                    LastName = Input.Surname,
                    PhoneNumber = Input.PhoneNumber,
                    UserType = UserTypesEnum.SimpleUser,
                    DateRegistered = DateTime.Now,
                    UserStatusID = 1
                };
                _userBalanceService.AssignAccountNumber(user);

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

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
