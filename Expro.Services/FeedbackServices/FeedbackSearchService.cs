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
    public class FeedbackSearchService : IFeedbackSearchService
    {
        protected IFeedbackService _feedbackService;

        public FeedbackSearchService() { }

        public FeedbackSearchService(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public virtual IQueryable<Feedback> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum? curUserType,
            int? statusID,          
            string authorID,
            string feedbackToUser
            )
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<Feedback> feedbacks;

                if (curUserType.HasValue)
                {
                    if (curUserType == UserTypesEnum.Admin)
                        feedbacks = _feedbackService.GetAllForAdmin(feedbackToUser);                                   
                    else
                        feedbacks = _feedbackService.GetAllApproved(feedbackToUser);
                }
                else
                    feedbacks = _feedbackService.GetAllApproved(feedbackToUser);

                recordsTotal = feedbacks.Count();

                feedbacks = ApplyFilters(feedbacks, statusID);

                recordsFiltered = feedbacks.Count();

                feedbacks = ApplyOrder(feedbacks, start, length);

                return feedbacks;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<Feedback>().AsQueryable();
            }
        }

        protected IQueryable<Feedback> ApplyFilters(
            IQueryable<Feedback> feedbacks,           
            int? statusID
           )
        {
            if (statusID.HasValue)
            {
                if (statusID.Value == (int)FeedbackStatusEnum.Approved)
                {
                    feedbacks = feedbacks
                        .Where(m => m.FeedbackStatusID == (int)FeedbackStatusEnum.Approved);
                            
                }
                else if (statusID.Value == (int)FeedbackStatusEnum.Rejected)
                {
                    feedbacks = feedbacks
                        .Where(m => m.FeedbackStatusID == (int)FeedbackStatusEnum.Rejected);

                }
                else if (statusID.Value == (int)FeedbackStatusEnum.NotApproved)
                {
                    feedbacks = feedbacks
                        .Where(m => m.FeedbackStatusID == (int)FeedbackStatusEnum.NotApproved);

                }
                else if (statusID.Value == (int)FeedbackStatusEnum.WaitingForApproval)
                {
                    feedbacks = feedbacks
                        .Where(m => m.FeedbackStatusID == (int)FeedbackStatusEnum.WaitingForApproval);
                }
            }          

            return feedbacks;
        }

        protected IQueryable<Feedback> ApplyOrder(
            IQueryable<Feedback> feedbacks,
            int? start,
            int? length)
        {
            feedbacks = feedbacks.OrderByDescending(m => m.DateModified);
            if (start.HasValue && start.Value > 0)
                feedbacks = feedbacks.Skip(start.Value);
            if (length.HasValue && length.Value > 0)
                feedbacks = feedbacks.Take(length.Value);

            return feedbacks;
        }
    }
}