using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.ViewModels
{
    public class ExpertRatingInfoVM
    {
        public string OverallRating { get; set; }
        public int FiveStarsSum { get; set; }
        public string FiveStarsProgressBarPercent { get; set; }
        public int FourStarsSum { get; set; }
        public string FourStarsProgressBarPercent { get; set; }
        public int ThreeStarsSum { get; set; }
        public string ThreeStarsProgressBarPercent { get; set; }
        public int TwoStarsSum { get; set; }
        public string TwoStarsProgressBarPercent { get; set; }
        public int OneStarsSum { get; set; }
        public string OneStarsProgressBarPercent { get; set; }
        public double OverallStars { get; set; }

        public ExpertRatingStarsTypeVM OverallRatingStarsType { get; set; }
    }

    public class ExpertRatingStarsTypeVM
    {
        public ExpertRatingStarsTypeVM()
        {
            Star = 0;
            StarHalf = 0;
            StarMuted = 0;
        }
        public int Star { get; set; }
        public int StarHalf { get; set; }
        public int StarMuted { get; set; }


        public static ExpertRatingStarsTypeVM GetExpertRatingStarsTypeVM(double overallRating)
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
