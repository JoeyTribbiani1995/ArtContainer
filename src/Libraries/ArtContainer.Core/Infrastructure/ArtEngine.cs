using System;
using System.Linq;
using ArtContainer.Core.Configuration;
using ArtContainer.Core.Infrastructure.DependencyManagement;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtContainer.Core.Infrastructure
{
    /// <summary>
    /// Represents art engine
    /// </summary>
    public class ArtEngine : IEngine
    {
        #region Properties
        /// <summary>
        /// Gets or sets service provider
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        /// <summary>
        /// Service provider
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        #endregion

        #region Methods
        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration, ArtConfig config)
        {
            //find startup configurations provided by other assemblies
            var typeFinder = new WebAppTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<IArticleStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IArticleStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure services
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);


            //register dependencies
            RegisterDependencies(services, typeFinder, config);

            return _serviceProvider;
        }

        /// <summary>
        /// Configure HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            //find startup configurations provided by other assemblies
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<IArticleStartup>();

            //create and sort instances of startup configurations
            var instances = startupConfigurations
                .Select(startup => (IArticleStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //configure request pipeline
            foreach (var instance in instances)
                instance.Configure(application);
        }

        #endregion

        #region Utilities
        // <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="nopConfig">Nop configuration parameters</param>
        protected virtual IServiceProvider RegisterDependencies(IServiceCollection services, ITypeFinder typeFinder, ArtConfig config)
        {
            var containerBuilder = new ContainerBuilder();

            //register instance object engine
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            //register instance object type finder
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(services);

            //find dependency registrars provided by other assemblies
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //create and sort instances of dependency registrars
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //register all provided dependencies
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, typeFinder, config);

            //create service provider
            _serviceProvider = new AutofacServiceProvider(containerBuilder.Build());

            return _serviceProvider;
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }


        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        public object Resolve(Type type)
        {
            return GetServiceProvider().GetService(type);
        }

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        #endregion
    }
}
