using SS.Collections.Domain.Users;
using System;

namespace SS.Collections.Domain.History
{
    public class Log
    {
        public Guid LogId { get; private set; }
        public LoggedEntity Entity { get; private set; }
        public Status Status{ get; private set; }
        public DateTime Time { get; private set; }

        public Log(Guid ownerId, string name, int statusId,
            DateTime time)
        {
            LogId = Guid.NewGuid();
            Entity = new LoggedEntity(ownerId, name);
            Status = Status.From(statusId);
            Time = time;
        }
    }
}
