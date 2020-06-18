using SS.Organizations.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace SS.Organizations.Application.Configuration.Services
{
    public interface IUserService
    {
        Task<UserShortViewDTO> GetbyEmail(string email);

        Task<UserDTO> GetbyId(Guid Id);
    }
}
