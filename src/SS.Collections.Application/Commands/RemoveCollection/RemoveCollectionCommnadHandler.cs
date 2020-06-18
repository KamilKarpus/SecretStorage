using MediatR;
using SS.Collections.Application.Configuration.Commands;
using SS.Collections.Domain.Exceptions;
using SS.Collections.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Collections.Application.Commands.RemoveCollection
{
    public class RemoveCollectionCommnadHandler : ICommandHandler<RemoveCollectionCommand>
    {
        private readonly ICollectionRepository _repository;
        public RemoveCollectionCommnadHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(RemoveCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection =  await _repository.GetbyId(request.CollectionId);
            if(collection == null)
            {
                throw new CollectionException($"Collection with Id [{request.CollectionId}] not found!", HttpCodes.NotFound, ExceptionCode.CollectionNotFound);    
            }
            collection.PrepareToRemove();
            await _repository.Delete(collection);
            return Unit.Value;
        }
    }
}
