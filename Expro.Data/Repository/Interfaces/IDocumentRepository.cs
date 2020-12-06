using Expro.Models;
using System.Linq;

namespace Expro.Data.Repository.Interfaces
{
    public interface IDocumentRepository : IBaseCRUDRepository<Document>
    {
        IQueryable<Document> GetManyWithRelatedDataAsIQueryable();
        Document GeWithRelatedDataByID(int id);
        Document GeWithAnswersAndCommentsByID(int id);
    }
}