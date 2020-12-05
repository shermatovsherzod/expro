using Expro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface IUserStatusService
    {
        UserStatus GetUserStatus(ApplicationUser user);
        Task<bool> Approve(string userID);
        Task<bool> Reject(string userID);
        Task<bool> ProfileConfirmationRequestByExpert(string userID);
    }
}
