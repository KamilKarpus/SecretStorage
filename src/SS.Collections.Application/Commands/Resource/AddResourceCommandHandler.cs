using MediatR;
using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Application.Cryptography;
using SS.Collections.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.Resource
{
    internal class AddResourceCommandHandler : ICommandHandler<AddResourceCommand>
    {
        private readonly ICollectionRepository _repository;
        private readonly ICryptographyService _service;
        public AddResourceCommandHandler(ICollectionRepository repository,
            ICryptographyService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<Unit> Handle(AddResourceCommand request, CancellationToken cancellationToken)
        {
            var collection = await _repository.GetbyId(request.CollectionId);
            if(collection != null)
            {
               var source = await  _service.Encrypt(request.Id, request.Resource);
                collection.AddResource(request.Id, request.OwnerId, request.OwnerName,
                    source, request.Name);
                await _repository.UpdateAsync(collection);
            }
            return Unit.Value;
        }
    }
}
