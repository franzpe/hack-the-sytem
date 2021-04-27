using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Database;
using API.Core.Models;
using API.Core.Platform.Enums;
using MongoDB.Driver;

namespace API.Core.Services
{
    public interface IUserProfileService
    {
        Task<OrchestratorResult<UserProfile>> CreateAsync(UserProfile userProfile);
        Task<OrchestratorResult<UserProfile>> UpdateAsync(UserProfile userProfile);
        Task<OrchestratorResult<UserProfile>> DeleteAsync(Guid userId);
        Task<OrchestratorResult<List<UserProfile>>> GetVerifiersAsync();
    }

    public class UserProfileService : IUserProfileService
    {
        private readonly IMongoCollection<UserProfile> _userProfiles;

        public UserProfileService(IUserstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userProfiles = database.GetCollection<UserProfile>(settings.UsersCollectionName);
        }

        public async Task<OrchestratorResult<UserProfile>> CreateAsync(UserProfile userProfile)
        {
            var result = new OrchestratorResult<UserProfile>();

            userProfile.Status = UserStatus.Unverified; //TODO: Unverified sa zmeni na active po odkliknuti verifikacneho mailu?
            userProfile.UpdatedOn = DateTime.UtcNow;

            await _userProfiles.InsertOneAsync(userProfile);
            result.Model = userProfile;

            return result;
        }

        public async Task<OrchestratorResult<UserProfile>> UpdateAsync(UserProfile userProfile)
        {
            var result = new OrchestratorResult<UserProfile>();

            var userProfiles = await _userProfiles.FindAsync(x => x.UserId == userProfile.UserId);
            var dbUserProfile = userProfiles.SingleOrDefault();

            if (dbUserProfile == null)
            {
                return result.Unauthorized();
            }

            userProfile.UpdatedOn = DateTime.UtcNow;

            await _userProfiles.FindOneAndReplaceAsync(x => x.UserId == userProfile.UserId, userProfile);
            result.Model = userProfile;

            return result;
        }

        public async Task<OrchestratorResult<UserProfile>> DeleteAsync(Guid userId)
        {
            var result = new OrchestratorResult<UserProfile>();

            var userProfiles = await _userProfiles.FindAsync(x => x.UserId == userId);
            var dbUserProfile = userProfiles.SingleOrDefault();

            if (dbUserProfile == null)
            {
                return result.Unauthorized();
            }

            dbUserProfile.Status = UserStatus.Deleted;
            dbUserProfile.UpdatedOn = DateTime.UtcNow;

            await _userProfiles.FindOneAndReplaceAsync(x => x.UserId == userId, dbUserProfile);
            result.Model = dbUserProfile;

            return result;
        }

        public async Task<OrchestratorResult<List<UserProfile>>> GetVerifiersAsync()
        {
            var result = new OrchestratorResult<List<UserProfile>>();

            var verifierProfiles = await _userProfiles.FindAsync(x => x.IsVerifier);
            result.Model = await verifierProfiles.ToListAsync();

            return result;
        }
    }
}
