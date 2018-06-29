using NetCore.FirstStep.Business.Mongo;

namespace NetCore.FirstStep
{
    public class MongoDefaultConfig : IMongoConfig
    {
        public MongoDefaultConfig(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
        }

        public string ConnectionString { get; }
        public string Database { get; }
    }
}
