using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlog.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SimpleBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Configuration["DATABASE_URL"]));
            services.AddControllersWithViews();
            services.AddTransient<IRepository, PostgresRepository>();
            services.AddControllersWithViews();
            services.AddRazorPages();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Blog}/{action=Posts}/{id?}");

               /* endpoints.MapControllerRoute(
                   name: "NewPost",
                   pattern: "blog/newpost",
                   defaults: new { controller = "Blog", action = "NewPost" });

                endpoints.MapControllerRoute(
                   name: "Categories",
                   pattern: "blog/categories",
                   defaults: new { controller = "Blog", action = "Categories" });*/

               /* endpoints.MapControllerRoute(
                    name: "Detail",
                    pattern: "blog/details",
                    defaults: new { controller = "Blog", action = "Detail/id?" });
                endpoints.MapControllerRoute(
                    name: "Edit",
                    pattern: "blog/edit",
                    defaults: new { controller = "Blog", action = "Edit/id?" });*/

                /*endpoints.MapControllerRoute(
                     name: "detail",
                     pattern: "{controller = Blog}/{action=Detail}/{id?}");
                endpoints.MapControllerRoute(
                    name: "edit",
                    pattern: "{controller=Blog}/{action=Posts}/{id?}");*/

                /*endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller = Blog}/{action = Posts}");
                endpoints.MapControllerRoute(
                   name: "blog",
                   pattern: "blog/{*article}",
                   defaults: new { controller = "Blog", action = "Article" });
                endpoints.MapControllerRoute(
                    name: "blog",
                    pattern: "{controller = Blog}/{action=Detail}/{id?}");
                endpoints.MapControllerRoute(
                    name: "edit",
                    pattern: "{controller=Blog}/{action=Posts}/{id?}");*/

            });
        }
    }
}
