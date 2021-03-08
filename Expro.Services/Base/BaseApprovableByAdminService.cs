using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Infrastructure = Expro.Data.Infrastructure;

namespace Expro.Services
{
    public class BaseApprovableByAdminService<T> : BaseAuthorableService<T>
        where T : BaseModelApprovableByAdmin
    {
        public BaseApprovableByAdminService(IBaseCRUDRepository<T> repository, Infrastructure.IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
            //this._repository = repository;
            //this._unitOfWork = unitOfWork;
        }

        bool StatusIsWaitingForApproval(T entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.WaitingForApproval;
        }

        bool IsApproved(T entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Approved;
        }

        public bool ApprovingIsAllowed(T entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public virtual void Approve(T entity, string userID)
        {
            var now = DateTime.Now;
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Approved;
            entity.DateApproved = now;
            entity.DatePublished = now;

            Update(entity, userID);
        }

        public bool RejectingIsAllowed(T entity)
        {
            return StatusIsWaitingForApproval(entity) || IsApproved(entity);
        }

        public virtual void Reject(T entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            if (string.IsNullOrWhiteSpace(userID))
                base.Update(entity);
            else
                Update(entity, userID);
        }

        public void RejectionDeadlineReaches(T model)
        {
            if (!StatusIsWaitingForApproval(model))
                return;

            Reject(model, null);
        }
    }
}
