using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core.Services
{
    public interface IUserProfileService
    {
        Task<OrchestratorResult<UserProfile>> CreateAsync(UserProfile model);
        Task<OrchestratorResult<UserProfile>> UpdateAsync(UserProfile model);
        Task<OrchestratorResult<UserProfile>> DeleteAsync(string userId);
        Task<OrchestratorResult<List<UserProfile>>> GetVerifiersAsync();
    }
}