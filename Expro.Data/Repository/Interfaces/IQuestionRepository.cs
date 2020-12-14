using Expro.Models;
using System.Linq;

namespace Expro.Data.Repository.Interfaces
{
    public interface IQuestionRepository : IBaseCRUDRepository<Question>
    {
        IQueryable<Question> GetManyWithRelatedDataAsIQueryable();
        Question GeWithRelatedDataByID(int id);
        Question GeWithAnswersAndCommentsByID(int id);
    }
}