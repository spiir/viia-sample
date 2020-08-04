using Aiia.Sample.Helpers;
using Autofac;
using DbUp.Engine.Output;
using Microsoft.AspNetCore.Hosting;

namespace Aiia.Sample.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseStartupFilter>().As<IStartupFilter>().InstancePerDependency();
            builder.RegisterType<DbLogger<DatabaseStartupFilter>>().AsSelf().SingleInstance();
        }
    }
}
