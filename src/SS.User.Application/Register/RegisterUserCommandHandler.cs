using MediatR;
using SS.Users.Application.Configuration.Commands;
using System.Threading;
using System.Threading.Tasks;
using SS.Common.Mongo;
using System;
using SS.Users.Domain;
using SS.Users.Domain.Security;
using SS.Users.Domain.Repositories;
using SS.Users.Domain.Exceptions;

namespace SS.Users.Application.Register
{
    public class RegisterUserCommandHandler : ICommandHandler<UserRegisterCommand>
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _hasher;
        public RegisterUserCommandHandler(IUserRepository repository, IPasswordHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }
        public async Task<Unit> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetbyEmail(request.Email);
            if(user != null)
            {
                throw new UserException("This email is already taken",HttpCodes.EmailIsTaken, ErrorCodes.EmailIsTaken);
            }
            var hashedPassword = _hasher.Hash(request.Password);
            await _repository.AddAsync(User.Create(request.Id,request.Email,hashedPassword,request.DisplayName));
            return Unit.Value;
        }
    }
}
