using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Expro.Services
{
    public class ExpertStatusService : BaseDropdownableService<UserStatus>, IExpertStatusService
    {
        public ExpertStatusService(IUserStatusRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public List<SelectListItem> GetAsSelectList()
        {
            return _repository.GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = CultureInfo.CurrentCulture.Name == "fr" ? item.NameShort.TextUz : item.NameShort.TextRu
            }).ToList();
        }
    }
}