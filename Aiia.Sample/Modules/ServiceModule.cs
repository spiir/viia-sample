using Aiia.Sample.Services;
using Autofac;

namespace Aiia.Sample.Modules
{
    internal class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersService>().As<IUsersService>().SingleInstance();
        }
    }
}
