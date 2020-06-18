using MediatR;
using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Application.Cryptography;
using SS.Collections.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.UpdateResource
{
    internal class UpdateResourceCommandHandler : ICommandHandler<UpdateResourceCommand>
    {
        private readonly ICollectionRepository _repository;
        private readonly ICryptographyService _service;
        public UpdateResourceCommandHandler(ICollectionRepository  repository,
            ICryptographyService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<Unit> Handle(UpdateResourceCommand request, CancellationToken cancellationToken)
        {
            var collection = await _repository.GetbyId(request.CollectionId);
            if(collection != null)
            {
                var encryptedValue = await _service.UpdateValue(request.ResourceId, request.Value);
                collection.EditResource(request.ResourceId, request.UserId, request.DisplayName, encryptedValue);
                await _repository.UpdateAsync(collection);
            }
            return Unit.Value;
        }
    }
}
