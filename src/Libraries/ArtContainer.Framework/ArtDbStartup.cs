using System;
using ArtContainer.Core.Infrastructure;
using ArtContainer.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtContainer.Framework
{
    public class ArtDbStartup : IArticleStartup
    {
        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 10;


        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>

        public void Configure(IApplicationBuilder application)
        {
            
        }

        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddArtObjectContext();
        }
    }
}
