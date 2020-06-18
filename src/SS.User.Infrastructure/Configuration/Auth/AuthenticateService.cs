using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SS.Infrastructure.GrantStore;
using SS.Users.Application.Authenticate;
using SS.Users.Application.ReadModels;
using SS.Users.Domain;
using SS.Users.Domain.Exceptions;
using SS.Users.Domain.Repositories;
using SS.Users.Infrastructure.Configuration.Auth.Claims;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SS.Users.Infrastructure.Configuration.Auth
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly string _SecretKey;
        private readonly IGrantStore _store;
        private readonly IUserRepository _repository;
        public AuthenticateService(string secretKey, IGrantStore store,
            IUserRepository repository)
        {
            _SecretKey = secretKey;
            _store = store;
            _repository = repository;
        }
        public async Task<TokenResponse> GenerateCreditinial(User user)
        {
            var createdToken = CreateToken(user);
            var refreshToken = CreateRefreshToken();
            var grantInfo = await _store.GetTokenInfo(user.Id);
            if (grantInfo == null)
            {
                await _store.AddAsync(user.Id, refreshToken);
            }
            else
            {
                await _store.UpdateGrantedToken(user.Id, refreshToken);
            }

            return new TokenResponse()
            {
                DisplayName = user.DisplayName,
                Token = createdToken,
                Email = user.Email,
                RefreshToken = refreshToken
            };

        }
        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(UserClaims.Id, user.Id.ToString()),
                    new Claim(UserClaims.Email, user.Email),
                    new Claim(UserClaims.DisplayName, user.DisplayName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            foreach (var organization in user.Organizations)
            {
                foreach (var scopes in organization.Claims)
                {
                    var claim = new Claim(organization.Id.ToString(), scopes);
                    tokenDescriptor.Subject.AddClaim(claim);
                }
            }

            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
        public async Task<RefreshTokenResponse> RefreshToken(string refreshToken)
        {
            var grantInfo = await _store.GetTokenInfo(refreshToken);
            if(grantInfo == null)
            {
                throw new UserException("Token cannot be refreshed", HttpCodes.NotAllowed, ErrorCodes.TokenCannotBeRefreshed);
            }
            var userInfo = await _repository.GetbyId(grantInfo.OwnerId);
            var token = CreateToken(userInfo);
            var newRefreshToken = CreateRefreshToken();
            await _store.UpdateGrantedToken(userInfo.Id, newRefreshToken);
            return new RefreshTokenResponse()
            {
                RefreshToken = newRefreshToken,
                Token = token
            };
        }

        private string CreateRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
