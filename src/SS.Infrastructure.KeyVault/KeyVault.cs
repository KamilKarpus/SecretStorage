using Autofac;
using MediatR;
using SS.Infrastructure.KeyVault.Commands;
using SS.Infrastructure.KeyVault.Model;
using SS.Infrastructure.KeyVault.ReadModels;
using System;
using System.Threading.Tasks;

namespace SS.Infrastructure.KeyVault
{
    public class KeyVault : IKeyVault
    {
        public async Task AddKey(Guid resourceId, byte[] key, byte[] iv)
        {
            using (var scope = KeyVaultCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(new AddStorageKeyCommand()
                {
                    IV = iv,
                    Key = key,
                    ResourceId = resourceId
                });
            }
        }

        public async Task<KeyView> GetKeybyId(Guid Id)
        {
            using (var scope = KeyVaultCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(new GetStorageKeyCommand()
                {
                    ResourceId = Id
                });
            }
        }
    }
}
