using System;

namespace PopularizaceCz.Services.Configuration
{
    /// <summary>
    /// Typed interface for access to configuration with sensible fallback.
    /// </summary>
    public interface IAppConfiguration
    {
        bool Development { get; }

        string ConnectionString { get; }
    }
}