using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{  
    public interface IVacancyStatusService  :IBaseDropdownableService<VacancyStatus>
    {
        //List<SelectListItem> GetAsSelectList();
    }
}