using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Expro.Services
{
    public class QuestionAnswerService : BaseAuthorableService<QuestionAnswer>, IQuestionAnswerService
    {
        //private readonly IQuestionAnswerLikeRepository _likeRepository;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;

        public QuestionAnswerService(IQuestionAnswerRepository repository,
                           IUnitOfWork unitOfWork,
                           //IQuestionAnswerLikeRepository likeRepository,
                           IQuestionAnswerRepository questionAnswerRepository)
            : base(repository, unitOfWork)
        {
            //_likeRepository = likeRepository;
            _questionAnswerRepository = questionAnswerRepository;
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