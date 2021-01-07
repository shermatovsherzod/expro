using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{
    public class FeedbackAdminActionsService : IFeedbackAdminActionsService
    {
        protected IFeedbackService _feedbackService;

        public FeedbackAdminActionsService() { }

        public FeedbackAdminActionsService(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public void Approve(Feedback entity, string userID)
        {
            entity.FeedbackStatusID = (int)FeedbackStatusEnum.Approved;
            entity.DateApproved = DateTime.Now;

            _feedbackService.Update(entity, userID);
        }

        public void Reject(Feedback entity, string userID)
        {
            entity.FeedbackStatusID = (int)FeedbackStatusEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            _feedbackService.Update(entity, userID);
        }
    }
}