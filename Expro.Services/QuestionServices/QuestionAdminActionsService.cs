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
    public class QuestionAdminActionsService : IQuestionAdminActionsService
    {
        protected IQuestionService QuestionService;

        public QuestionAdminActionsService() { }

        public QuestionAdminActionsService(IQuestionService questionService)
        {
            QuestionService = questionService;
        }

        bool StatusIsWaitingForApproval(Question entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.WaitingForApproval;
        }

        public bool ApprovingIsAllowed(Question entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public void Approve(Question entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Approved;
            entity.DateApproved = DateTime.Now;
#if DEBUG
            entity.QuestionCompletionDeadline = entity.DateApproved.Value.AddMinutes(20);
#else
            entity.QuestionCompletionDeadline = QuestionService.RoundToUp(entity.DateApproved.Value.AddMinutes(7200)); //5 days
#endif
            QuestionService.Update(entity, userID);
        }

        public bool RejectingIsAllowed(Question entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public void Reject(Question entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            QuestionService.Update(entity, userID);
        }

        public void RejectionDeadlineReaches(Question model)
        {
            if (!RejectingIsAllowed(model))
                return;

            Reject(model, "634a8718-167d-4b77-98bb-7548340e95b2"); //add botUser
        }

        public void CompletionDeadlineReaches(Question Question)
        {
            if (QuestionService.IsCompleted(Question))
                return;

            QuestionService.Complete(Question, "634a8718-167d-4b77-98bb-7548340e95b2"); //add botUser
        }
    }
}