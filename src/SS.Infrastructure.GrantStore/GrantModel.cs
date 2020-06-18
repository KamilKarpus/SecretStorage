

using SS.Common.Mongo;
using System;

namespace SS.Infrastructure.GrantStore
{
    public class GrantModel : DocumentBase<Guid>
    {
        public Guid OwnerId { get; set; }
        public string RefreshToken { get; set; }
        public bool IsValid { get; set; }
    }
}
