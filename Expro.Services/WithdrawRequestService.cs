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
    public class WithdrawRequestService : BaseAuthorableService<WithdrawRequest>, IWithdrawRequestService
    {
        private IWithdrawRequestRepository _withdrawRequestRepository;
        private readonly IUserBalanceService UserBalanceService;
        //protected int _tmpPeriodMinutes = 5;

        private readonly int MinimalAmountInBalanceForWithdrawal = 100000;

        //public DocumentTypesEnum _documentType;

        public WithdrawRequestService(IWithdrawRequestRepository repository,
                           IUnitOfWork unitOfWork,
                           IUserBalanceService userBalanceService)
            : base(repository, unitOfWork)
        {
            _withdrawRequestRepository = repository;
            UserBalanceService = userBalanceService;
        }

        public void Add(WithdrawRequest entity, ApplicationUser user /*string creatorID*/)
        {
            entity.StatusID = (int)WithdrawRequestStatusesEnum.Pending;
            UserBalanceService.TakeOffBalance(user, entity.Amount);

            base.Add(entity, user.Id /*creatorID*/);
        }

        public IQueryable<WithdrawRequest> GetManyWithRelatedDataAsIQueryable()
        {
            return _withdrawRequestRepository.GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<WithdrawRequest> GetAllForAdmin()
        {
            return GetManyWithRelatedDataAsIQueryable();
        }

        public IQueryable<WithdrawRequest> GetAllByCreator(string userID)
        {
            return GetManyWithRelatedDataAsIQueryable()
                .Where(m => m.CreatedBy == userID);
        }

        public IQueryable<WithdrawRequest> Search(
            int? start,
            int? length,

            out int recordsTotal,
            out int recordsFiltered,
            out string error,

            UserTypesEnum curUserType,
            UserTypesEnum? filteredUserType,
            int? statusID,
            string authorID)
        {
            recordsTotal = 0;
            recordsFiltered = 0;
            error = "";

            try
            {
                IQueryable<WithdrawRequest> requests;

                if (curUserType == UserTypesEnum.Admin)
                    requests = GetAllForAdmin();
                else 
                    requests = GetAllByCreator(authorID);

                recordsTotal = requests.Count();

                if (statusID.HasValue)
                    requests = requests.Where(m => m.StatusID == statusID.Value);

                if (filteredUserType.HasValue)
                    requests = requests.Where(m => m.Creator.UserType == filteredUserType.Value);

                recordsFiltered = requests.Count();

                requests = requests.OrderBy(m => m.StatusID).ThenBy(m => m.DateModified);
                if (start.HasValue && start.Value > 0)
                    requests = requests.Skip(start.Value);
                if (length.HasValue && length.Value > 0)
                    requests = requests.Take(length.Value);

                //return documents.OrderByDescending(m => m.DateModified);
                return requests;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                if (ex.InnerException != null)
                    error += ". Inner exception: " + ex.InnerException.Message;

                return Enumerable.Empty<WithdrawRequest>().AsQueryable();
            }
        }

        public bool UserHasNotEnoughtMoneyForWithdrawal(int balance)
        {
            return balance < MinimalAmountInBalanceForWithdrawal;
        }

        public bool AmountIsLessThanMinimum(int amount)
        {
            return amount < MinimalAmountInBalanceForWithdrawal;
        }

        public int GetMinimalAmountInBalanceForWithdrawal()
        {
            return MinimalAmountInBalanceForWithdrawal;
        }

        public bool IsCompleted(WithdrawRequest model)
        {
            return model.StatusID == (int)WithdrawRequestStatusesEnum.Completed;
        }

        public void MarkAsCompleted(WithdrawRequest model, string adminID)
        {
            model.StatusID = (int)WithdrawRequestStatusesEnum.Completed;
            model.DateCompleted = DateTime.Now;

            Update(model, adminID);
        }
    }
}