﻿using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using PopularizaceCz.Services.Configuration;
using System.Data;
using System.Data.SqlClient;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using PopularizaceCz.Repositories;
using PopularizaceCz.Services.ICalExport;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Runtime;

namespace PopularizaceCz
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            //if (env.IsDevelopment())
            //{
            //    // This reads the configuration keys from the secret store.
            //    // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
            //    builder.AddUserSecrets();
            //}

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
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

			services.AddScoped<IICalExporter, DDayICalExporter>();

            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<ITalkRepository, TalkRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory, IAppConfiguration appConfig)
        {
            loggerfactory.AddConsole();

            if (appConfig.Development)
            {
                app.UseErrorPage();
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
