using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers.Experts
{
    [Area("Admin")]
    [Authorize(Policy = "AdminOnly")]
    public class ExpertFeedbacksController : BaseFeedbackController
    {
        public ExpertFeedbacksController(
              IFeedbackSearchService feedbackSearchService,
              IFeedbackService feedbackService,
              IFeedbackAdminActionsService feedbackAdminActionsService
              ) : base(
                  feedbackSearchService,
                  feedbackService,
                  feedbackAdminActionsService)
        {

        }

        public override IActionResult Index(string id)
        {
            ViewBag.feedbackToUserID = id;
            return base.Index(id);
        }

        [HttpPost]
        public override IActionResult Search(int draw, int? start = null, int? length = null, int? statusID = null, string feedbackToUser = "")
        {
            return base.Search(draw, start, length, statusID, feedbackToUser);
        }

        [HttpPost]
        public override IActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}