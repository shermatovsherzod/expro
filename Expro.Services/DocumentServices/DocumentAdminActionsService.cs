using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Expro.Services
{

    //**********************************************************************
    //AdminActionsClass
    //public class DocumentAdminActionsService : 
    public class DocumentAdminActionsService : IDocumentAdminActionsService
    {
        protected IDocumentService DocumentService;

        public DocumentAdminActionsService() { }

        public DocumentAdminActionsService(IDocumentService documentService)
        {
            DocumentService = documentService;
        }

        bool StatusIsWaitingForApproval(Document entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.WaitingForApproval;
        }

        public bool ApprovingIsAllowed(Document entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public virtual void Approve(Document entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Approved;
            entity.DateApproved = DateTime.Now;

            DocumentService.Update(entity, userID);
        }

        public bool RejectingIsAllowed(Document entity)
        {
            return StatusIsWaitingForApproval(entity);
        }

        public void Reject(Document entity, string userID)
        {
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Rejected;
            entity.DateRejected = DateTime.Now;

            DocumentService.Update(entity, userID);
        }

        public void RejectionDeadlineReaches(Document model)
        {
            if (!RejectingIsAllowed(model))
                return;

            Reject(model, "634a8718-167d-4b77-98bb-7548340e95b2"); //add botUser
        }
    }
}