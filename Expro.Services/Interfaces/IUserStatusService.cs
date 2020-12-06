using Expro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Expro.Services.Interfaces
{
    public interface IUserStatusService
    {
        string GetUserStatusText(string userID);
        Task<bool> Approve(string userID);
        Task<bool> Reject(string userID);
        Task<bool> ProfileConfirmationRequestByExpert(string userID);
    }
}
