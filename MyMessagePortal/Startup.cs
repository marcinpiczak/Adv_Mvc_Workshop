using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMessagePortal.EntityConfig;
using MyMessagePortal.Models;

namespace MyMessagePortal
{
    public class Startup
    {
        public Startup()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddXmlFile("appsettings.xml");
            Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                //Added for TempData in Chrome
                //This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<CookieTempDataProviderOptions>(options =>
            {
                options.Cookie.SameSite = SameSiteMode.Lax; // By default this is set to 'Strict'.
            });
			
			services.AddDbContext<EFCContext>(builder =>
            {
                var cs = Configuration["ConnectionStrings:DefaultConnection"];
                builder.UseSqlServer(cs);
            }, ServiceLifetime.Transient);

            services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<EFCContext>();

            services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
            }
			
			app.UseHttpsRedirection();

            app.UseAuthentication();

			app.UseStaticFiles();
			app.UseCookiePolicy();
			
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
