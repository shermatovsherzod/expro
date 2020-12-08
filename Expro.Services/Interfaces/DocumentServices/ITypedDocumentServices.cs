using Expro.Models;
using Expro.Models.Enums;
using System.Linq;

namespace Expro.Services.Interfaces
{
    public interface ISampleDocumentService : IDocumentService
    {
    }

    public interface IArticleDocumentService : IDocumentService
    {
    }

    public interface IPracticeDocumentService : IDocumentService
    {
    }

    public interface IQuestionDocumentService : IDocumentService
    {
        void Complete(Document question, string userID);
        bool AdminIsAllowedToComplete(Document question);
        bool IsCompleted(Document question);
    }
}