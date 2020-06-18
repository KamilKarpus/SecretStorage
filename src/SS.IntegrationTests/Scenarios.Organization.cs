using Newtonsoft.Json;
using SS.IntegrationTests.Model;
using SS.IntegrationTests.Utils;
using SS.Organizations.Application.ReadModels.Organizations;
using SS.Organizations.Domain.Roles;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SS.IntegrationTests
{
    
    [Collection("Scenarios")]
    public class ScenariosOrganization
    {
        private readonly HttpClient _client;
        public ScenariosOrganization(SSFixture fixture)
        {
            _client = fixture.CreateClient();
        }
        private HttpContent CreateContent(object body)
            => new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        private async Task<Guid> RegisterUser(string email)
        {
            var body = CreateContent(new
            {
                email = email,
                password = "Test1234.",
                displayName = "Test User"
            });
            var response = await _client.PostAsync(Users.Post.Register, body);
            response.EnsureSuccessStatusCode();
            var result = await response.CastTo<ResponseWithId>();
            return result.Id;
        }
        private async Task<Guid> CreateOrganization(HttpClient client, string name)
        {
            var organization = CreateContent(
                new
                {
                    name = name
                });
            
            var response = await client.PostAsync(Organization.Post.AddOrganization, organization);
            response.EnsureSuccessStatusCode();
            var organizationId = await response.CastTo<ResponseWithId>();
            return organizationId.Id;
        } 

        [Fact]
        public async Task Add_Organization_Should_Create()
        {
            var client = new HttpClientWithUser(_client);
            await client.Register("test3@test.pl", "Test1234.");
            await client.AddTokenHeader();
            var name = "test12333";
            var organizationId = await CreateOrganization(client.Client,name);
            var organizationResponse = await _client.GetAsync(Organization.Get.GetOrganizationbyId(organizationId));
            organizationResponse.EnsureSuccessStatusCode();
            var organizationInfo = await organizationResponse.CastTo<OrganizationView>();
            Assert.Equal(organizationId, organizationInfo.Id);
            Assert.Equal(name, organizationInfo.Name);
        }
        [Theory]
        [InlineData("")]
        [InlineData("12")]
        [InlineData("sd")]
        public async Task Add_Organization_Should_Return_BadRequest(string name)
        {
            var organization = CreateContent(
                new
                {
                    name = name
                });
            var response = await _client.PostAsync(Organization.Post.AddOrganization, organization);
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task AddUser_To_Organization_Should_Add()
        {
            var client = new HttpClientWithUser(_client);
            await client.Register("test1@test.pl", "Test1234.");
            await client.AddTokenHeader();
            var userEmail = "test@tester.pl";
            var newUserId = await RegisterUser(userEmail);
            var organizationId = await CreateOrganization(client.Client,"janusz");
            await client.RefreshToken();
            var body = CreateContent(new
            {
                email = userEmail
            });
            var response = await client.Client.PostAsync(Organization.Post.AddUserToOrganization(organizationId), body);
            response.EnsureSuccessStatusCode();

            var organizationResponse = await client.Client.GetAsync(Organization.Get.GetOrganizationbyId(organizationId));
            organizationResponse.EnsureSuccessStatusCode();
            var organizationInfo = await organizationResponse.CastTo<OrganizationView>();

            var user = organizationInfo.Users.FirstOrDefault(p => p.Id == newUserId);

            Assert.Equal(newUserId, user.Id);
            Assert.Equal(Role.User.Name, user.Role);

        }

        [Fact]
        public async Task AddUser_To_Organization_BadRequest()
        {
            var userEmail = "hjdhasdhsa";
            var client = new HttpClientWithUser(_client);
            await client.Register("test2@test.pl", "Test1234.");
            await client.AddTokenHeader();
            var organizationId = await CreateOrganization(client.Client, "przemek");
            var body = CreateContent(new
            {
                email = userEmail
            });
            var response = await _client.PostAsync(Organization.Post.AddUserToOrganization(organizationId), body);
            Assert.False(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData(2, "test4@test.pl", "test2@tester.pl", "COMPANY 1")]
        [InlineData(1, "test4.2@test.pl", "test2.2@tester.pl", "COMPANY 2")]
        public async Task ChangeUser_Role_ShouldChange(int roleId, string email, string newEmail, string company)
        {
            var client = new HttpClientWithUser(_client);
            await client.Register(email, "Test1234.");
            await client.AddTokenHeader();
            var organizationId = await CreateOrganization(client.Client, company);
            var userEmail = newEmail;
            var newUserId = await RegisterUser(userEmail);

            await client.RefreshToken();

            var body = CreateContent(new
            {
                email = userEmail
            });
            var response = await client.Client.PostAsync(Organization.Post.AddUserToOrganization(organizationId), body);
            response.EnsureSuccessStatusCode();
            var role = Role.From(roleId);
            var roleBody = CreateContent(new
            {
                roleId = role.Id
            });
            var roleChangeResposne = await client.Client.PutAsync(Organization.Put.ChangeRole(organizationId,newUserId), roleBody);
            roleChangeResposne.EnsureSuccessStatusCode();

            var organizationResponse = await client.Client.GetAsync(Organization.Get.GetOrganizationbyId(organizationId));
            organizationResponse.EnsureSuccessStatusCode();
            var organizationInfo = await organizationResponse.CastTo<OrganizationView>();

            var user = organizationInfo.Users.FirstOrDefault(p => p.Id == newUserId);

            Assert.Equal(role.Name, user.Role);
        }

        [Fact]
        public async Task Change_UserRole_Shoud_BadRequest()
        {
            var client = new HttpClientWithUser(_client);
            await client.Register("assss@trest.pl", "Test1234.");
            await client.AddTokenHeader();
            var organizationId = await CreateOrganization(client.Client, "coommspany");
            var userEmail = "testr88@test.pl";
            var newUserId = await RegisterUser(userEmail);

            await client.RefreshToken();

            var body = CreateContent(new
            {
                email = userEmail
            });
            var response = await client.Client.PostAsync(Organization.Post.AddUserToOrganization(organizationId), body);
            response.EnsureSuccessStatusCode();
            var roleBody = CreateContent(new
            {
                roleId = 4
            });
            var roleChangeResposne = await client.Client.PutAsync(Organization.Put.ChangeRole(organizationId, newUserId), roleBody);
            Assert.False(roleChangeResposne.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_UserFromOrganization_BadRequest()
        {
            var client = new HttpClientWithUser(_client);
            var userId = await client.Register("test18@trest.pl", "Test1234.");
            await client.AddTokenHeader();
            var organizationId = await CreateOrganization(client.Client, "coommspany 18");
            await client.RefreshToken();
            var response = await client.Client.DeleteAsync(Organization.Delete.DeleteUserFromOrganization(organizationId, userId));
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_UserFromOrganization_Should_Delete()
        {
            var client = new HttpClientWithUser(_client);
            await client.Register("test19@trest.pl", "Test1234.");
            await client.AddTokenHeader();
            var organizationId = await CreateOrganization(client.Client, "coommspany 19");
            await client.RefreshToken();

            var userEmail = "testr57@test.pl";
            await RegisterUser(userEmail);
            var body = CreateContent(new
            {
                email = userEmail
            });
            var response = await client.Client.PostAsync(Organization.Post.AddUserToOrganization(organizationId), body);
            response.EnsureSuccessStatusCode();

            var responseWithId = await response.CastTo<ResponseWithId>();


            var responseDelete = await client.Client.DeleteAsync(Organization.Delete.DeleteUserFromOrganization(organizationId, responseWithId.Id));
            Assert.False(responseDelete.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_Organization_Should_Delete()
        {
            var client = new HttpClientWithUser(_client);
            await client.Register("test20@trest.pl", "Test1234.");
            await client.AddTokenHeader();
            var organizationId = await CreateOrganization(client.Client, "coommspany 20");
            await client.RefreshToken();

            var response = await client.Client.DeleteAsync(Organization.Delete.DeleteOrganization(organizationId));

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
