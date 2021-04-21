using API.Core.Models;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Platform.Security;

namespace API.Core.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class ContractsController : BaseApiController
        {
            private readonly IContractService _contractService;

            public ContractsController(IContractService contractService)
            {
                _contractService = contractService;
            }

            [HttpPost]
            public async Task<IActionResult> Create(Contract contract)
            {
                var result = await _contractService.CreateAsync(contract);

                return Json(result);
            }

            [HttpPost]
            public async Task<IActionResult> Accept(Guid contractId)
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _contractService.AcceptAsync(contractId, userId.GetValueOrDefault());

                return Json(result);
            }

            [HttpPost]
            public async Task<IActionResult> Finish(Guid contractId)
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _contractService.FinishAsync(contractId, userId.GetValueOrDefault());

                return Json(result);
            }

            [HttpPost]
            public async Task<IActionResult> Cancel(Guid contractId)
            {
                var userId = User.GetId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _contractService.CancelAsync(contractId, userId.GetValueOrDefault());

                return Json(result);
            }

        [HttpPut("{id:length(24)}")]
            public IActionResult Update(Guid id, Contract ContractIn)
            {
                var Contract = _contractService.Get(id);

                if (Contract == null)
                {
                    return NotFound();
                }

                _contractService.Update(id, ContractIn);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public IActionResult Delete(Guid id)
            {
                var Contract = _contractService.Get(id);

                if (Contract == null)
                {
                    return NotFound();
                }

                _contractService.Remove(Contract.Id);

                return NoContent();
            }

            [HttpGet]
            public ActionResult<List<Contract>> Get() =>
                _contractService.Get();

            [HttpGet("{id:length(24)}", Name = "GetContract")]
            public ActionResult<Contract> Get(Guid id)
            {
                var Contract = _contractService.Get(id);

                if (Contract == null)
                {
                    return NotFound();
                }

                return Contract;
            }
    }
}

