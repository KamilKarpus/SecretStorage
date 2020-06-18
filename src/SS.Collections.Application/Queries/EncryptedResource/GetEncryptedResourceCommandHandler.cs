using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Application.Configuration.Queries;
using SS.Collections.Application.Cryptography;
using SS.Collections.Application.ReadModels;
using SS.Collections.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.EncryptedResource
{
    internal class GetEncryptedResourceCommandHandler : ICommandHandler<GetEncryptedResourceCommand, EncryptedValue>
    {
        private readonly ICollectionRepository _repository;
        private readonly ICryptographyService _service;
        public GetEncryptedResourceCommandHandler(ICollectionRepository repository, ICryptographyService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<EncryptedValue> Handle(GetEncryptedResourceCommand request, CancellationToken cancellationToken)
        {
            var collection = await _repository.GetbyId(request.CollectionId);
            if(collection != null)
            {
                var resource = collection.ReadResource(request.ResourceId, request.UserId, request.DisplayName);
                var decryptedvalue = await _service.Decrypt(request.ResourceId, resource.EncryptedResource);
                await _repository.UpdateAsync(collection);
                return new EncryptedValue()
                {
                    Value = decryptedvalue
                };
            }
            return new EncryptedValue()
            {
                Value = String.Empty
            };
        }
    }
}
