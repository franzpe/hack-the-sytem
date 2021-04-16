using API.Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Services
{
    public class ContractService
    {
        private readonly IMongoCollection<Contract> _Contracts;

        public ContractService(IContractstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Contracts = database.GetCollection<Contract>(settings.ContractsCollectionName);
        }

        public List<Contract> Get() =>
            _Contracts.Find(Contract => true).ToList();

        public Contract Get(string id) =>
            _Contracts.Find<Contract>(Contract => Contract.Id == id).FirstOrDefault();

        public Contract Create(Contract Contract)
        {
            _Contracts.InsertOne(Contract);
            return Contract;
        }

        public void Update(string id, Contract ContractIn) =>
            _Contracts.ReplaceOne(Contract => Contract.Id == id, ContractIn);

        public void Remove(Contract ContractIn) =>
            _Contracts.DeleteOne(Contract => Contract.Id == ContractIn.Id);

        public void Remove(string id) =>
            _Contracts.DeleteOne(Contract => Contract.Id == id);
    }
}
