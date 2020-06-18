using MediatR;
using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Domain;
using SS.Collections.Domain.Exceptions;
using SS.Collections.Domain.Repositories;
using SS.Common.BuldingBlocks;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.AddCollection
{
    internal class AddCollectionCommandHandler : ICommandHandler<AddCollectionCommand>
    {
        private readonly ICollectionRepository _repository;
        public AddCollectionCommandHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(AddCollectionCommand request, CancellationToken cancellationToken)
        {
            var collectionName = await _repository.GetbyName(request.Name);
            if(collectionName != null)
            {
                throw new CollectionException($"[{collectionName.Name}] is already taken.", HttpCodes.Conflict, ExceptionCode.CollestionNameIsTaken);
            }
            var collection = Collection.Create(request.CollectionId, request.OrganizationId, request.Name);
            await _repository.AddAsync(collection);
            return Unit.Value;
        }
    }

}
