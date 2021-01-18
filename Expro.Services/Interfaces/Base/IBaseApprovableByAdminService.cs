using Expro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Services.Interfaces
{
    public interface IBaseApprovableByAdminService<T> : IBaseAuthorableService<T>
        where T : BaseModelApprovableByAdmin
    {
        bool ApprovingIsAllowed(T entity);
        void Approve(T entity, string userID);
        bool RejectingIsAllowed(T entity);
        void Reject(T entity, string userID);
        void RejectionDeadlineReaches(T Question);
    }
}
