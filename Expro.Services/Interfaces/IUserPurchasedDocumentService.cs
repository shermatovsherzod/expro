using Expro.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface IUserPurchasedDocumentService : IBaseCRUDService<UserPurchasedDocument>
    {
        void Purchase(ApplicationUser user, Document document);
        bool UserPurchasedDocumentBefore(ApplicationUser user, Document document);
        IQueryable<UserPurchasedDocument> GetPurchasesByUser(string userID);
        IQueryable<UserPurchasedDocument> GetSalesByUser(string userID);
    }
}