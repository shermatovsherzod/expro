using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Expro.Services
{
    public class QuestionAnswerService : BaseAuthorableService<QuestionAnswer>, IQuestionAnswerService
    {
        //private readonly IQuestionAnswerLikeRepository _likeRepository;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public QuestionAnswerService(IQuestionAnswerRepository repository,
                           IUnitOfWork unitOfWork,
                           //IQuestionAnswerLikeRepository likeRepository,
                           IQuestionAnswerRepository questionAnswerRepository,
                           IQuestionService questionService,
                           IUserService userService,
                           IEmailService emailService)
            : base(repository, unitOfWork)
        {
            //_likeRepository = likeRepository;
            _questionAnswerRepository = questionAnswerRepository;
            _questionService = questionService;
            _userService = userService;
            _emailService = emailService;
        }

        public async override void Add(QuestionAnswer entity, string creatorID)
        {
            base.Add(entity, creatorID);

            try
            {
                List<Tuple<string, string>> emailsWithNames = new List<Tuple<string, string>>();
                var question = _questionService.GetByID(entity.QuestionID);
                var creator = _userService.GetByID(question.CreatedBy);
                emailsWithNames.Add(new Tuple<string, string>(creator.Email, creator.FirstName + " " + creator.LastName));

                string subjectUz = "Саволга янги жавоб берилди";
                string subjectRu = "Получен новый ответ на вопрос";

                string questionUrl = "/Question/Details/" + entity.ID;
                var answerGiver = _userService.GetByID(creatorID);
                string answerGiverFullName = answerGiver.FirstName + " " + answerGiver.LastName;
                string messageUz = "Сизнинг саволингизга (<a href='" + questionUrl + "'>\"" + question.Title + "\"</a>) " + answerGiverFullName + " жавоб берди.";
                string messageRu = answerGiverFullName + " ответил на Ваш вопрос (<a href='" + questionUrl + "'>\"" + question.Title + "\"</a>)";

                await _emailService.SendEmailAsync(
                    emailsWithNames,
                    subjectUz, subjectRu,
                    messageUz, messageRu);
            }
            catch (Exception ex) { }
        }

        public bool DistributionIsCorrect(List<int> percentages)
        {
            return percentages.Sum() == 100;
        }

        public int CalculatePaidFee(int questionFee, int percentage)
        {
            return questionFee * percentage / 100;
        }

        //public void AddLike(QuestionAnswer questionAnswer, string userID, bool isPositive)
        //{
        //    var existingLike = questionAnswer.Likes.AsQueryable().FirstOrDefault(m => m.UserID == userID);

        //    if (existingLike == null)
        //    {
        //        questionAnswer.Likes.Add(new QuestionAnswerLike()
        //        {
        //            UserID = userID,
        //            IsPositive = isPositive
        //        });
        //    }
        //    else
        //    {
        //        if (existingLike.IsPositive != isPositive)
        //            existingLike.IsPositive = isPositive;
        //        else
        //            _likeRepository.Delete(existingLike);
        //    }
                

        //    Update(questionAnswer);
        //}

        public IQueryable<QuestionAnswer> GetManyPaidByAnswerer(string answererID)
        {
            return _questionAnswerRepository
                .GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.CreatedBy == answererID
                    && m.PaidFee.HasValue);
        }

        public IQueryable<QuestionAnswer> GetAllByAnswerer(string answererID)
        {
            return GetAsIQueryable().Where(m => m.CreatedBy == answererID);
        }
    }
}