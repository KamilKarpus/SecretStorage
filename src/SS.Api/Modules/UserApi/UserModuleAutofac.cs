using Autofac;
using SS.Users.Application;
using SS.Users.Infrastructure.Configuration.ModuleExecution;

namespace SS.Api.Modules
{
    public class UserModuleAutofac : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserModule>().As<IUserModule>();
            base.Load(builder);
        }
    }
}
