﻿using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;

namespace Expro.Data.Repository
{
    public class WithdrawRequestStatusRepository : BaseDropdownableRepository<WithdrawRequestStatus>, IWithdrawRequestStatusRepository
    {
        public WithdrawRequestStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}