using MediatR;
using SS.Infrastructure.KeyVault.ReadModels;
using System;

namespace SS.Infrastructure.KeyVault.Commands
{
    public class GetStorageKeyCommand : IRequest<KeyView>
    {
        public Guid ResourceId { get; set; }
    }
}
