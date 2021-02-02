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
    public class QuestionAdminActionsService : BaseApprovableByAdminService<Question>, IQuestionAdminActionsService
    {
        protected IQuestionService _questionService;

        public QuestionAdminActionsService(IQuestionRepository repository,
                           IUnitOfWork unitOfWork,
                           IQuestionService questionService)
            : base(repository, unitOfWork)
        {
            _questionService = questionService;
        }

        public override void Approve(Question entity, string userID)
        {
            base.Approve(entity, userID);
#if DEBUG
            entity.QuestionCompletionDeadline = entity.DateApproved.Value.AddMinutes(20);
#else
            entity.QuestionCompletionDeadline = QuestionService.RoundToUp(entity.DateApproved.Value.AddMinutes(7200)); //5 days
#endif
            Update(entity, userID);
        }

        public void CompletionDeadlineReaches(Question Question)
        {
            if (_questionService.IsCompleted(Question))
                return;

            _questionService.Complete(Question, null);
        }
    }
}