using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            // Get ConnectionString based on environment
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

            // Add ApplicationDbContext to DI
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(ConnectionString));

            services.AddMvc();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
