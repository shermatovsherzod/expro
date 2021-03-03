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
    public class SidebarItemsViewComponent : BaseViewComponent
    {
        private readonly IUserService _userService;

        private readonly IQuestionService _questionService;
        private readonly IArticleDocumentService _articleDocumentService;
        private readonly IPracticeDocumentService _practiceDocumentService;
        private readonly ISampleDocumentService _sampleDocumentService;

        private readonly ICompanyService _companyService;
        private readonly IVacancyService _vacancyService;
        private readonly IResumeService _resumeService;

        public SidebarItemsViewComponent(
            IUserService userService,
            IQuestionService questionService,
            IArticleDocumentService articleDocumentService,
            IPracticeDocumentService practiceDocumentService,
            ISampleDocumentService sampleDocumentService,
            ICompanyService companyService,
            IVacancyService vacancyService,
            IResumeService resumeService)
            : base()
        {
            _userService = userService;

            _questionService = questionService;
            _articleDocumentService = articleDocumentService;
            _practiceDocumentService = practiceDocumentService;
            _sampleDocumentService = sampleDocumentService;

            _companyService = companyService;
            _vacancyService = vacancyService;
            _resumeService = resumeService;
        }

        public override IViewComponentResult Invoke()
        {
            var curUser = accountUtil.GetCurrentUser(HttpContext.User);
            AdminSidebarItemsCountVM vmodel = new AdminSidebarItemsCountVM()
            {
                SimpleUsersCount = _userService.GetAllSimpleUsersForAdmin().Count(),
                ExpertsCount = _userService.GetAllExpertsForAdmin().Count(),

                QuestionsCount = _questionService.GetAllForAdmin().Count(),
                ArticleDocumentsCount = _articleDocumentService.GetAllForAdmin().Count(),
                PracticeDocumentsCount = _practiceDocumentService.GetAllForAdmin().Count(),
                SampleDocumentsCount = _sampleDocumentService.GetAllForAdmin().Count(),

                CompaniesCount = _companyService.GetAllForAdmin().Count(),
                VacanciesCount = _vacancyService.GetAllForAdmin().Count(),
                ResumesCount = _resumeService.GetAllForAdmin().Count()
            };

            return View("SidebarItems", vmodel);
        }
    }
}
