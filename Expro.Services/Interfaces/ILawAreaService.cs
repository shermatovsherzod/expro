using Expro.Models;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{
    public interface ILawAreaService : IBaseDropdownableService<LawArea>
    {
        void UpdateUserLawAreas(ApplicationUser model, List<int> selectedLawAreas);
    }
}