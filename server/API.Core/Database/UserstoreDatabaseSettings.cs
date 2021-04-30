namespace API.Core.Database
{
    public class UserstoreDatabaseSettings : IUserstoreDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
