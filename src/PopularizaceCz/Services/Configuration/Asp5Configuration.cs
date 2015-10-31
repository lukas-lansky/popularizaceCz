using System;
using PopularizaceCz.Helpers;
using Microsoft.Framework.Configuration;

namespace PopularizaceCz.Services.Configuration
{
    /// <summary>
    /// Configuration provider for IAppConfiguration taking it's data
    /// from the default (non-typed) ASP.NET 5 configuration infrastructure
    /// (Microsoft.Framework.ConfigurationModel.IConfiguration.)
    /// Configuration of sources for this configuration can be found in Startup.cs.
    /// </summary>
    public sealed class Asp5Configuration : IAppConfiguration
    {
        private IConfiguration _config;

        public Asp5Configuration(IConfiguration config)
        {
            this._config = config;
        }

        /// <summary>
        /// The application connection string for its DB.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                var cs = this._config["ConnectionString"];

                if (cs.IsNullOrWhitespace())
                {
                    throw new Exception("Configuration lacks default connection string.");
                }

                return cs;
            }
        }

        /// <summary>
        /// General switch that governs whether application leaks development
        /// info to UI. It fallbacks to false and that's a good thing.
        /// 
        /// It also can and should be used by composition root to select
        /// services with respect to their side-effects, e.g. IMailer should
        /// be something like SmtpMailer for production, but SingleDestinationSmtpMailer
        /// for development as we really do not want to spam users with unfinished
        /// newsletters.
        /// </summary>
        public bool Development
        {
            get
            {
                return this._config["Development"].ToLower().Trim() == "true";
            }
        }
    }
}