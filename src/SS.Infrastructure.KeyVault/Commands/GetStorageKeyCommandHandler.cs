using MediatR;
using SS.Infrastructure.KeyVault.ReadModels;
using SS.Infrastructure.KeyVault.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Infrastructure.KeyVault.Commands
{
    public class GetStorageKeyCommandHandler : IRequestHandler<GetStorageKeyCommand, KeyView>
    {
        private readonly IKeyVaultService _service;
        public GetStorageKeyCommandHandler(IKeyVaultService service)
        {
            _service = service;
        }
        public async Task<KeyView> Handle(GetStorageKeyCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.GetKeyByResourceId(request.ResourceId);
            return new KeyView()
            {
                IV = result.IV,
                Key = result.Key,
                LastRead = result.LastRead,
                ResourceId = result.ResourceId
            };
        }
    }
}
