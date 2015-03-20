using System;

namespace PopularizaceCz.Services.Configuration
{
    public interface IAppConfiguration
    {
        bool Development { get; }

        string ConnectionString { get; }
    }
}