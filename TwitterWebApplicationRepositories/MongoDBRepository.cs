using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace TwitterWebApplicationRepositories
{
    /// <summary>
    /// This implementation used the MongoDB C# Driver, see
    /// http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial
    /// </summary>
    public class MongoDBRepository<TEntity> where TEntity : class
    {
        // These three classes are supposed to be thread-safe, see 
        // http://www.mongodb.org/display/DOCS/CSharp+Driver+Tutorial#CSharpDriverTutorial-Threadsafety
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<TEntity> _entities;

        string collectionName = "";

        public MongoDBRepository(string connection, string database, string collection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://localhost:27017";
            }


            _client = new MongoClient(connection);
            _server = _client.GetServer();
            _database = _server.GetDatabase(database, WriteConcern.Unacknowledged);

            collectionName = collection;
            _entities = _database.GetCollection<TEntity>(collectionName);
        }

        public IEnumerable<TEntity> GetAllContacts()
        {
            return _entities.FindAll();
        }

        public TEntity GetContact(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            return _entities.Find(query).FirstOrDefault();
        }

        public bool RemoveContact(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            WriteConcernResult result = _entities.Remove(query);
            return result.DocumentsAffected == 1;
        }
    }
}
