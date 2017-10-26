using ASP.NET_Core_on_Framework.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;

namespace ASP.NET_Core_on_Framework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                var conn = DbProviderFactories.GetFactory(Configuration["ConnectionStrings:providerName"]).CreateConnection();
                conn.ConnectionString = Configuration["ConnectionStrings:DefaultConnection"];
                return new MySqlDbContext(conn, true);
            });
            //services.AddScoped(sp => new MySqlDbContext(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (MySqlDbContext contextDB = new MySqlDbContext(connection, false))
                {
                    if (contextDB.Database.CreateIfNotExists())
                    {
                        var dongnidamu = new Student() {Name= "东尼大木", Teachers=new List<Teacher>()};
                        var cangjingkong = new Teacher() { Name = "苍井空", Students = new List<Student>() };
                        dongnidamu.Teachers.Add(cangjingkong);
                        cangjingkong.Students.Add(dongnidamu);
                        contextDB.Students.Add(dongnidamu);
                        contextDB.Teachers.Add(cangjingkong);
                        contextDB.SaveChanges();
                    }
                    
                }
            }
        }
    }
}
