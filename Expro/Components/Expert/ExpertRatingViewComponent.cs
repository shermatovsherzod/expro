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

            double _allStarsCount = _feedbackService.GetAllStarsCountByExpert(id);

            vmodel.OverallRating = overallRating.ToString("0.00");

            int _fiveStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 5);
            vmodel.FiveStarsSum = _fiveStarsSum;
            vmodel.FiveStarsProgressBarPercent = GetProgressBarPercent(_fiveStarsSum, _allStarsCount);

            int _fourStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 4);
            vmodel.FourStarsSum = _fourStarsSum;
            vmodel.FourStarsProgressBarPercent = GetProgressBarPercent(_fourStarsSum, _allStarsCount);

            int _threeStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 3);
            vmodel.ThreeStarsSum = _threeStarsSum;
            vmodel.ThreeStarsProgressBarPercent = GetProgressBarPercent(_threeStarsSum, _allStarsCount);

            int _twoStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 2);
            vmodel.TwoStarsSum = _twoStarsSum;
            vmodel.TwoStarsProgressBarPercent = GetProgressBarPercent(_twoStarsSum, _allStarsCount);

            int _oneStarsSum = _feedbackService.GetRatingStarsCountByExpert(id, 1);
            vmodel.OneStarsSum = _oneStarsSum;
            vmodel.OneStarsProgressBarPercent = GetProgressBarPercent(_oneStarsSum, _allStarsCount);

            vmodel.OverallRatingStarsType = GetOverallRatingStarsType(overallRating);

            return View("ExpertRating", vmodel);

        }

        public string GetProgressBarPercent(int starsValue, double allstarsCount)
        {
            if (allstarsCount != 0)
            {
                double result = ((double)starsValue / allstarsCount) * 100;

                int rounded = (int)Math.Round(result);

                return rounded.ToString();
            }
            return "0";
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
