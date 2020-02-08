using System;
using ArtContainer.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtContainer.Core.Infrastructure
{
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the various services composing the Nop engine. 
    /// Edit functionality, modules and implementations access most Nop functionality through this interface.
    /// </summary>
    public interface IEngine
    {

        /// <summary>
        /// Add and configure services
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        /// <param name="config">Art configuration parameters</param>
        /// <returns>Service provider</returns>
        IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration, ArtConfig config);

        /// <summary>
        /// Configure HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        void ConfigureRequestPipeline(IApplicationBuilder application);

    }
}
