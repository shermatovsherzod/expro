using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Expro.Data.Repository
{
    public class QuestionRepository : BaseCRUDRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public IQueryable<Question> GetManyWithRelatedDataAsIQueryable()
        {
            return DbSet
                .Include(m => m.QuestionLawAreas).ThenInclude(m => m.LawArea)
                .Include(m => m.Creator)
                    .ThenInclude(m => m.Avatar)
                .Include(m => m.DocumentStatus);
        }

        public Question GeWithRelatedDataByID(int id)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Include(m => m.Attachment)
                .FirstOrDefault(m => m.ID == id);
        }

        public Question GeWithAnswersAndCommentsByID(int id)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Include(m => m.Answers)
                    .ThenInclude(m => m.Creator)
                .Include(m => m.Answers)
                    .ThenInclude(m => m.QuestionAnswerLikes)
                        .ThenInclude(m => m.Like)
                .Include(m => m.Answers)
                    .ThenInclude(m => m.Attachment)
                .Include(m => m.Answers)
                    .ThenInclude(m => m.Comments)
                        .ThenInclude(m => m.Comment)
                            .ThenInclude(m => m.Creator)
                .Include(m => m.Answers)
                    .ThenInclude(m => m.Comments)
                        .ThenInclude(m => m.Comment)
                            .ThenInclude(m => m.Attachment)
                .Include(m => m.Attachment)
                .FirstOrDefault(m => m.ID == id);
        }
    }
}