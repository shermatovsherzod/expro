//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Expro.Data.Repository.Interfaces;
//using Expro.Models;
//using Expro.Models.Enums;
//using Infrastructure = Expro.Data.Infrastructure;

//namespace Expro.Services
//{
//    public class BaseApprovableByAdminService<T> : BaseAuthorableService<T> 
//        where T : BaseModelApprovableByAdmin
//    {
//        public BaseApprovableByAdminService(IBaseCRUDRepository<T> repository, Infrastructure.IUnitOfWork unitOfWork)
//            : base(repository, unitOfWork)
//        {
//            //this._repository = repository;
//            //this._unitOfWork = unitOfWork;
//        }

//        bool StatusIsWaitingForApproval(T entity)
//        {
//            if (entity == null)
//                return false;

//            return entity.DocumentStatusID == (int)DocumentStatusesEnum.WaitingForApproval;
//        }

//        public bool ApprovingIsAllowed(T entity)
//        {
//            return StatusIsWaitingForApproval(entity);
//        }

//        public void Approve(Question entity, string userID)
//        {
//            entity.DocumentStatusID = (int)DocumentStatusesEnum.Approved;
//            entity.DateApproved = DateTime.Now;
//#if DEBUG
//            entity.QuestionCompletionDeadline = entity.DateApproved.Value.AddMinutes(20);
//#else
//            entity.QuestionCompletionDeadline = QuestionService.RoundToUp(entity.DateApproved.Value.AddMinutes(7200)); //5 days
//#endif
//            QuestionService.Update(entity, userID);
//        }

//        public bool RejectingIsAllowed(Question entity)
//        {
//            return StatusIsWaitingForApproval(entity);
//        }

//        public void Reject(Question entity, string userID)
//        {
//            entity.DocumentStatusID = (int)DocumentStatusesEnum.Rejected;
//            entity.DateRejected = DateTime.Now;

//            QuestionService.Update(entity, userID);
//        }

//        public void RejectionDeadlineReaches(Question model)
//        {
//            if (!RejectingIsAllowed(model))
//                return;

//            Reject(model, "634a8718-167d-4b77-98bb-7548340e95b2"); //add botUser
//        }
//    }
//}
