using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class DocumentStatusService : BaseDropdownableService<DocumentStatus>, IDocumentStatusService
    {
        public DocumentStatusService(IDocumentStatusRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }

    public class QuestionStatusService : DocumentStatusService, IQuestionStatusService
    {
        public QuestionStatusService(IDocumentStatusRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public override List<SelectListItem> GetAsSelectList(int[] selected = null, bool includeOther = false)
        {
            var result = base.GetAsSelectList(selected, includeOther);

            result.Add(new SelectListItem()
            {
                Value = "100",
                Text = "Вопрос решен",
                Selected = false
            });

            result.Add(new SelectListItem()
            {
                Value = "200",
                Text = "Вопрос завершен",
                Selected = false
            });

            return result;
        }
    }
}