using System;
using ArtContainer.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace ArtContainer.Framework.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        /// <summary>
        /// Configure MVC routing
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void UseArtMvc(this IApplicationBuilder application)
        {
            application.UseMvc(routeBuilder =>
            {
                
            });
        }

    }
}
