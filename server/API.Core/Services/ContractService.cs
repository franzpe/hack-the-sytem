using System;
using API.Core.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Platform.Enums;

namespace API.Core.Services
{
    public class ContractService : IContractService
    {
        private readonly IMongoCollection<Contract> _contracts;

        public ContractService(IContractstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _contracts = database.GetCollection<Contract>(settings.ContractsCollectionName);
        }

        public List<Contract> Get() =>
            _contracts.Find(contract => true).ToList();

        public Contract Get(Guid id) =>
            _contracts.Find(contract => contract.Id == id).FirstOrDefault();

        public async Task<OrchestratorResult<Contract>> CreateAsync(Contract contract)
        {
            var result = new OrchestratorResult<Contract>();

            contract.Status = ContractStatus.Created;
            contract.UpdatedOn = DateTime.UtcNow;

            await _contracts.InsertOneAsync(contract);
            result.Model = contract;

            return result;
        }

        public async Task<OrchestratorResult<Contract>> AcceptAsync(Contract contract, User user)
        {
            var result = new OrchestratorResult<Contract>();
            
            if (user.Id == contract.VerifierId)
            {
                contract.VerifierAccepted = true;
            }
            else if (user.Id == contract.SellerId || user.Id == contract.BuyerId)
            {
                contract.PartnerAccepted = true;
            }
            else
            {
                return result.Unauthorized();
            }

            if (contract.PartnerAccepted && contract.VerifierAccepted)
            {
                contract.Status = ContractStatus.Accepted;
            }
            
            contract.UpdatedOn = DateTime.UtcNow;

            await _contracts.ReplaceOneAsync(c => c.Id == contract.Id, contract);

            result.Model = contract;

            return result;
        }

        public async Task<OrchestratorResult<Contract>> FinishAsync(Guid contractId, User user)
        {
            var result = new OrchestratorResult<Contract>();

            var contracts = await _contracts.FindAsync(c => c.Id == contractId);
            if (!contracts.Any())
            {
                return result.Error("Contract not found.");
            }

            var contract = contracts.First();

            if (user.Id != contract.VerifierId)
            {
                return result.Unauthorized();
            }

            if (contract.PartnerAccepted && contract.VerifierAccepted)
            {
                contract.Status = ContractStatus.VerifierAccepted;
            }

            //TODO: vykonanie samotnej tranzakcie
            //contract.Status = ContractStatus.Finished;

            contract.UpdatedOn = DateTime.UtcNow;

            await _contracts.ReplaceOneAsync(c => c.Id == contract.Id, contract);

            result.Model = contract;

            return result;
        }

        public void Update(Guid id, Contract ContractIn) =>
            _contracts.ReplaceOne(Contract => Contract.Id == id, ContractIn);

        public async Task<OrchestratorResult> CancelAsync(Guid contractId, User user)
        {
            var result = new OrchestratorResult();
            var contracts = await _contracts.FindAsync(c => c.Id == contractId);
            if (!contracts.Any())
            {
                return result.Error("Contract not found.");
            }

            var contract = contracts.First();
            if (contract.BuyerId != user.Id)
            {
                return result.Unauthorized();
            }

            //TODO: vykonanie tranzakcie, kde sa lumeny poslu s5 buyerovi a odrata sa manipulacny poplatok
            //contract.Status = ContractStatus.Canceled;

            await _contracts.ReplaceOneAsync(c => c.Id == contract.Id, contract);

            return result.Success();
        }

        public void Remove(Guid id) =>
            _contracts.DeleteOne(Contract => Contract.Id == id);
    }
}
