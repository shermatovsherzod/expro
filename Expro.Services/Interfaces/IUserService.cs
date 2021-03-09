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
        IQueryable<ApplicationUser> GetAllSimpleUsersForAdmin();

        IQueryable<ApplicationUser> GetAllApprovedExperts();
        IQueryable<ApplicationUser> GetAllExpertsForSite();
        IQueryable<ApplicationUser> GetAllExpertsAndSimpleUsers();        
        ApplicationUser GetWithRelatedDataByID(string id);
        IQueryable<ApplicationUser> GetTopExperts(int count);
        List<string> GetAdminEmails();

        void Block(ApplicationUser user);
        void Unblock(ApplicationUser user);
        bool IsBlocked(ApplicationUser user);
    }
}