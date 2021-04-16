namespace API.Core.Models
{
    public interface IContractstoreDatabaseSettings
    {
        string ContractsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
