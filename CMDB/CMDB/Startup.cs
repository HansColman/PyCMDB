using CMDB.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CMDB
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration config)
        {
            this.Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddProblemDetails();
            services.AddControllersWithViews(x => x.SuppressAsyncSuffixInActionNames = false).AddRazorRuntimeCompilation();
            services.AddScoped<ITokenStore, TokenStore>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ReleaseDevice",
                    pattern: "identity/ReleaseDevice/{id?}/{AssetTag}",
                    defaults: new {controller= "Identity", action = "ReleaseDevice" });
                endpoints.MapControllerRoute(
                    name: "ReleaseMobile",
                    pattern: "{controller=Identity}/{action=ReleaseMobile}/{id?}/{MobileId}");
                endpoints.MapControllerRoute(
                    name: "ReleaseInternetSubscription",
                    pattern: "{controller=Identity}/{action=ReleaseInternetSubscription}/{id?}/{SubscriptionId}");
                endpoints.MapControllerRoute(
                    name: "ReleaseIdentity",
                    pattern: "{controller=Mobile}/{action=ReleaseIdentity}/{id?}/{idenid}");
                endpoints.MapControllerRoute(
                    name: "ReleaseSubscription",
                    pattern: "{controller=Mobile}/{action=ReleaseSubscription}/{id?}/{SubscriptionId}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
