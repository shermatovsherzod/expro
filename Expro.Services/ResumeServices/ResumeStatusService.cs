using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class ResumeStatusService : BaseDropdownableService<ResumeStatus>, IVacancyStatusService
    {
        public ResumeStatusService(IResumeStatusRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public List<SelectListItem> GetAsSelectList()
        {
            var result = _repository.GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name.ToString()
            }).ToList();

            return result;
        }
    }
}