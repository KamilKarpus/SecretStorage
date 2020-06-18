using Autofac;
using SS.Collections.Infrastructure;

namespace SS.Api.Modules
{
    public class CollectionsModuleAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CollectionModule>()
                .AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
