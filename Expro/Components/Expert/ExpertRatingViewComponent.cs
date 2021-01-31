using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class ExpertRatingViewComponent : ViewComponent
    {
        IFeedbackService _feedbackService;
        UserManager<ApplicationUser> _userManager;
        public ExpertRatingViewComponent(UserManager<ApplicationUser> userManager, IFeedbackService feedbackService)
        {
            _userManager = userManager;
            _feedbackService = feedbackService;
        }

        public IViewComponentResult Invoke(string id)
        {
            ExpertRatingInfoVM vmodel = new ExpertRatingInfoVM();

            vmodel.OverallRating = Math.Round(_feedbackService.GetOverallRatingByExpert(id), 2).ToString();
            vmodel.FiveStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 5);
            vmodel.FourStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 4);
            vmodel.ThreeStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 3);
            vmodel.TwoStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 2);
            vmodel.OneStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 1);

            return View("ExpertRating", vmodel);

        }
    }
}
