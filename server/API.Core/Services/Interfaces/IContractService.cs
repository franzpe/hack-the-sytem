using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core.Services
{
    public interface IContractService
    {
        List<Contract> Get();
        Contract Get(Guid id);
        Task<OrchestratorResult<Contract>> CreateAsync(Contract contract);
        Task<OrchestratorResult<Contract>> AcceptAsync(Contract contract, User user);
        Task<OrchestratorResult<Contract>> FinishAsync(Guid contractId, User user);
        void Update(Guid id, Contract ContractIn);
        Task<OrchestratorResult> CancelAsync(Guid contractId, User user);
        void Remove(Guid id);
    }
}