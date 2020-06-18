using MediatR;
using System;

namespace SS.Infrastructure.KeyVault.Commands
{
    public class AddStorageKeyCommand : IRequest
    {
        public Guid ResourceId { get; set; }
        public byte[] Key { get; set; }

        public byte[] IV { get; set; }
    }
}
