using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Dal.Abstract;
using Petroteks.Dal.Concreate.EntityFramework;
using Petroteks.MvcUi.Services;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Petroteks.MvcUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();

            services.AddScoped<IWebsiteService, WebsiteManager>();
            services.AddScoped<IWebsiteDal, EfWebsiteDal>();

            services.AddScoped<IAboutUsObjectService, AboutUsObjectManager>();
            services.AddScoped<IAboutUsObjectDal, EfAboutUsObjectDal>();

            services.AddScoped<IPrivacyPolicyObjectService, PrivacyPolicyObjectManager>();
            services.AddScoped<IPrivacyPolicyObjectDal, EfPrivacyPolicyObjectDal>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();

            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDal, EfProductDal>();

            services.AddScoped<IMainPageService, MainPageManager>();
            services.AddScoped<IMainPageDal, EfMainPageDal>();

            services.AddScoped<IEmailService, EmailManager>();
            services.AddScoped<IEmailDal, EfEmailDal>();

            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IBlogDal, EfBlogDal>();

            services.AddScoped<IDynamicPageService, DynamicPageManager>();
            services.AddScoped<IDynamicPageDal, EfDynamicPageDal>();

            services.AddScoped<ILanguageService, LanguageManager>();
            services.AddScoped<ILanguageDal, EfLanguageDal>();

            services.AddScoped<IUI_NavbarService, UI_NavbarManager>();
            services.AddScoped<IUI_NavbarDal, EfUI_NavbarDal>();

            services.AddScoped<IUI_ContactService, UI_ContactManager>();
            services.AddScoped<IUI_ContactDal, EfUI_ContactDal>();

            services.AddScoped<IUI_FooterService, UI_FooterManager>();
            services.AddScoped<IUI_FooterDal, EfUI_FooterDal>();

            services.AddScoped<IUI_NoticeService, UI_NoticeManager>();
            services.AddScoped<IUI_NoticeDal, EfUI_NoticeDal>();

            services.AddSingleton<IUserSessionService, UserSessionService>();
            services.AddSingleton<IUserCookieService, UserCookieService>();
            services.AddSingleton<ILanguageCookieService, LanguageCookieService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA }));

            services.AddRazorPages();
            IMvcBuilder mvcBuilder = services.AddControllersWithViews();
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif

            services.AddSession();
            services.AddDistributedMemoryCache();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/500");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
