using API.Core.Models;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            [HttpPost]
            public async Task<IActionResult> Create(Contract contract)
            {
                var result = await _contractService.CreateAsync(contract);

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
        }
}

