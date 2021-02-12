using System;
using System.Collections.Generic;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Expro.Controllers
{
    public class FeedbackController : BaseFeedbackController
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(
              IFeedbackSearchService feedbackSearchService,
            IFeedbackService feedbackService,
            IFeedbackAdminActionsService feedbackAdminActionsService
              ) : base(
                  feedbackSearchService,
                  feedbackService,
                  feedbackAdminActionsService)
        {
            _feedbackService = feedbackService;
        }

        public override IActionResult Index(string id)
        {
            ViewBag.feedbackToUserID = id;
            var currentUserAccount = accountUtil.GetCurrentUser(User);
            ViewBag.FeedbackExist = _feedbackService.FeedbackExist(id, currentUserAccount.ID);

            return base.Index(id);
        }

        [HttpPost]
        public override IActionResult Search(int draw, int? start = null, int? length = null, int? statusID = null, string feedbackToUser = "")
        {
            return base.Search(draw, start, length, statusID, feedbackToUser);
        }

    }
}