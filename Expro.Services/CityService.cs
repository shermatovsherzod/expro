using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

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
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;


            var items = GetByRegionID(regionID).ToList();

            var result = new List<SelectListItem>();
            foreach (var item in items)
            {
                var selectListItem = new SelectListItem();
                selectListItem.Value = item.ID.ToString();

                if (item.NameShort != null)
                {
                    if (cultureInfo.Name == "fr") //uz
                        selectListItem.Text = item.NameShort.TextUz;
                    else
                        selectListItem.Text = item.NameShort.TextRu;
                }
                if (string.IsNullOrWhiteSpace(selectListItem.Text))
                    selectListItem.Text = item.Name;

                selectListItem.Selected = (selected != null && selected.Contains(item.ID));

                result.Add(selectListItem);
            }

            //.Select(item => new SelectListItem()
            //{
            //    Value = item.ID.ToString(),
            //    Text = item.Name.ToString(),
            //    Selected = (selected != null && selected.Contains(item.ID))
            //}).ToList();

            if (includeOther)
            {
                string otherCity = "";
                if (cultureInfo.Name == "fr") //uz
                    otherCity="Бошқа шаҳар";
                else
                    otherCity = "Другой город";

                result.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = otherCity,
                    Selected = (selected != null && selected.Contains(0))
                });
            }

            return result;
        }
    }
}