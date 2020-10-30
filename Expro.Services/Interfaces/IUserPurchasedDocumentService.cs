using Expro.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Expro.Services.Interfaces
{
    public interface IUserPurchasedDocumentService : IBaseCRUDService<UserPurchasedDocument>
    {
        void Purchase(ApplicationUser user, Document document);
        bool UserPurchasedDocumentBefore(ApplicationUser user, Document document);
    }
}