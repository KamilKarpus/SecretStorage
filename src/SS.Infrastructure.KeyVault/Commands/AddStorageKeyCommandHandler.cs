using MediatR;
using SS.Infrastructure.KeyVault.Services;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Infrastructure.KeyVault.Commands
{
    public class AddStorageKeyCommandHandler : IRequestHandler<AddStorageKeyCommand>
    {
        private readonly IKeyVaultService _service;
        public AddStorageKeyCommandHandler(IKeyVaultService service)
        {
            _service = service;
        }
        public async Task<Unit> Handle(AddStorageKeyCommand request, CancellationToken cancellationToken)
        {
            await _service.AddKeyAsync(new Model.StorageKey(request.ResourceId, request.Key, request.IV));
            return Unit.Value;
        }
    }
}
