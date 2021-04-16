using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core.Models
{
    public class ContractstoreDatabaseSettings : IContractstoreDatabaseSettings
    {
        public string ContractsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
