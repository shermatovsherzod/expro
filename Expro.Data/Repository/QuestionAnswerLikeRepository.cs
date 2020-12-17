using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class QuestionAnswerLikeRepository : BaseCRUDRepository<QuestionAnswerLike>, IQuestionAnswerLikeRepository
    {
        public QuestionAnswerLikeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}