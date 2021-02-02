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
    public class BaseFeedbackController : BaseController
    {
        private readonly IFeedbackSearchService _feedbackSearchService;
        private readonly IFeedbackService _feedbackService;
        private readonly IFeedbackAdminActionsService _feedbackAdminActionsService;
        public BaseFeedbackController(
            IFeedbackSearchService feedbackSearchService,
            IFeedbackService feedbackService,
            IFeedbackAdminActionsService feedbackAdminActionsService
            )
        {
            _feedbackSearchService = feedbackSearchService;
            _feedbackService = feedbackService;
            _feedbackAdminActionsService = feedbackAdminActionsService;
        }

        //public virtual IActionResult Index()
        //{
        //    return View();
        //}

        public virtual IActionResult Index(string feedbackToUserID = "")
        {
            return View();
        }

        [HttpPost]
        public virtual IActionResult Search(
           int draw, int? start = null, int? length = null, int? statusID = null, string feedbackToUser = "")
        {
            int recordsTotal = 0;
            int recordsFiltered = 0;
            string error = "";

            var curUser = accountUtil.GetCurrentUser(User);

            IQueryable<Feedback> dataIQueryable = _feedbackSearchService.Search(
                start,
                length,

                out recordsTotal,
                out recordsFiltered,
                out error,

                curUser.UserType,
                statusID,
                "",
                feedbackToUser
            );

            dynamic data = new FeedbackDetailsVM().GetListOfFeedbackDetailsVM(dataIQueryable);

            return Json(new
            {
                draw = draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data,
                error = error
            });
        }

        public virtual IActionResult Details(int id)
        {
            var feedback = _feedbackService.GetByID(id);
            if (feedback == null)
                throw new Exception("Вакансия не найдена");

            FeedbackDetailsVM feedbackDetailsVM = new FeedbackDetailsVM(feedback);

            return View(feedbackDetailsVM);
        }

        [HttpPost]
        public virtual IActionResult Approve(int id)
        {
            try
            {
                var feedback = _feedbackService.GetByID(id);
                if (feedback == null)
                    throw new Exception("Отзыв не найден");

                var curUser = accountUtil.GetCurrentUser(User);
                _feedbackAdminActionsService.Approve(feedback, curUser.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public virtual IActionResult Reject(int id)
        {
            try
            {
                var feedback = _feedbackService.GetByID(id);
                if (feedback == null)
                    throw new Exception("Отзыв не найден");

                var curUser = accountUtil.GetCurrentUser(User);
                _feedbackAdminActionsService.Reject(feedback, curUser.ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                var feedback = _feedbackService.GetByID(id);
                if (feedback == null)
                    throw new Exception("Отзыв не найден");

                var curUser = accountUtil.GetCurrentUser(User);
                _feedbackAdminActionsService.Delete(feedback);
                return Ok();
            }
            catch (Exception ex)
            {
                return CustomBadRequest(ex);
            }
        }
    }
}