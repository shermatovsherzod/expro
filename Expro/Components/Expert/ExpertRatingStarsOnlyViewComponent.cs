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
    public class ExpertRatingStarsOnlyViewComponent : ViewComponent
    {
        IFeedbackService _feedbackService;      
        public ExpertRatingStarsOnlyViewComponent(IFeedbackService feedbackService)
        {            
            _feedbackService = feedbackService;
        }

        public IViewComponentResult Invoke(string id)
        {           
            double overallRating = _feedbackService.GetOverallRatingByExpert(id);
            ExpertRatingStarsTypeVM vmodel = ExpertRatingStarsTypeVM.GetExpertRatingStarsTypeVM(overallRating);
            return View("ExpertRatingStarsOnly", vmodel);
        }

       
    }
}
