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

            double overallRating = _feedbackService.GetOverallRatingByExpert(id);

            vmodel.OverallRating = overallRating.ToString("0.00");
            vmodel.FiveStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 5);
            vmodel.FourStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 4);
            vmodel.ThreeStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 3);
            vmodel.TwoStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 2);
            vmodel.OneStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 1);

            vmodel.OverallRatingStarsType = GetOverallRatingStarsType(overallRating);         

            return View("ExpertRating", vmodel);

        }

        public ExpertRatingStarsTypeVM GetOverallRatingStarsType(double overallRating)
        {
            ExpertRatingStarsTypeVM result = new ExpertRatingStarsTypeVM();
            if (overallRating == 0)
            {
                result.StarMuted = 5;
            }

            if (0 < overallRating && overallRating < 1)
            {
                result.StarHalf = 1;
                result.StarMuted = 4;
            }

            if (overallRating == 1)
            {
                result.Star = 1;
                result.StarMuted = 4;
            }

            if (1 < overallRating && overallRating < 2)
            {
                result.Star = 1;
                result.StarHalf = 1;
                result.StarMuted = 3;
            }

            if (overallRating == 2)
            {
                result.Star = 2;
                result.StarMuted = 3;
            }

            if (2 < overallRating && overallRating < 3)
            {
                result.Star = 2;
                result.StarHalf = 1;
                result.StarMuted = 2;
            }

            if (overallRating == 3)
            {
                result.Star = 3;
                result.StarMuted = 2;
            }

            if (3 < overallRating && overallRating < 4)
            {
                result.Star = 3;
                result.StarHalf = 1;
                result.StarMuted = 1;
            }

            if (overallRating == 4)
            {
                result.Star = 4;
                result.StarMuted = 1;
            }

            if (4 < overallRating && overallRating < 5)
            {
                result.Star = 4;
                result.StarHalf = 1;
            }

            if (overallRating == 5)
            {
                result.Star = 5;
            }
            return result;
        }
    }
}
