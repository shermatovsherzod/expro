using Expro.Models;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IResumeService : IBaseAuthorableService<Resume>
    {
        IQueryable<Resume> GetResumeByCreatorID(string userID);
        IQueryable<Resume> GetAllForAdmin();
        IQueryable<Resume> GetAllApproved();
        bool ResumeBelongsToUser(Resume model, string userID);
        bool ResumeBelongsToUser(int modelID, string userID);
        void SubmitForApproval(Resume entity, string userID);
    }
}