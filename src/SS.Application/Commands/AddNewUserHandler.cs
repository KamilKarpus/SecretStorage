using MediatR;
using SS.Application.Configuration.Proccesing;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Application.Commands
{
    public class AddNewUserHandler : ICommandHandler<AddUserCommand>
    {
        public Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
