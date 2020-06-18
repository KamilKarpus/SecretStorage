using Autofac;
using SS.Infrastructure.GrantStore;
using SS.Users.Domain.Repositories;
using SS.Users.Infrastructure.AuthGuards;

namespace SS.Users.Infrastructure.Configuration.Auth
{
    public class AuthModule : Autofac.Module
    {
        private readonly string _secretKey;
        private readonly string _connectionString;
        private readonly string _dbName;
        public AuthModule(string secretKey, string connectionString, string dbName)
        {
            _secretKey = secretKey;
            _connectionString = connectionString;
            _dbName = dbName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(r=>
            new GrantStore(_connectionString, _dbName))
            .AsImplementedInterfaces();
            builder.Register<AuthenticateService>(r => {
                var grantStore = r.Resolve<IGrantStore>();
                var userRepo = r.Resolve<IUserRepository>();
                    return new AuthenticateService(_secretKey, grantStore, userRepo);
                 }).AsImplementedInterfaces();
            builder.RegisterType<AuthGuard>()
                .AsImplementedInterfaces();

            base.Load(builder);
        }
    }
}
