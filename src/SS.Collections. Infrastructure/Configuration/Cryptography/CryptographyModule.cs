using Autofac;
using SS.Collections._Infrastructure.Cryptography;
using SS.Infrastructure.KeyVault;
using SS.Infrastructure.KeyVault.Startup;
using SS.Rijndael.Cryptography;

namespace SS.Collections._Infrastructure.Configuration.Cryptography
{
    public class CryptographyModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _dbName;
        public CryptographyModule(string connectionsString, string dbName)
        {
            _connectionString = connectionsString;
            _dbName = dbName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RijndealCryptography>()
                .AsImplementedInterfaces();
            KeyVaultStartup.Intilize(_connectionString, _dbName);
            builder.RegisterType<KeyVault>()
                .AsImplementedInterfaces();
            builder.RegisterType<CryptographyService>()
                .AsImplementedInterfaces();
            base.Load(builder);
        }
    }
}
