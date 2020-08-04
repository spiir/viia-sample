using Aiia.Sample.Repositories;
using Autofac;

namespace Aiia.Sample.Modules
{
    internal class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UsersRepository>().As<IUsersRepository>().SingleInstance();
        }
    }
}
