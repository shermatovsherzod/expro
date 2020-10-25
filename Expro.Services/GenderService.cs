using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class GenderService : BaseCRUDService<Gender>, IGenderService
    {
        public GenderService(IGenderRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IEnumerable<Gender> GetByGenderID(int genderID)
        {
            var result = GetAsIQueryable()
                .Where(m => m.ID == genderID)
                .ToList();

            return result;
        }

        public List<SelectListItem> GetAsSelectListOne(int? selected = null)
        {
            var result = GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name.ToString(),
                Selected = (selected != null && selected == item.ID)
            }).ToList();

            return result;
        }

    }
}