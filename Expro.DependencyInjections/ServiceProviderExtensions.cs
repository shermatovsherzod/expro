using Expro.Data.Infrastructure;
using Expro.Data.Repository;
using Expro.Data.Repository.Interfaces;
using Expro.Services;
using Expro.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expro.DependencyInjections
{
    public static class ServiceProviderExtensions
    {
        public static void AddCommonDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDatabaseFactory, DatabaseFactory>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ILawAreaRepository, LawAreaRepository>();

            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IEducationRepository, EducationRepository>();
            services.AddTransient<IWorkExperienceRepository, WorkExperienceRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IUserPurchasedDocumentRepository, UserPurchasedDocumentRepository>();
            services.AddTransient<IDocumentStatusRepository, DocumentStatusRepository>();
            services.AddTransient<IClickTransactionRepository, ClickTransactionRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            //services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ILawAreaService, LawAreaService>();

            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IEducationService, EducationService>();
            services.AddTransient<IWorkExperienceService, WorkExperienceService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IUserBalanceService, UserBalanceService>();
            services.AddTransient<IUserPurchasedDocumentService, UserPurchasedDocumentService>();
            services.AddTransient<IHangfireService, HangfireService>();
            services.AddTransient<IDocumentStatusService, DocumentStatusService>();
            services.AddTransient<ISampleDocumentService, SampleDocumentService>();
            services.AddTransient<IArticleDocumentService, ArticleDocumentService>();
            services.AddTransient<IDocumentSearchService, DocumentSearchService>();
            services.AddTransient<ISampleDocumentSearchService, SampleDocumentSearchService>();
            services.AddTransient<IArticleDocumentSearchService, ArticleDocumentSearchService>();
            services.AddTransient<IDocumentAdminActionsService, DocumentAdminActionsService>();
            services.AddTransient<IDocumentCounterService, DocumentCounterService>();
            services.AddTransient<IPracticeDocumentService, PracticeDocumentService>();
            services.AddTransient<IPracticeDocumentSearchService, PracticeDocumentSearchService>();
            services.AddTransient<IClickTransactionService, ClickTransactionService>();
        }
    }
}
