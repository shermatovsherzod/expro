using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Expro.Controllers
{
    public class ExpertsController : BaseExpertsController
    {
        private readonly IExpertStatusService _expertStatusService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public ExpertsController(
             IExpertsListSearchService expertsListSearchService,
             IUserService userService,
             IExpertStatusService expertStatusService,
             IExpertsListAdminActionsService expertsListAdminActionsService,
             IStringLocalizer<Resources.ResourceTexts> localizer,
             IFeedbackService feedbackService,
             ILawAreaService lawAreaService,
             IRegionService regionService
           ) : base(
               expertsListSearchService,
               userService,
               expertsListAdminActionsService,
               localizer,
               lawAreaService,
               regionService)
        {
            _expertStatusService = expertStatusService;
            _feedbackService = feedbackService;
            _userService = userService;
        }

        public override IActionResult Index()
        {

            return base.Index();
        }

        [HttpPost]
        public override IActionResult Search(
            int draw, int? start = null, int? length = null, 
            int? statusID = null,
            int? lawAreaParent = null,
            int[] lawAreas = null,
            int? regionID = null,
            int? cityID = null)
        {
            return base.Search(draw, start, length, statusID, lawAreaParent, lawAreas, regionID, cityID);
        }


        public override IActionResult Details(string id)
        {  
            return base.Details(id);
        }


        [HttpPost]
        public IActionResult Feedback(string toUserId, int rating, string feedbackText)
        {
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            if (currentUserAccount != null && !_feedbackService.FeedbackExist(toUserId, currentUserAccount.ID))
            {
                Feedback model = new Feedback();
                model.Stars = rating;
                model.FeedbackText = feedbackText;
                model.FeedbakToUser = toUserId;
                model.FeedbackStatusID = (int)FeedbackStatusEnum.Approved;

                _feedbackService.Add(model, currentUserAccount.ID);
            }

            return RedirectToAction("Details", new { id = toUserId });
        }
    }
}
