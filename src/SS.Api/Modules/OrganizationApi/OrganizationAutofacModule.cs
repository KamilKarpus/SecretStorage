using Autofac;
using SS.Organizations.Infrastructure.Configuration.ModuleExecution;

namespace SS.Api.Modules.OrganizationApi
{
    public class OrganizationAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrganizationModule>()
             .AsImplementedInterfaces();
            base.Load(builder);
            base.Load(builder);
        }
    }
}
