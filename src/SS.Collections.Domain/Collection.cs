using SS.Collections.Domain.BussinessRules;
using SS.Collections.Domain.DomainEvents;
using SS.Collections.Domain.Exceptions;
using SS.Common.BuldingBlocks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SS.Collections.Domain
{
    public class Collection : Entity
    {
        private HashSet<Resource> _resources;
        public Guid Id { get; private set; }
        public Guid OrganizationId { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<Resource> Resources => _resources.ToList();

        public Collection(Guid id, Guid organizationId,string name, HashSet<Resource> resources)
        {
            _resources = resources;
            Id = id;
            OrganizationId = organizationId;
            Name = name;
        }

        public static Collection Create(Guid id, Guid organizationId, string name)
            => new Collection(id, organizationId, name, new HashSet<Resource>());

        public void AddResource(Guid id, Guid ownerId, string displayName, byte[] source, string name)
        {
            CheckRule(new ResourceNameBussinessRule(_resources, name));
            var resource = new Resource(id, ownerId, name, displayName, source);
            
            _resources.Add(resource);
            
            AddDomainEvent(new LoggedEntityCreatedDomainEvent(id, Id, ownerId, displayName, DateTime.Now)); 
        }

        public Resource ReadResource(Guid id, Guid userId, string displayName)
        {
            var readedTime = DateTime.Now;
            var resource = _resources.FirstOrDefault(p => p.Id == id);
            resource.Read(userId,displayName,readedTime);
            AddDomainEvent(new LoggedEntityReadedDomainEvent(id, Id, userId, displayName, readedTime));
            return resource;
        }

        public void EditResource(Guid id, Guid userId, string displayName, byte[] source)
        {
            var edited = DateTime.Now;
            var resource = _resources.FirstOrDefault(p => p.Id == id);
            resource.Edit(userId, displayName, source, edited);
            AddDomainEvent(new LoggedEntityEditedDomainEvent(id, Id,userId, displayName, edited));
        }

        public void RemoveResource(Guid id)
        {
            var resource = _resources.FirstOrDefault(p => p.Id == id);
            if(resource == null)
            {
                throw new ResourceException($"Resource with Id = [{id}] not found.", HttpCodes.NotFound, ExceptionCode.CollectionNotFound);
            }
            _resources.Remove(resource);
            AddDomainEvent(new RemoveResourceDomainEvent(id));
        }

        public void PrepareToRemove()
        {
            AddDomainEvent(new CollectionRemovedDomainEvent(Id));
        }
    }
}
