using System;
using System.Linq;
using ArtContainer.Core.Configuration;
using ArtContainer.Core.Infrastructure.DependencyManagement;
using Autofac;
using Autofac.Extensions.DependencyInjection;
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

        #endregion

        #region Methods
        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration, ArtConfig config)
        {
            //find startup configurations provided by other assemblies
            var typeFinder = new WebAppTypeFinder();

            //register dependencies
            RegisterDependencies(services, typeFinder, config);

            return _serviceProvider;
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

        #endregion
    }
}
