using Autofac;
using SS.Infrastructure.ModuleClient;

namespace SS.Organizations.Infrastructure.Configuration.InternalClient
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
