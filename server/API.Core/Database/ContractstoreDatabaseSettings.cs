namespace API.Core.Database
{
    public class ContractstoreDatabaseSettings : IContractstoreDatabaseSettings
    {
        public string ContractsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
