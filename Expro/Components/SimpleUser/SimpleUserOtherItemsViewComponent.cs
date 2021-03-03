using Expro.Models;
using Expro.Services.Interfaces;
using Expro.Utils;
using Expro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.Components
{
    public class SimpleUserOtherItemsViewComponent : BaseViewComponent
    {
        private readonly ICompanyService _companyService;
        private readonly IVacancyService _vacancyService;
        private readonly IResumeService _resumeService;

        public SimpleUserOtherItemsViewComponent(
            ICompanyService companyService,
            IVacancyService vacancyService,
            IResumeService resumeService)
            : base()
        {
            _companyService = companyService;
            _vacancyService = vacancyService;
            _resumeService = resumeService;
        }

        public override IViewComponentResult Invoke()
        {
            var curUser = accountUtil.GetCurrentUser(HttpContext.User);
            OtherItemsCountVM vmodel = new OtherItemsCountVM()
            {
                CompaniesCount = _companyService.GetCompanyByCreatorID(curUser.ID).Count(),
                VacanciesCount = _vacancyService.GetVacancyByCreatorID(curUser.ID).Count(),
                ResumesCount = _resumeService.GetResumeByCreatorID(curUser.ID).Count(),
            };

            return View("SimpleUserOtherItems", vmodel);
        }
    }
}
