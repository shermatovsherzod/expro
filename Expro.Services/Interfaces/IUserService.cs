using Expro.Models;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IUserService : IBaseUserService<ApplicationUser>
    {
        bool UserIsAllowedToWorkWithPaidMaterials(ApplicationUser user);
        IQueryable<ApplicationUser> GetManyWithRelatedDataAsIQueryable();
        IQueryable<ApplicationUser> GetAllForAdmin();

        IQueryable<ApplicationUser> GetAllExpertsForAdmin();

        IQueryable<ApplicationUser> GetAllApprovedExperts();
        IQueryable<ApplicationUser> GetAllExpertsAndSimpleUsers();        
        ApplicationUser GetWithRelatedDataByID(string id);
        IQueryable<ApplicationUser> GetTopExperts(int count);
        List<string> GetAdminEmails();
    }
}