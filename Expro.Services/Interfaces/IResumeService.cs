using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IResumeService : IBaseAuthorableService<Resume>
    {
        IQueryable<Resume> GetResumeByCreatorID(string userID);
        bool ResumeBelongsToUser(Resume model, string userID);
        bool ResumeBelongsToUser(int modelID, string userID);
    }
}