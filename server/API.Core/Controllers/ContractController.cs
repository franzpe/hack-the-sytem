using System;
using API.Core.Models;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Core.Platform.Security;
using Microsoft.AspNetCore.Authorization;

namespace API.Core.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class ContractController : BaseApiController
        {
            private readonly IContractService _contractService;

            public ContractController(IContractService contractService)
            {
                _contractService = contractService;
            }

            [HttpPost("Create")]
            [AllowAnonymous]
            public async Task<IActionResult> Create(Contract contract)
            {
                var dateTime = DateTime.UtcNow;
                var result = await _contractService.CreateAsync(contract);

                return Json(result);
            }

            [HttpPost]
            public async Task<IActionResult> Accept(string contractId)
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _contractService.AcceptAsync(contractId, userId);

                return Json(result);
            }

            [HttpPost]
            public async Task<IActionResult> Finish(string contractId)
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _contractService.FinishAsync(contractId, userId);

                return Json(result);
            }

            [HttpPost]
            public async Task<IActionResult> Cancel(string contractId)
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _contractService.CancelAsync(contractId, userId);

                return Json(result);
            }

        //[HttpPut("{id:length(24)}")]
        //    public IActionResult Update(Guid id, Contract ContractIn)
        //    {
        //        var Contract = _contractService.Get(id);

        //        if (Contract == null)
        //        {
        //            return NotFound();
        //        }

        //        _contractService.Update(id, ContractIn);

        //        return NoContent();
        //    }

        //    [HttpDelete("{id:length(24)}")]
        //    public IActionResult Delete(Guid id)
        //    {
        //        var Contract = _contractService.Get(id);

        //        if (Contract == null)
        //        {
        //            return NotFound();
        //        }

        //        _contractService.Remove(Contract.Id);

        //        return NoContent();
        //    }

        //    [HttpGet]
        //    public ActionResult<List<Contract>> Get() =>
        //        _contractService.Get();

        //    [HttpGet("{id:length(24)}", Name = "GetContract")]
        //    public ActionResult<Contract> Get(Guid id)
        //    {
        //        var Contract = _contractService.Get(id);

        //        if (Contract == null)
        //        {
        //            return NotFound();
        //        }

        //        return Contract;
        //    }
    }
}

