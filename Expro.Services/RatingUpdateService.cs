using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Services
{
    public class RatingUpdateService : BaseCRUDService<RatingUpdate>, IRatingUpdateService
    {
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;
        private readonly IQuestionAnswerService _questionAnswerService;

        public RatingUpdateService(IRatingUpdateRepository repository,
                           IUnitOfWork unitOfWork,
                           IUserService userService,
                           IDocumentService documentService,
                           IQuestionAnswerService questionAnswerService)
            : base(repository, unitOfWork)
        {
            _userService = userService;
            _documentService = documentService;
            _questionAnswerService = questionAnswerService;
        }

        public void UpdateRatingForAllExperts()
        {
            DateTime now = DateTime.Now;
            DateTime endDate = now.Date;
            DateTime startDate = endDate.AddDays(-30);

            var experts = _userService.GetAsIQueryable().ToList(); // must be _userService.GetAllExperts()
            if (experts.Count == 0)
                throw new Exception("В базе нет экспертов");

            foreach (var expert in experts)
            {
                int actionsCount = CalculateActionsCountByUserAndPeriod(expert, startDate, endDate);
                expert.Rating = CalculateRating(actionsCount, expert.Points);

                expert.DateRatingLastUpdated = now;
            }

            _userService.UpdateCollection(experts);
        }

        private int CalculateActionsCountByUserAndPeriod(
            ApplicationUser user, 
            DateTime startDate, DateTime endDate)
        {
            int approvedDocumentsCount = _documentService
                .GetAllApprovedByUserAndPeriod(user.Id, startDate, endDate)
                .Count();

            int questionAnswersCount = _questionAnswerService
                .GetAllByAnswerer(user.Id)
                .Count();

            return approvedDocumentsCount + questionAnswersCount;
        }

        private int CalculateRating(int actionsCount, int points)
        {
            int ratio = 0;

            if (actionsCount >= 10 && actionsCount <= 24)
                ratio = 6;
            else if (actionsCount >= 25 && actionsCount <= 50)
                ratio = 8;
            //else if ...

            return points * ratio;
        }
    }
}