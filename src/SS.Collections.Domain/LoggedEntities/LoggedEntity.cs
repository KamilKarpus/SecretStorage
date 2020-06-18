using System;

namespace SS.Collections.Domain.Users
{
    public class LoggedEntity
    {
        public Guid Id { get; private set; }
        public string DisplayName {get; private set;}

        public LoggedEntity(Guid id, string name)
        {
            Id = id;
            DisplayName = name;
        }
    }
}
