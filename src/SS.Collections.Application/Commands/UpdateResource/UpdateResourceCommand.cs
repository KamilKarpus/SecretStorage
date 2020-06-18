using SS.Collections.Application.Configuration.Commands;
using System;


namespace SS.Collections.Application.UpdateResource
{
    public class UpdateResourceCommand : ICommand
    {
        public Guid CollectionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public Guid UserId { get; set; }
    }
}
