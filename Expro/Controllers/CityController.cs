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

        public List<SelectListItem> GetByRegionIDAsSelectList(int regionID)
        {
            return _cityService.GetByRegionIDAsSelectList(regionID);
        }
    }
}
