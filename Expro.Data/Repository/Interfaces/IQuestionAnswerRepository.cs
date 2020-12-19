using Expro.Models;
using System.Linq;

namespace Expro.Data.Repository.Interfaces
{
    public interface IQuestionAnswerRepository : IBaseCRUDRepository<QuestionAnswer>
    {
        IQueryable<QuestionAnswer> GetManyWithRelatedDataAsIQueryable();
    }
}