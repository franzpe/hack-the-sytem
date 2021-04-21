namespace API.Core.Database
{
    public interface IContractstoreDatabaseSettings
    {
        string ContractsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
