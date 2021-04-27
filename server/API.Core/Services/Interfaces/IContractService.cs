using System;
using System.Threading.Tasks;
using API.Core.Models;

namespace API.Core.Services
{
    public interface IContractService
    {
        Task<OrchestratorResult<Contract>> CreateAsync(Contract contract);
        Task<OrchestratorResult<Contract>> AcceptAsync(string contractId, Guid userId);
        Task<OrchestratorResult<Contract>> FinishAsync(string contractId, Guid userId);
        Task<OrchestratorResult> CancelAsync(string contractId, Guid userId);
        //void Update(Guid id, Contract ContractIn);
        //void Remove(Guid id);
        //List<Contract> Get();
        //Contract Get(Guid id);
    }
}