using Autofac;

namespace SS.Application.Configuration.Module
{
    public class RijandealAufacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RijandealModule>().As<IRijandealModule>();
        }
    }
}
