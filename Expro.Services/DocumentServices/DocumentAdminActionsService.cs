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
    public class DocumentAdminActionsService : BaseApprovableByAdminService<Document>, IDocumentAdminActionsService
    {
        public DocumentAdminActionsService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}