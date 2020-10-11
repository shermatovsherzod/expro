using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Expro.Controllers
{
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;

        public CityController(
            ICityService cityService)
        {
            _cityService = cityService;
            //_logger = logger;
        }

        public List<SelectListItem> GetByRegionIDAsSelectList(int regionID, int? selected = null)
        {
            int[] selectedCities = null;
            if (selected.HasValue)
                selectedCities = new int[1] { selected.Value };

            return _cityService.GetByRegionIDAsSelectList(regionID, selectedCities, true);
        }
    }
}
