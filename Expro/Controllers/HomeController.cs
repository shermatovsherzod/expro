using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Expro.Models;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Expro.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Expro.ViewModels;
using Expro.Services.Interfaces;

namespace Expro.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IQuestionService _questionService;
        private readonly IArticleDocumentService _articleDocumentService;
        private readonly ISampleDocumentService _sampleDocumentService;
        private readonly IPracticeDocumentService _practiceDocumentService;
        private readonly IUserService _userService;

        protected AppConfiguration AppConfiguration { get; set; }

        public HomeController(
            IQuestionService questionService,
            IArticleDocumentService articleDocumentService,
            ISampleDocumentService sampleDocumentService,
            IPracticeDocumentService practiceDocumentService,
            IUserService userService,
            ILogger<HomeController> logger,
            IOptionsSnapshot<AppConfiguration> settings = null)
        {
            _questionService = questionService;
            _articleDocumentService = articleDocumentService;
            _sampleDocumentService = sampleDocumentService;
            _practiceDocumentService = practiceDocumentService;
            _userService = userService;

            _logger = logger;

            if (settings != null) 
                AppConfiguration = settings.Value;
        }

        public IActionResult Index()
        {
            var m1 = AppConfiguration.RatingThresholdForCreatingPaidDocuments;
            //var m2 = Configuration.GetSection("MySettings")["RatingThresholdForCreatingPaidDocuments"];


            int k = AppData.Configuration.RatingThresholdForCreatingPaidDocuments;

            ViewData["curPageUrl"] = Request.Path.Value;

            bool showAskQuestionCTA = true;
            string askQuestionCTAUrl = "#";

            var curUser = accountUtil.GetCurrentUser(User);
            if (User.Identity.IsAuthenticated)
            {
                if (curUser.IsSimpleUser)
                    askQuestionCTAUrl = "/User/Question";
                else
                    showAskQuestionCTA = false;
            }
            else
                askQuestionCTAUrl = "/Identity/Account/Login";

            ViewData["showAskQuestionCTA"] = showAskQuestionCTA;
            ViewData["askQuestionCTAUrl"] = askQuestionCTAUrl;

            HomePageVM modelVM = new HomePageVM()
            {
                Stats = new HomePageStatsVM(),
                //{
                //    QuestionsCount = _questionService.GetAllApproved().Count(),
                //    ArticlesCount = _articleDocumentService.GetAllApproved().Count(),
                //    SampleDocumentsCount = _sampleDocumentService.GetAllApproved().Count(),
                //    ExpertsCount = _userService.GetAllExpertsForAdmin().Count()
                //},
                FeaturedSampleDocuments = new List<DocumentListItemForSiteVM>(),
                FeaturedPracticeDocuments = new List<DocumentListItemForSiteVM>(),
                FeaturedQuestions = new List<QuestionListItemForSiteVM>(),
                TopExperts = new List<ExpertTopInfoVM>()
            };

            //var featuredSampleDocuments = _sampleDocumentService.GetRandomDocuments(3);
            //foreach (var item in featuredSampleDocuments)
            //{
            //    modelVM.FeaturedSampleDocuments.Add(new DocumentListItemForSiteVM(item));
            //}

            //var featuredPracticeDocuments = _practiceDocumentService.GetRandomDocuments(3);
            //foreach (var item in featuredPracticeDocuments)
            //{
            //    modelVM.FeaturedPracticeDocuments.Add(new DocumentListItemForSiteVM(item));
            //}

            //var featuredQuestions = _questionService.GetRandomQuestions(3);
            //foreach (var item in featuredQuestions)
            //{
            //    modelVM.FeaturedQuestions.Add(new QuestionListItemForSiteVM(item));
            //}

            var topExperts = _userService.GetTopExperts(4);
            foreach (var item in topExperts)
            {
                modelVM.TopExperts.Add(new ExpertTopInfoVM(item));
            }

            return View(modelVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Foobar()
        {
            return View();
        }

        public IActionResult ComingSoon()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
