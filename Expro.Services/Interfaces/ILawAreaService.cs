using Expro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Expro.Services.Interfaces
{
    public interface ILawAreaService : IBaseDropdownableService<LawArea>
    {
        void UpdateUserLawAreas(ApplicationUser model, List<int> selectedLawAreas);
        void UpdateDocumentLawAreas(Document model, List<int> selectedLawAreas);
        void UpdateQuestionLawAreas(Question model, List<int> selectedLawAreas);
        void UpdateCompanyLawAreas(Company model, List<int> selectedLawAreas);
        List<SelectListItem> GetAsGroupedSelectList(List<int> selectedLawAreas = null);
        List<SelectListItem> GetAsGroupedSelectListForUser(ApplicationUser model);
        List<SelectListItem> GetAsGroupedSelectListForDocument(Document model);
        List<SelectListItem> GetAsGroupedSelectListForQuestion(Question model);
        List<SelectListItem> GetAsGroupedSelectListForCompany(Company model);
    }
}