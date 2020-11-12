using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.IO;
using System.Linq;
using System;

namespace Expro.Services
{
    public class UserPurchasedDocumentService : BaseCRUDService<UserPurchasedDocument>, IUserPurchasedDocumentService
    {
        private readonly IUserBalanceService UserBalanceService;

        public UserPurchasedDocumentService(IUserPurchasedDocumentRepository repository,
                           IUnitOfWork unitOfWork,
                           IUserBalanceService userBalanceService)
            : base(repository, unitOfWork)
        {
            UserBalanceService = userBalanceService;
        }

        public void Purchase(ApplicationUser user, Document document)
        {
            if (UserPurchasedDocumentBefore(user, document))
                throw new Exception("Вы уже покупали этот документ");

            var model = new UserPurchasedDocument()
            {
                User = user,
                Document = document,
                Price = document.Price ?? 0,
                DatePurchased = DateTime.Now
            };

            UserBalanceService.TakeOffBalance(user, model.Price);
            UserBalanceService.ReplenishBalance(document.Creator, model.Price);

            Add(model);
        }

        public bool UserPurchasedDocumentBefore(ApplicationUser user, Document document)
        {
            return GetAsIQueryable()
                .FirstOrDefault(m => m.DocumentID == document.ID && m.UserID == user.Id) != null;
        }
    }
}