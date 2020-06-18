
using SS.Users.Application.Configuration.Queries;
using SS.Users.Application.ReadModels;
using SS.Users.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Application.GetUserShortInfo
{
    public class GetUserShortInfobyEmailQueryHandler : IQueryHandler<GetUserShortInfobyEmailQuery, UserShortInfo>
    {
        private readonly IUserRepository _repository;
        public GetUserShortInfobyEmailQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserShortInfo> Handle(GetUserShortInfobyEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetbyEmail(request.Email);
            return new UserShortInfo()
            {
                Id = result.Id,
                DisplayName = result.DisplayName,
                Email = request.Email
            };
        }
    }
}
