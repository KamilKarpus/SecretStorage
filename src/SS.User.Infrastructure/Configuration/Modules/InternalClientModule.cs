using Autofac;
using SS.Infrastructure.ModuleClient;

namespace SS.Users.Infrastructure.Configuration.Modules
{
    public class InternalClientModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ModuleClient>().AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
