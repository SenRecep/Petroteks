using System;
using System.IO.Compression;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;

using GCSAPI;

using ImageMagick;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Petroteks.Bll.Abstract;
using Petroteks.Bll.Concreate;
using Petroteks.Dal.Abstract;
using Petroteks.Dal.Concreate.EntityFramework;
using Petroteks.Entities.Concreate;
using Petroteks.MvcUi.ExtensionMethods;
using Petroteks.MvcUi.Models;
using Petroteks.MvcUi.Services;

using Smidge;
using Smidge.Cache;
using Smidge.Options;

namespace Petroteks.MvcUi
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "MyPolicy";

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

            services.AddScoped<IML_ProductService, ML_ProductManager>();
            services.AddScoped<IML_ProductDal, EfML_ProductDal>();

            services.AddSingleton<UrlControlHelper>();

            services.AddScoped<DbServiceProvider>();
            services.AddScoped<SearchManager>();
            services.AddScoped<SearchSystem>();

            services.AddSingleton<IUserSessionService, UserSessionService>();

            services.AddSingleton<IUserCookieService, UserCookieService>();
            services.AddSingleton<ILanguageCookieService, LanguageCookieService>();
            services.AddSingleton<IWebsiteCookieService, WebsiteCookieService>();

            // services.AddSingleton<ISavedWebsiteFactory, SavedWebsiteFactory>();
            services.AddSingleton<IRouteTable, RouteTable>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.Configure<CustomSearchSetting>(Configuration.GetSection("CustomSearchSetting"));

            services.Configure<Language>(Configuration.GetSection("DefaultLanguage"));

            services.AddHttpClient<CustomSearchApi>();

            services.AddSingleton<ImageOptimizer>();

            services.AddHostedService<ExistingImageOptimizer>();

            //services.AddDbContext<PetroteksDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("SomeePetroteksDbConnectionString")));

            services.AddSingleton(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA }));

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder
                                      .AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod();
                                  });
            });

            services.AddSession();

            services.AddScoped<ICacheService, CacheManager>();
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddResponseCaching();

            services.AddRazorPages();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes =
                    ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "image/svg+xml" });
            });


            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });


            services.AddSmidge(Configuration.GetSection("Smidge"));

            IMvcBuilder mvcBuilder = services.AddControllersWithViews(opts =>
            {
                //[ResponseCache(CacheProfileName = “BasicCache”)]
                opts.CacheProfiles.Add("BasicCache", new CacheProfile
                {
                    Duration = (int)TimeSpan.FromMinutes(30).TotalSeconds
                });
            });
#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif



            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/500");
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");


            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse =
                  r =>
                  {
                      string path = r.File.PhysicalPath;
                      if (
                      path.EndsWith(".gif") || path.EndsWith(".jpg") ||
                      path.EndsWith(".png") || path.EndsWith(".svg") ||
                      path.EndsWith(".webp") || path.EndsWith(".woff2") ||
                      path.EndsWith(".css") || path.EndsWith(".js") ||
                      path.EndsWith(".xml"))
                      {
                          TimeSpan maxAge = new TimeSpan(365, 0, 0, 0);
                          r.Context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));
                      }
                  }
            });


            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseResponseCaching();

            app.UseResponseCompression();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseSession();


            //var routeTable = serviceProvider.GetService<IRouteTable>();



            BundleEnvironmentOptions bundleEnvironmentOptions = BundleEnvironmentOptions.Create().ForDebug(opts =>
                       opts.EnableCompositeProcessing()
                       .EnableFileWatcher()
                       .SetCacheBusterType<AppDomainLifetimeCacheBuster>()
                       .CacheControlOptions(enableEtag: false, cacheControlMaxAge: 0)
                    ).Build();

            app.UseSmidge(manager =>
            {
                manager.CreateJs("layout-login-js",
                    "~/Admin/assets/libs/jquery/dist/jquery.min.js",
                    "~/Admin/assets/libs/popper.js/dist/umd/popper.min.js ",
                    "~/Admin/assets/libs/bootstrap/dist/js/bootstrap.min.js",
                    "~/Admin/dist/js/notify.js")
                .WithEnvironmentOptions(bundleEnvironmentOptions);

                manager.CreateCss("layout-login-css",
                  "~/Admin/dist/css/style.min.css")
                .WithEnvironmentOptions(bundleEnvironmentOptions);




            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers()
                     .RequireCors(MyAllowSpecificOrigins);

                endpoints.MapRazorPages();

                //endpoints.MapControllerRoute(
                //   name: "bloglist",
                //   pattern: "{blogPageName}.html",
                //   defaults: new { area = "", controller = "Home", action = "PetroBlog" },
                //   constraints: new { blogPageName = new BlogListConstraint(routeTable) }
                //   );

                //endpoints.MapControllerRoute(
                //    name: "blogDetail",
                //    pattern: "Blog/{blogPageName}/{id:int}/{title}",
                //    defaults: new { area = "", controller = "Detail", action = "BlogDetail" },
                //    constraints: new { blogPageName = new BlogDetailConstraint(routeTable) }
                //    );

                //endpoints.MapControllerRoute(
                //   name: "categoryDetail",
                //   pattern: "{pageTag}/{id:int}/{categoryName}",
                //   defaults: new { area = "", controller = "Detail", action = "CategoryDetail" },
                //   constraints: new { pageTag = new CategoryDetailConstraint(routeTable) }
                //   );

                //endpoints.MapControllerRoute(
                //   name: "productDetail",
                //   pattern: "{pageTag}/{produtname}/{id:int}",
                //   defaults: new { area = "", controller = "Detail", action = "BlogDetail" },
                //   constraints: new { pageTag = new ProductDetailConstraint(routeTable) }
                //   );

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
