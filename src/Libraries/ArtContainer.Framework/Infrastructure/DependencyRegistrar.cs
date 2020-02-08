using System;
using ArtContainer.Core.Configuration;
using ArtContainer.Core.Infrastructure;
using ArtContainer.Core.Infrastructure.DependencyManagement;
using Autofac;

namespace ArtContainer.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, ArtConfig config)
        {
            //file provider
            builder.RegisterType<ArtFileProvider>().As<IArtFileProvider>().InstancePerLifetimeScope();
        }
    }
}
