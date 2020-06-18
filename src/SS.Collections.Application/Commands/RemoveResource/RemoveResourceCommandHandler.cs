using MediatR;
using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Domain.Exceptions;
using SS.Collections.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.Commands.RemoveResource
{
    public class RemoveResourceCommandHandler : ICommandHandler<RemoveResourceCommand>
    {
        private readonly ICollectionRepository _repository;
        public RemoveResourceCommandHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveResourceCommand request, CancellationToken cancellationToken)
        {
            var collection = await _repository.GetbyId(request.CollectionId);
            if(collection == null)
            {
                throw new CollectionException($"Collection with Id = [{request.CollectionId}] not found.", HttpCodes.NotFound, ExceptionCode.CollectionNotFound);
            }
            collection.RemoveResource(request.ResourceId);
            await _repository.UpdateAsync(collection);
            return Unit.Value;
        }
    }
}
