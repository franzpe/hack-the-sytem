using System.Threading.Tasks;
using API.Core.Models;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly IUserProfileService _userProfileService;

        public UserController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProfile(UserProfile model)
        {
            var result = await _userProfileService.CreateAsync(model);

            return new JsonResult(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProfile(UserProfile model)
        {
            var result = await _userProfileService.UpdateAsync(model);

            return new JsonResult(result);
        }

        [HttpPost("Remove")]
        public async Task<IActionResult> RemoveProfile(string userId)
        {
            var result = await _userProfileService.DeleteAsync(userId);

            return new JsonResult(result);
        }

        [HttpGet("GetVerifiers")]
        public async Task<IActionResult> GetVerifiers()
        {
            var result = await _userProfileService.GetVerifiersAsync();

            return new JsonResult(result);
        }
    }
}
