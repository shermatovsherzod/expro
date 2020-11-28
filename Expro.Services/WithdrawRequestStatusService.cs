using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class WithdrawRequestStatusService : BaseDropdownableService<WithdrawRequestStatus>, IWithdrawRequestStatusService
    {
        public WithdrawRequestStatusService(IWithdrawRequestStatusRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}