using ClientConfigurations;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterWebApplicationRepositories
{
    /// <summary>
    /// This implementation used the MongoDB C# Driver, see
    /// http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial
    /// </summary>
    public class MongoDbRepository<TEntity> where TEntity : class
    {
        // These three classes are supposed to be thread-safe, see 
        // http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial#CSharpDriverTutorial-Threadsafety
        protected MongoClient _client;
        protected MongoServer _server;
        protected MongoDatabase _database;
        protected MongoCollection<TEntity> _entities;

        public MongoDbRepository(string collection)
        {
            _client = new MongoClient(DatabaseSettings.MongoDBServerLocation);
            // Get a reference to a server object from the Mongo client 
            _server = _client.GetServer();
            _database = _server.GetDatabase(DatabaseSettings.MongoDBDatabase, WriteConcern.Unacknowledged);

            _entities = _database.GetCollection<TEntity>(collection);
        }

        public IEnumerable<TEntity> GetAllEntities()
        {
            return _entities.FindAll();
        }

        public TEntity GetEntity(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);

            return _entities.Find(query).FirstOrDefault();
        }

        public bool RemoveEntity(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            WriteConcernResult result = _entities.Remove(query);

            return result.DocumentsAffected == 1;
        }
    }
}
