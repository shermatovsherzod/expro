using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Controllers;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using Expro.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Expro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RatingController : BaseController
    {
        protected readonly IDocumentService DocumentService;
        private readonly IRatingUpdateService _ratingUpdateService;
        private readonly IDocumentSearchService DocumentSearchService;
        protected readonly IDocumentAdminActionsService DocumentAdminActionsService;
        protected readonly IHangfireService HangfireService;
        private readonly IUserService _userService;
        private readonly IUserRatingService _userRatingService;

        public RatingController(
            IDocumentService documentService,
            IRatingUpdateService ratingUpdateService,
            IDocumentSearchService documentSearchService,
            IDocumentAdminActionsService documentAdminActionsService,
            IHangfireService hangfireService,
            IUserService userService,
            IUserRatingService userRatingService)
        {
            DocumentService = documentService;
            _ratingUpdateService = ratingUpdateService;
            DocumentSearchService = documentSearchService;
            DocumentAdminActionsService = documentAdminActionsService;
            HangfireService = hangfireService;
            _userService = userService;
            _userRatingService = userRatingService;
        }

        [HttpPost]
        public IActionResult UpdateRatings()
        {
            try
            {
                _ratingUpdateService.UpdateRatingForAllExperts();

                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}
