using SS.Collections.Domain.DomainEvents;
using SS.Collections.Domain.Users;
using SS.Common.BuldingBlocks;
using System;

namespace SS.Collections.Domain
{
    public class Resource
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public LoggedEntity Owner {get; private set; }
        public byte[] EncryptedResource { get; private set; }
        public DateTime ReadedTime { get; private set; }
        public LoggedEntity ReadedBy { get; private set; }
        public DateTime EditedTime { get; private set; }
        public LoggedEntity EditedBy { get; private set; }

        public Resource(Guid id, Guid ownerId, string name, string displayName, byte[] source)
        {
            Id = id;
            Owner = new LoggedEntity(ownerId, displayName);
            EncryptedResource = source;
            ReadedBy = new LoggedEntity(ownerId, displayName);
            EditedBy = new LoggedEntity(ownerId, displayName);
            ReadedTime = DateTime.Now;
            EditedTime = DateTime.Now;
            Name = name;
        }
        public Resource(Guid id, LoggedEntity owner, LoggedEntity readedBy,
            LoggedEntity editedBy, DateTime readed, DateTime edited, byte[] source, string name)
        {
            Id = id;
            Owner = owner;
            ReadedBy = readedBy;
            EditedBy = editedBy;
            ReadedTime = readed;
            EditedTime = edited;
            EncryptedResource = source;
            Name = name;
        }

        public void Read(Guid userId, string name, DateTime readedDate)
        {
            ReadedTime = readedDate;
            ReadedBy = new LoggedEntity(userId, name);

        }

        public void Edit(Guid userId, string name, byte[] resource, DateTime edited)
        {
            Read(userId,name,edited);
            EditedBy = new LoggedEntity(userId, name);
            EncryptedResource = resource;
        }
    }
}
