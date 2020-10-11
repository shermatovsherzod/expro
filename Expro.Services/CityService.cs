using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class CityService : BaseDropdownableService<City>, ICityService
    {
        public CityService(ICityRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public IEnumerable<City> GetByRegionID(int regionID)
        {
            var result = GetAsIQueryable()
                .Where(m => m.RegionID == regionID)
                .ToList();

            return result;
        }

        public List<SelectListItem> GetByRegionIDAsSelectList(int regionID, int[] selected = null, bool includeOther = false)
        {
            var result = GetByRegionID(regionID)
                .Select(item => new SelectListItem()
                {
                    Value = item.ID.ToString(),
                    Text = item.Name.ToString(),
                    Selected = (selected != null && selected.Contains(item.ID))
                }).ToList();

            if (includeOther)
            {
                result.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "Другой город",
                    Selected = (selected != null && selected.Contains(0))
                });
            }

            return result;
        }
    }
}