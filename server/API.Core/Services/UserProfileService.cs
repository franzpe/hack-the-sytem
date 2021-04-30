using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Database;
using API.Core.Models;
using API.Core.Platform.Enums;
using MongoDB.Driver;

namespace API.Core.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IMongoCollection<UserProfile> _userProfiles;

        public UserProfileService(IUserstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userProfiles = database.GetCollection<UserProfile>(settings.UsersCollectionName);
        }

        public async Task<OrchestratorResult<UserProfile>> CreateAsync(UserProfile model)
        {
            var result = new OrchestratorResult<UserProfile>();

            model.Status = UserStatus.Unverified; //TODO: Unverified sa zmeni na active po odkliknuti verifikacneho mailu?
            model.UpdatedOn = DateTime.UtcNow;

            await _userProfiles.InsertOneAsync(model);
            result.Model = model;

            return result;
        }

        public async Task<OrchestratorResult<UserProfile>> UpdateAsync(UserProfile model)
        {
            var result = new OrchestratorResult<UserProfile>();

            var models = await _userProfiles.FindAsync(x => x.Id == model.Id);
            var dbUserProfile = models.SingleOrDefault();

            if (dbUserProfile == null)
            {
                return result.Unauthorized();
            }

            model.UpdatedOn = DateTime.UtcNow;

            await _userProfiles.FindOneAndReplaceAsync(x => x.Id == model.Id, model);
            result.Model = model;

            return result;
        }

        public async Task<OrchestratorResult<UserProfile>> DeleteAsync(string userId)
        {
            var result = new OrchestratorResult<UserProfile>();

            var models = await _userProfiles.FindAsync(x => x.Id == userId);
            var dbUserProfile = models.SingleOrDefault();

            if (dbUserProfile == null)
            {
                return result.Unauthorized();
            }

            dbUserProfile.Status = UserStatus.Deleted;
            dbUserProfile.UpdatedOn = DateTime.UtcNow;

            await _userProfiles.FindOneAndReplaceAsync(x => x.Id == userId, dbUserProfile);
            result.Model = dbUserProfile;

            return result;
        }

        public async Task<OrchestratorResult<List<UserProfile>>> GetVerifiersAsync()
        {
            var result = new OrchestratorResult<List<UserProfile>>();

            var verifierProfiles = await _userProfiles
                .FindAsync(x => x.IsVerifier && x.Status == UserStatus.Active);
            result.Model = await verifierProfiles.ToListAsync();

            return result;
        }
    }
}
