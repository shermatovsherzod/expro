using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expro.Services.Interfaces
{
    public interface IBaseDropdownableService<T> : IBaseCRUDService<T> 
        where T : BaseModelDropdownable
    {
        List<SelectListItem> GetAsSelectList(int[] selected = null, bool includeOther = false);
    }
}
