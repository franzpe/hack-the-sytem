using System.Threading.Tasks;
using API.Core.Models;
using API.Core.Orchestrators;
using API.Core.Platform.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DraftController : BaseApiController
    {
        private readonly IDraftOrchestrator _draftOrchestrator;

        public DraftController(IDraftOrchestrator draftOrchestrator)
        {
            _draftOrchestrator = draftOrchestrator;
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDraft(Contract Model)
        {
            var userEmail = User.GetEmail();

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized();
            }

            var result = await _draftOrchestrator.CreateDraft(User.GetEmail(), Model.PartnerEmail, Model.VerifierEmail, Model.ValidUntil,
                Model.Description);

            return Json(result);
        }
    }
}
