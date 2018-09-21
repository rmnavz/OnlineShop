using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Online_Shop
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>{
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute{});
            });

            #region GetConnectionString
                // Add ApplicationDbContext to DI
                if(Environment.IsProduction()){
                    services.AddDbContext<DatabaseContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));
                }
                else if(Environment.IsDevelopment()){
                    services.AddDbContext<DatabaseContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));
                }
                
            #endregion

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.AccessDeniedPath = "";
                    options.LoginPath = "/Account/Login/";
                    options.LogoutPath = "/Account/Logout/";
                });

            services.AddAuthorization(options =>{
                options.AddPolicy("SA", p => p.RequireAuthenticatedUser().RequireRole("SuperAdministrator"));
                options.AddPolicy("A", p => p.RequireAuthenticatedUser().RequireRole("Administrator"));
                options.AddPolicy("S", p => p.RequireAuthenticatedUser().RequireRole("Seller"));
                options.AddPolicy("C", p => p.RequireAuthenticatedUser().RequireRole("Customer"));
                options.AddPolicy("G", p => p.RequireAuthenticatedUser().RequireRole("Guests"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Index/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
