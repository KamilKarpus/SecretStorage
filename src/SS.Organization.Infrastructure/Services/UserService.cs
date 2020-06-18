using SS.Infrastructure.ModuleClient;
using SS.Organizations.Application.Configuration.Services;
using SS.Organizations.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace SS.Organizations.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private string BasePath = "internal/users";

        private readonly IModuleClient _client;
        public UserService(IModuleClient client)
        {
            _client = client;
        }
        public async Task<UserShortViewDTO> GetbyEmail(string email)
          =>  await _client.GetAsync<UserShortViewDTO>(BasePath, String.Empty, new UserShortViewRequestDto() { Email = email });

        public async Task<UserDTO> GetbyId(Guid Id)
        => await _client.GetAsync<UserDTO>(BasePath, "Info", new UserRequestDTO(Id));
    }
}
