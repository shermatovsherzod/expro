using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expro.Services.Interfaces
{
    public interface IBaseDropdownableService<T> : IBaseCRUDService<T> 
        where T : BaseModelDropdownable
    {
        IQueryable<T> GetWithLocalizedNameAsIQueryable();
        List<SelectListItem> GetAsSelectList(int[] selected = null, bool includeOther = false);
        List<SelectListItem> GetAsSelectListOne(int? selected = null, bool includeOther = false);
    }
}
