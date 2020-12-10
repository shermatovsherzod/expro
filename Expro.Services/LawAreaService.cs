using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Expro.Services
{
    public class LawAreaService : BaseDropdownableService<LawArea>, ILawAreaService
    {
        public LawAreaService(ILawAreaRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }

        public void UpdateUserLawAreas(ApplicationUser model, List<int> selectedLawAreas)
        {
            if (selectedLawAreas == null || selectedLawAreas.Count == 0)
            {
                model.UserLawAreas = new List<UserLawArea>();
                return;
            }

            HashSet<int> selectedCategoriesHS = new HashSet<int>(selectedLawAreas);
            if (model.UserLawAreas == null)
                model.UserLawAreas = new List<UserLawArea>();

            HashSet<int> talentLawAreas = new HashSet<int>(model.UserLawAreas
                .Select(m => m.LawAreaID));

            List<LawArea> allLawAreas = GetAll().ToList();
            foreach (var lawArea in allLawAreas)
            {
                if (selectedCategoriesHS.Contains(lawArea.ID))
                {
                    if (!talentLawAreas.Contains(lawArea.ID))
                    {
                        model.UserLawAreas.Add(new UserLawArea()
                        {
                            LawArea = lawArea,
                            User = model
                        });
                    }
                }
                else
                {
                    if (talentLawAreas.Contains(lawArea.ID))
                    {
                        var lawAreaa = model.UserLawAreas.FirstOrDefault(m => m.LawAreaID == lawArea.ID);
                        if (lawAreaa != null)
                            model.UserLawAreas.Remove(lawAreaa);
                    }
                }
            }
        }

        public void UpdateDocumentLawAreas(Document model, List<int> selectedLawAreas)
        {
            if (selectedLawAreas == null || selectedLawAreas.Count == 0)
            {
                model.DocumentLawAreas = new List<DocumentLawArea>();
                return;
            }

            HashSet<int> selectedCategoriesHS = new HashSet<int>(selectedLawAreas);
            if (model.DocumentLawAreas == null)
                model.DocumentLawAreas = new List<DocumentLawArea>();

            HashSet<int> talentLawAreas = new HashSet<int>(model.DocumentLawAreas
                .Select(m => m.LawAreaID));

            List<LawArea> allLawAreas = GetAll().ToList();
            foreach (var lawArea in allLawAreas)
            {
                if (selectedCategoriesHS.Contains(lawArea.ID))
                {
                    if (!talentLawAreas.Contains(lawArea.ID))
                    {
                        model.DocumentLawAreas.Add(new DocumentLawArea()
                        {
                            LawArea = lawArea,
                            LawAreaID = lawArea.ID,
                            Document = model
                        });
                    }
                }
                else
                {
                    if (talentLawAreas.Contains(lawArea.ID))
                    {
                        var lawAreaa = model.DocumentLawAreas.FirstOrDefault(m => m.LawAreaID == lawArea.ID);
                        if (lawAreaa != null)
                            model.DocumentLawAreas.Remove(lawAreaa);
                    }
                }
            }

            List<DocumentLawArea> parentLawAreasToBeAdded = new List<DocumentLawArea>();
            foreach (var item in model.DocumentLawAreas)
            {
                if (item.LawArea.ParentID.HasValue)
                {
                    if (parentLawAreasToBeAdded.FirstOrDefault(m => m.LawAreaID == item.LawArea.ParentID.Value) == null
                        && model.DocumentLawAreas.FirstOrDefault(m => m.LawAreaID == item.LawArea.ParentID.Value) == null)
                    {
                        parentLawAreasToBeAdded.Add(new DocumentLawArea()
                        {
                            LawAreaID = item.LawArea.ParentID.Value,
                            Document = model
                        });
                    }
                }
            }

            if (parentLawAreasToBeAdded.Count > 0)
            {
                foreach (var item in parentLawAreasToBeAdded)
                {
                    model.DocumentLawAreas.Add(item);
                }
            }
        }

        public void UpdateCompanyLawAreas(Company model, List<int> selectedLawAreas)
        {
            if (selectedLawAreas == null || selectedLawAreas.Count == 0)
            {
                model.CompanyLawAreas = new List<CompanyLawArea>();
                return;
            }

            HashSet<int> selectedCategoriesHS = new HashSet<int>(selectedLawAreas);
            if (model.CompanyLawAreas == null)
                model.CompanyLawAreas = new List<CompanyLawArea>();

            HashSet<int> talentLawAreas = new HashSet<int>(model.CompanyLawAreas
                .Select(m => m.LawAreaID));

            List<LawArea> allLawAreas = GetAll().ToList();
            foreach (var lawArea in allLawAreas)
            {
                if (selectedCategoriesHS.Contains(lawArea.ID))
                {
                    if (!talentLawAreas.Contains(lawArea.ID))
                    {
                        model.CompanyLawAreas.Add(new CompanyLawArea()
                        {
                            LawArea = lawArea,
                            LawAreaID = lawArea.ID,
                            Company = model
                        });
                    }
                }
                else
                {
                    if (talentLawAreas.Contains(lawArea.ID))
                    {
                        var lawAreaa = model.CompanyLawAreas.FirstOrDefault(m => m.LawAreaID == lawArea.ID);
                        if (lawAreaa != null)
                            model.CompanyLawAreas.Remove(lawAreaa);
                    }
                }
            }

            List<CompanyLawArea> parentLawAreasToBeAdded = new List<CompanyLawArea>();
            foreach (var item in model.CompanyLawAreas)
            {
                if (item.LawArea.ParentID.HasValue)
                {
                    if (parentLawAreasToBeAdded.FirstOrDefault(m => m.LawAreaID == item.LawArea.ParentID.Value) == null
                        && model.CompanyLawAreas.FirstOrDefault(m => m.LawAreaID == item.LawArea.ParentID.Value) == null)
                    {
                        parentLawAreasToBeAdded.Add(new CompanyLawArea()
                        {
                            LawAreaID = item.LawArea.ParentID.Value,
                            Company = model
                        });
                    }
                }
            }

            if (parentLawAreasToBeAdded.Count > 0)
            {
                foreach (var item in parentLawAreasToBeAdded)
                {
                    model.CompanyLawAreas.Add(item);
                }
            }
        }

    }
}