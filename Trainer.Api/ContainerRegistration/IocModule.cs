using System;
using Autofac;
using Trainer.Infrastructure;

namespace trainer.api.ContainerRegistration
{
    public static class AutofacContainerExtensions
    {
        public static IServiceProvider BuildContainer(this ContainerBuilder builder)
        {
            builder.RegisterModule<IocModule>();

            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }
    }

    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDbContext>().As<MongoDbContext>();
            
            builder.RegisterAssemblyTypes(typeof(MongoDbContext).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }
    }
}