using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{
    public interface ICityService : IBaseDropdownableService<City>
    {
        IEnumerable<City> GetByRegionID(int regionID);
        List<SelectListItem> GetByRegionIDAsSelectList(int regionID, bool includeOther = false);
    }
}