using MongoDB.Driver;
using SS.Common.Mongo.Utils;
using System;
using System.Threading.Tasks;

namespace SS.Infrastructure.GrantStore
{
    public class GrantStore : IGrantStore
    {
        private readonly MongoConnection _mongoConnection;
        public GrantStore(string connectionString, string dbname)
        {
            _mongoConnection = new MongoConnection(connectionString, dbname);
        }

        public async Task AddAsync(Guid userId, string refreshToken)
        {
            var grantModel = new GrantModel()
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                RefreshToken = refreshToken,
                IsValid = true
            };
            var collection = _mongoConnection.GetCollection<GrantModel>("grantmodel");
            await collection.InsertOneAsync(grantModel);
        }
        public async Task<bool> HasUserHaveValidToken(Guid userId)
        {
            var collection = _mongoConnection.GetCollection<GrantModel>("grantmodel");
            var result = await collection.Find<GrantModel>(p => p.OwnerId == userId).FirstOrDefaultAsync();
            return result.IsValid;

        }
        public async Task DeleteUserGrant(Guid userId)
        {
            var collection = _mongoConnection.GetCollection<GrantModel>("grantmodel");
            var result = await collection.Find<GrantModel>(p => p.OwnerId == userId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsValid = false;
                await collection.ReplaceOneAsync<GrantModel>(p => p.OwnerId.Equals(userId), result);
            }
        }

        public async Task UpdateGrantedToken(Guid userId, string newToken)
        {
            var collection = _mongoConnection.GetCollection<GrantModel>("grantmodel");
            var result = await collection.Find<GrantModel>(p => p.OwnerId == userId).FirstOrDefaultAsync();
            result.RefreshToken = newToken;
            result.IsValid = true;
            await collection.ReplaceOneAsync<GrantModel>(p => p.OwnerId.Equals(userId), result);
        }


        public async Task<GrantModel> GetTokenInfo(Guid ownerId)
        {
            var collection = _mongoConnection.GetCollection<GrantModel>("grantmodel");
            return await collection.Find<GrantModel>(p => p.OwnerId == ownerId).FirstOrDefaultAsync();
        }

        public async Task<GrantModel> GetTokenInfo(string refreshToken)
        {
            var collection = _mongoConnection.GetCollection<GrantModel>("grantmodel");
            return await collection.Find<GrantModel>(p => p.RefreshToken == refreshToken).FirstOrDefaultAsync();
        }
    }
}
