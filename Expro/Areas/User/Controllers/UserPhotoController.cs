using System.Linq;
using Expro.Controllers;
using Expro.Models;
using Expro.ViewModels.Expert;
using Expro.ViewModels.SimpleUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.User.Controllers
{
    [Area("User")]
    public class UserPhotoController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserPhotoController(
            UserManager<ApplicationUser> userManager
            )
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            var user = _userManager.Users.FirstOrDefault(c => c.UserName == currentUserAccount.UserName);
            var photoEditVM = new SimpleUserShortInfoVM(user);
            return View(photoEditVM);
        }
    }
}