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

        }
    }
}
