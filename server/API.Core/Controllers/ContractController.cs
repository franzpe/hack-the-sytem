using API.Core.Models;
using API.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class ContractsController : ControllerBase
        {
            private readonly ContractService _ContractService;

            public ContractsController(ContractService ContractService)
            {
                _ContractService = ContractService;
            }

            [HttpGet]
            public ActionResult<List<Contract>> Get() =>
                _ContractService.Get();

            [HttpGet("{id:length(24)}", Name = "GetContract")]
            public ActionResult<Contract> Get(string id)
            {
                var Contract = _ContractService.Get(id);

                if (Contract == null)
                {
                    return NotFound();
                }

                return Contract;
            }

            [HttpPost]
            public ActionResult<Contract> Create(Contract Contract)
            {
                _ContractService.Create(Contract);

                return CreatedAtRoute("GetContract", new { id = Contract.Id.ToString() }, Contract);
            }

            [HttpPut("{id:length(24)}")]
            public IActionResult Update(string id, Contract ContractIn)
            {
                var Contract = _ContractService.Get(id);

                if (Contract == null)
                {
                    return NotFound();
                }

                _ContractService.Update(id, ContractIn);

                return NoContent();
            }

            [HttpDelete("{id:length(24)}")]
            public IActionResult Delete(string id)
            {
                var Contract = _ContractService.Get(id);

                if (Contract == null)
                {
                    return NotFound();
                }

                _ContractService.Remove(Contract.Id);

                return NoContent();
            }
        }
}

