using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Models.Enums;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class DocumentService : BaseAuthorableService<Document>, IDocumentService
    {
        public DocumentService(IDocumentRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public void AddSampleDocument(Document entity, string creatorID)
        {
            entity.DocumentTypeID = (int)DocumentTypesEnum.SampleDocument;
            entity.DocumentStatusID = (int)DocumentStatusesEnum.Pending;
            entity.ContentType = DocumentContentTypesEnum.text;

            Add(entity, creatorID);
        }

        bool StatusIsPending(Document entity)
        {
            if (entity == null)
                return false;

            return entity.DocumentStatusID == (int)DocumentStatusesEnum.Pending;
        }

        public bool EditingIsAllowed(Document entity)
        {
            return StatusIsPending(entity);
        }

        public bool BelongsToUser(Document entity, string userID)
        {
            if (entity == null)
                return false;

            return entity.CreatedBy == userID;
        }

        public bool AttachedFileIsAllowedToBeDeleted(Document entity)
        {
            return EditingIsAllowed(entity);
        }
    }
}