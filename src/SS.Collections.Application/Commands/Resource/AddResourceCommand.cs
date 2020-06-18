using SS.Collections.Application.Configuration.Commands;
using System;

namespace SS.Collections.Application.Resource
{
    public class AddResourceCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Name { get;  set; }
        public Guid OwnerId { get;  set; }
        public string OwnerName { get;  set; }
        public string Resource { get;  set; }

    }
}
