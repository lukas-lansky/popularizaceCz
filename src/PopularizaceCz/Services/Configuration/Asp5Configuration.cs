using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using System;
using PopularizaceCz.Helpers;

namespace PopularizaceCz.Services.Configuration
{
    /// <summary>
    /// Typed class for access to configuration with sensible fallback.
    /// </summary>
    public sealed class Asp5Configuration : IAppConfiguration
    {
        private IConfiguration _config;

        public Asp5Configuration(IConfiguration config)
        {
            this._config = config;
        }

        public string ConnectionString
        {
            get
            {
                var cs = this._config.Get("Data.DefaultConnection.ConnectionString");

                if (cs.IsNullOrWhitespace())
                {
                    throw new Exception("Configuration lacks default connection string.");
                }

                return cs;
            }
        }

        public bool Development
        {
            get
            {
                return this._config.Get("Development").ToLower().Trim() == "true";
                    // fallbacks to false
            }
        }
    }
}