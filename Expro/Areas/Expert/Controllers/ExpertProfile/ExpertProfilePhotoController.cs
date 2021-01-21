using System.Linq;
using Expro.Controllers;
using Expro.Models;
using Expro.ViewModels.Expert;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Expert.Controllers.ExpertProfile
{
    [Area("Expert")]
    public class ExpertProfilePhotoController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ExpertProfilePhotoController(
            UserManager<ApplicationUser> userManager           
            )
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            var photoEditVM = new ExpertShortInfoVM(user);
            return View(photoEditVM);
        }
    }
}