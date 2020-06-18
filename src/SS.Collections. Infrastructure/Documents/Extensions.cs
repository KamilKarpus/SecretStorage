using SS.Collections._Infrastructure.Documents.History;
using SS.Collections.Domain;
using SS.Collections.Domain.History;
using SS.Collections.Domain.Users;
using SS.Collections.Infrastructure.Documents.Resources;
using System.Collections.Generic;
using System.Linq;

namespace SS.Collections.Infrastructure.Documents
{
    public static class Extensions
    {
        public static CollectionDocument ToDocument(this Collection collection)
            => new CollectionDocument()
            {
                Id = collection.Id,
                Name = collection.Name,
                OrganizationId = collection.OrganizationId,
                Resources = collection.Resources.Select(p=>p.ToDocument()).ToList()
            };

        public static ResourceDocument ToDocument(this Resource resource)
            => new ResourceDocument()
            {
                Id = resource.Id,
                EditedBy = resource.EditedBy.ToDocument(),
                EditedTime = resource.EditedTime,
                EncryptedResource = resource.EncryptedResource,
                ReadedBy = resource.ReadedBy.ToDocument(),
                Name = resource.Name,
                Owner = resource.Owner.ToDocument(),
                ReadedTime = resource.ReadedTime
            };
        public static LoggedEntityDocument ToDocument(this LoggedEntity loggedEntity)
            => new LoggedEntityDocument()
            {
                Id = loggedEntity.Id,
                DisplayName = loggedEntity.DisplayName
            };

        public static LoggedEntity AsEntity(this LoggedEntityDocument document)
            => new LoggedEntity(document.Id, document.DisplayName);

        public static Resource AsEntity(this ResourceDocument document)
            => new Resource(document.Id, document.Owner.AsEntity(),
                document.ReadedBy.AsEntity(), document.EditedBy.AsEntity(),
                document.ReadedTime, document.EditedTime, document.EncryptedResource,
                document.Name);

        public static Collection AsEntity(this CollectionDocument document)
            => new Collection(document.Id, document.OrganizationId,
                document.Name, document.Resources.Select(p => p.AsEntity()).ToHashSet());

        public static List<Collection> ToEntityList(this List<CollectionDocument> documents)
            => documents.Select(p => p.AsEntity()).ToList();


        public static ResourceLogHistoryDocument ToDocument(this ResourceLogHistory history)
            => new ResourceLogHistoryDocument()
            {
                CollectionId = history.CollectionId,
                ResourceId = history.ResourceId,
                Id = history.Id,
                Logs = history.Logs.Select(p=>p.ToDocument())
                            .ToList(),
                Version = history.Version
            };

        public static LogDocument ToDocument(this Log log)
            => new LogDocument()
            {
                Entity = log.Entity.ToDocument(),
                LogId = log.LogId,
                Status = log.Status.Id,
                Time = log.Time
            };

        public static Log AsEntity(this LogDocument log)
            => new Log(log.Entity.Id, log.Entity.DisplayName, log.Status, log.Time);

        public static ResourceLogHistory AsEntity(this ResourceLogHistoryDocument history)
            => new ResourceLogHistory(history.Id, history.ResourceId,
                history.CollectionId, history.Logs.Select(p => p.AsEntity()).ToHashSet(),history.Version);


    }
}
