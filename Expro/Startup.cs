using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Expro.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Expro.Models;
using Expro.Utils;
using Expro.Common;
using Expro.DependencyInjections;
using Microsoft.Extensions.FileProviders;
using System.IO;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using Hangfire;
using Hangfire.SqlServer;
using Expro.Filters;
using Hangfire.Dashboard;
using Expro.Hubs;
using System.Globalization;
using Expro.Models.Enums;

namespace Expro
{
    public class Startup
    {
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            string connectionStringName = "DefaultConnection";
#else
            string connectionStringName = "ServerConnection";
#endif

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(connectionStringName),
                    b => b.MigrationsAssembly("Expro").UseRowNumberForPaging()));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();

            services.Configure<AppConfiguration>(Configuration.GetSection("MySettings"));

            //password requirements
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            //add common dependencies
            services.AddCommonDependencies(); //UnitOfWork and DatabaseFactory

            //add repositories
            services.AddRepositories();

            //add services
            services.AddServices();

            var webRoot = _env.ContentRootPath;

            //services.AddSingleton<IFileProvider>(
            //  new PhysicalFileProvider(
            //    Path.Combine(webRoot, "Uploads")));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            ConfigureFirebase();

            //Hangfire
#if DEBUG
            string connectionString = "Data Source=209.159.151.3\\SQLEXPRESS,1433;Initial Catalog=Expro;User Id=sa;Password=TD9N9CkDmQFNZ7HZ;";
#else
            string connectionString = "Data Source=INT-VM;Initial Catalog=Expro;User Id=sa;Password=44c2n8savJP8!;";
#endif
            services.AddHangfire(config =>
                config.UseSqlServerStorage(
                    connectionString,
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                        QueuePollInterval = TimeSpan.Zero,
                        UseRecommendedIsolationLevel = true,
                        UsePageLocksOnDequeue = true,
                        DisableGlobalLocks = true
                    }
                )
            );

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddSignalR();

            services.AddMvc()
                .AddViewLocalization();
            //.AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; }); ;

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
                    new CultureInfo("ru"),
                    new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("UserType", ((int)UserTypesEnum.Admin).ToString()));
                options.AddPolicy("ExpertOnly", policy => policy.RequireClaim("UserType", ((int)UserTypesEnum.Expert).ToString()));
                options.AddPolicy("SimpleUserOnly", policy => policy.RequireClaim("UserType", ((int)UserTypesEnum.SimpleUser).ToString()));
                options.AddPolicy("AdminOrSimpleUserOnly", policy => policy.RequireClaim("UserType", ((int)UserTypesEnum.Admin).ToString(), ((int)UserTypesEnum.SimpleUser).ToString()));
                options.AddPolicy("ExpertOrSimpleUserOnly", policy => policy.RequireClaim("UserType", ((int)UserTypesEnum.Expert).ToString(), ((int)UserTypesEnum.SimpleUser).ToString()));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            IOptions<AppConfiguration> appSettings
            /*, IBackgroundJobClient backgroundJobs*/)
        {
            AppData.Configuration = appSettings.Value;
            if (env.IsDevelopment())
            {
                //app.UseStatusCodePagesWithReExecute("/Error/Status/{0}");
                //app.UseExceptionHandler("/Error/Index");

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseStatusCodePagesWithReExecute("/Error/Status/{0}");
                //app.UseExceptionHandler("/Error/Index");

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Expert",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "User",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();

                //endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<UserTrackerHub>("/usertrackerhub");
            });

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
                IsReadOnlyFunc = (DashboardContext context) => true
            });

            

            ////both variants work
            //BackgroundJob.Enqueue(() => Console.WriteLine("Hello world from Hangfire 1!"));
            //backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire 2!"));
        }

        private void ConfigureFirebase()
        {
            AppOptions options = new AppOptions()
            {
                Credential = GoogleCredential.FromFile(_env.WebRootPath + "\\firebase\\expro-d7dd0-firebase-adminsdk-zr1rv-9c5c8eda64.json")
            };

            FirebaseApp.Create(options);
        }
    }
}
