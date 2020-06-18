using SS.Users.Application.ReadModels;
using SS.Users.Domain;
using System.Threading.Tasks;

namespace SS.Users.Application.Authenticate
{
    public interface IAuthenticateService
    {
        Task<TokenResponse> GenerateCreditinial(User user);

        Task<RefreshTokenResponse> RefreshToken(string refreshToken);
    }
}
