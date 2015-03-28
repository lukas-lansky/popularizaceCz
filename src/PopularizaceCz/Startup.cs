using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using PopularizaceCz.Database.Infrastructure;
using PopularizaceCz.Database;
using PopularizaceCz.Services.Configuration;
using System.Data;
using System.Data.SqlClient;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using PopularizaceCz.Repositories;

namespace PopularizaceCz
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IConfiguration>(isp => this.Configuration);

            services.AddSingleton<IAppConfiguration, Asp5Configuration>();

            services.AddScoped<IDbConnection>(isp => {
                var config = isp.GetService<IAppConfiguration>();
                var sqlConn = new SqlConnection(config.ConnectionString);

                if (!config.Development)
                {
                    return sqlConn;
                }

                return new ProfiledDbConnection(sqlConn, MiniProfiler.Current);
            });

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<ITalkRepository, TalkRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory, IAppConfiguration appConfig)
        {
            loggerfactory.AddConsole();

            if (appConfig.Development)
            {
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                app.UseErrorHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
