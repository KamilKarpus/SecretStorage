using System;

namespace SS.Infrastructure.KeyVault.ReadModels
{
    public class KeyView
    {
        public Guid ResourceId { get;  set; }
        public byte[] Key { get;  set; }

        public byte[] IV { get; set; }
        public DateTime LastRead { get; set; }
    }
}
