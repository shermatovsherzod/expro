using Expro.Models;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{
    public interface ILawAreaService : IBaseDropdownableService<LawArea>
    {
        void UpdateUserLawAreas(ApplicationUser model, List<int> selectedLawAreas);
        void UpdateDocumentLawAreas(Document model, List<int> selectedLawAreas);
        void UpdateQuestionLawAreas(Question model, List<int> selectedLawAreas);
        void UpdateCompanyLawAreas(Company model, List<int> selectedLawAreas);
    }
}