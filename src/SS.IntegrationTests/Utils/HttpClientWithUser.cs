using Newtonsoft.Json;
using SS.IntegrationTests.Model;
using SS.Users.Application.ReadModels;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SS.IntegrationTests.Utils
{
    public class HttpClientWithUser
    {
        private readonly HttpClient _client;
        private TokenResponse _tokenCache;
        private string _email;
        private string _password;

        public HttpClient Client => _client;
        public HttpClientWithUser(HttpClient client)
        {
            _client = client;
        }


        public static HttpContent CreateContent(object body)
            => new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        public async Task<Guid> Register(string email, string password)
        {
            _email = email;
            _password = password;
            var body = CreateContent(new
            {
                email = email,
                password = password,
                displayName = "Test User"
            });
            var response = await _client.PostAsync(Users.Post.Register, body);
            response.EnsureSuccessStatusCode();
            return (await response.CastTo<ResponseWithId>()).Id;
        }

        public async Task AddTokenHeader()
        {
            var login = CreateContent(new
            {
                email = _email,
                password = _password,
            }); 
            var loginResponse = await _client.PostAsync(Users.Post.Login, login);
            var result = loginResponse.CastTo<TokenResponse>().Result;
            _tokenCache = result;
            _client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", result.Token);
        }

        public  async Task RefreshToken()
        {
            var refreshToken = CreateContent(new
            {
                token = _tokenCache.RefreshToken
            });
            var responseMessage = await _client.PostAsync(Users.Post.RefreshToken, refreshToken);
            var result = await responseMessage.CastTo<RefreshTokenResponse>();
            _tokenCache.RefreshToken = result.RefreshToken;
            _tokenCache.Token = result.Token;
            _client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", result.Token);
        }
    }
}
