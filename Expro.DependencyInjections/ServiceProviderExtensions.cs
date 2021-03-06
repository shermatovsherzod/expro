﻿using Expro.Data.Infrastructure;
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
          //  services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ILawAreaRepository, LawAreaRepository>();

            services.AddTransient<IRegionRepository, RegionRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IGenderRepository, GenderRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IExpertEducationRepository, ExpertEducationRepository>();
            services.AddTransient<IWorkExperienceRepository, WorkExperienceRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            services.AddTransient<IUserPurchasedDocumentRepository, UserPurchasedDocumentRepository>();
            services.AddTransient<IDocumentStatusRepository, DocumentStatusRepository>();
            services.AddTransient<IClickTransactionRepository, ClickTransactionRepository>();
            services.AddTransient<IWithdrawRequestRepository, WithdrawRequestRepository>();
            services.AddTransient<IWithdrawRequestStatusRepository, WithdrawRequestStatusRepository>();
            services.AddTransient<IWithdrawRequestStatusRepository, WithdrawRequestStatusRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserStatusRepository, UserStatusRepository>();
            services.AddTransient<IQuestionAnswerRepository, QuestionAnswerRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IVacancyRepository, VacancyRepository>();
            services.AddTransient<IResumeRepository, ResumeRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            //services.AddTransient<IQuestionAnswerLikeRepository, QuestionAnswerLikeRepository>();
            services.AddTransient<IVacancyStatusRepository, VacancyStatusRepository>();
            services.AddTransient<IResumeStatusRepository, ResumeStatusRepository>();
            services.AddTransient<ICompanyStatusRepository, CompanyStatusRepository>();
            services.AddTransient<IRatingUpdateRepository, RatingUpdateRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFeedbackRepository, FeedbackRepository>();
            services.AddTransient<IFeedbackStatusRepository, FeedbackStatusRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            //services.AddTransient<IEmailService, EmailService>();

         //   services.AddTransient<IPostService, PostService>();
            services.AddTransient<ILawAreaService, LawAreaService>();

            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IGenderService, GenderService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IExpertEducationService, ExpertEducationService>();
            services.AddTransient<IWorkExperienceService, WorkExperienceService>();
            services.AddTransient<ILanguageService, LanguageService>();
            services.AddTransient<IAttachmentService, AttachmentService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IUserBalanceService, UserBalanceService>();
            services.AddTransient<IUserPurchasedDocumentService, UserPurchasedDocumentService>();
            services.AddTransient<IUserPurchasedArticleDocumentService, UserPurchasedArticleDocumentService>();
            services.AddTransient<IUserPurchasedSampleDocumentService, UserPurchasedSampleDocumentService>();
            services.AddTransient<IUserPurchasedPracticeDocumentService, UserPurchasedPracticeDocumentService>();
            services.AddTransient<IHangfireService, HangfireService>();
            services.AddTransient<IDocumentStatusService, DocumentStatusService>();
            services.AddTransient<ISampleDocumentService, SampleDocumentService>();
            services.AddTransient<IArticleDocumentService, ArticleDocumentService>();
            services.AddTransient<IDocumentSearchService, DocumentSearchService>();
            services.AddTransient<ISampleDocumentSearchService, SampleDocumentSearchService>();
            services.AddTransient<IArticleDocumentSearchService, ArticleDocumentSearchService>();
            services.AddTransient<IDocumentAdminActionsService, DocumentAdminActionsService>();
            services.AddTransient<IArticleDocumentAdminActionsService, ArticleDocumentAdminActionsService>();
            services.AddTransient<IPracticeDocumentAdminActionsService, PracticeDocumentAdminActionsService>();
            services.AddTransient<ISampleDocumentAdminActionsService, SampleDocumentAdminActionsService>();
            services.AddTransient<IQuestionAdminActionsService, QuestionAdminActionsService>();
            services.AddTransient<IDocumentCounterService, DocumentCounterService>();
            services.AddTransient<IPracticeDocumentService, PracticeDocumentService>();
            services.AddTransient<IPracticeDocumentSearchService, PracticeDocumentSearchService>();
            services.AddTransient<IClickTransactionService, ClickTransactionService>();
            services.AddTransient<IWithdrawRequestService, WithdrawRequestService>();
            services.AddTransient<IWithdrawRequestStatusService, WithdrawRequestStatusService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionSearchService, QuestionSearchService>();
            services.AddTransient<IUserStatusService, UserStatusService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IQuestionAnswerService, QuestionAnswerService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IVacancyService, VacancyService>();
            services.AddTransient<IResumeService, ResumeService>();
            services.AddTransient<IQuestionStatusService, QuestionStatusService>();
            services.AddTransient<IQuestionCounterService, QuestionCounterService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVacancyStatusService, VacancyStatusService>();
            services.AddTransient<IVacancySearchService, VacancySearchService>();
            services.AddTransient<IVacancyAdminActionsService, VacancyAdminActionsService>();
            services.AddTransient<IResumeStatusService, ResumeStatusService>();
            services.AddTransient<IResumeSearchService, ResumeSearchService>();
            services.AddTransient<IResumeAdminActionsService, ResumeAdminActionsService>();
            services.AddTransient<ICompanyStatusService, CompanyStatusService>();
            services.AddTransient<ICompanySearchService, CompanySearchService>();
            services.AddTransient<ICompanyAdminActionsService, CompanyAdminActionsService>();
            services.AddTransient<IRatingUpdateService, RatingUpdateService>();
            services.AddTransient<IUserRatingService, UserRatingService>();

            services.AddTransient<IExpertsListAdminActionsService, ExpertsListAdminActionsService>();
            services.AddTransient<IExpertsListSearchService, ExpertsListSearchService>();
            services.AddTransient<IExpertStatusService, ExpertStatusService>();

            services.AddTransient<ISimpleUsersListAdminActionsService, SimpleUsersListAdminActionsService>();
            services.AddTransient<ISimpleUsersListSearchService, SimpleUsersListSearchService>();

            services.AddTransient<IFeedbackService, FeedbackService>();
            services.AddTransient<IFeedbackAdminActionsService, FeedbackAdminActionsService>();
            
            services.AddTransient<IFeedbackSearchService, FeedbackSearchService>();
            services.AddTransient<IFeedbackStatusService, FeedbackStatusService>();

            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
