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
    }
}