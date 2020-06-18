using Autofac;
using SS.Users.Infrastructure.UserSecurity.Passwords;

namespace SS.Users.Infrastructure.Configuration.Security
{
    public class SecurityModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<PasswordHasher>(r =>
            {
                return new PasswordHasher(new HashingOptions());
            }).AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
