using Newtonsoft.Json;
using SS.IntegrationTests.Model;
using SS.IntegrationTests.Utils;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SS.IntegrationTests
{
    [Collection("Scenarios")]
    public class Scenarios
    {
        private readonly HttpClient _client;
        public Scenarios(SSFixture fixture)
        {
            _client = fixture.CreateClient();
        }
        private HttpContent CreateContent(object body)
            =>new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

       
        [Fact]
        public async Task RegisterUser_Should_Register_New_User()
        {
            var body = CreateContent(new
            {
                email = "test@user.com",
                password = "Test1234.",
                displayName = "Test User"
            });
            var response = await _client.PostAsync(Users.Post.Register, body);
            response.EnsureSuccessStatusCode();
            var result = await response.CastTo<ResponseWithId>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task RegisterUser_Should_Return_EmailError()
        {
            var user1 = CreateContent(new
            {
                email = "test2@user.com",
                password = "Test1234.",
                displayName = "Test User"
            });
            var user2 = CreateContent(new
            {
                email = "test2@user.com",
                password = "Test1234.",
                displayName = "Test User"
            });
            var response = await _client.PostAsync(Users.Post.Register,user1);
            response.EnsureSuccessStatusCode();
            var result = await response.CastTo<ResponseWithId>();
            Assert.NotNull(result);
            var response2 = await _client.PostAsync(Users.Post.Register, user2);
            Assert.Equal(System.Net.HttpStatusCode.UnprocessableEntity, response2.StatusCode);
        }
    }
}
