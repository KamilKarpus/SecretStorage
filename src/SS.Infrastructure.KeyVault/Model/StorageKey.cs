using SS.Common.Mongo;
using System;

namespace SS.Infrastructure.KeyVault.Model
{
    public class StorageKey : DocumentBase<Guid>
    {
        public Guid ResourceId { get; private set; }
        public byte[] Key { get; private set; }

        public byte[] IV { get; private set; }
        public DateTime LastRead { get; private set; }

        public StorageKey(Guid resourceId, byte[] key, byte[] iv)
        {
            Id = Guid.NewGuid();
            Key = key;
            IV = iv;
            ResourceId = resourceId;
            LastRead = DateTime.Now;
        }
    }
}
