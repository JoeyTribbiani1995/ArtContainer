using System;
using ArtContainer.Core.Infrastructure;
using ArtContainer.Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtContainer.Framework.Infrastructure
{
    public class ArtMvcStartup : IArticleStartup
    {
        public int Order => 1000;

        public void Configure(IApplicationBuilder application)
        {
            //MVC routing
            application.UseArtMvc();
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add and configure MVC feature
            services.AddMvc();
        }
    }
}
