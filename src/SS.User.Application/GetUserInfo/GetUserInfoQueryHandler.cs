using SS.Users.Application.Configuration.Queries;
using SS.Users.Application.ReadModels;
using SS.Users.Domain.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SS.Users.Application.GetUserInfo
{
    public class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, UserInfo>
    {
        private readonly IUserRepository _repository;
        public GetUserInfoQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserInfo> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetbyId(request.Id);
            return new UserInfo()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Organizations = user.Organizations.Select(p => new OrganizationInfo() { Id = p.Id, Claims = p.Claims }).ToList()
            };
        }
    }
}
