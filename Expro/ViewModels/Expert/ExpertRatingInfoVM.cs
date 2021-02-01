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
    }
}
