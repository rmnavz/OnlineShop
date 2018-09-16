using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        protected string ConnectionString;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>{
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute{});
            });

            #region GetConnectionString

                if(Environment.IsDevelopment())
                {
                    ConnectionString = Configuration.GetConnectionString("DevelopmentConnection");
                }
                else if(Environment.IsProduction())
                {
                    ConnectionString = Configuration.GetConnectionString("ProductionConnection");
                }
                else
                {
                    ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                }
                
            #endregion

            // Add ApplicationDbContext to DI
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(ConnectionString));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.AccessDeniedPath = "";
                    options.LoginPath = "/Account/Login/";
                    options.LogoutPath = "/Account/Logout/";
                });

            services.AddAuthorization(options =>{
                options.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                options.AddPolicy("Member", p => p.RequireAuthenticatedUser().RequireRole("Member"));
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
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
