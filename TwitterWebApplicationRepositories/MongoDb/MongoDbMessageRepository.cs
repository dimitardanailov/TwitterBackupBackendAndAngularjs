using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterWebApplicationEntities;
using TwitterWebApplicationRepositories;
using TwitterWebApplicationRepositories.MongoDb.interfaces;

namespace TwitterWebApplicationRepositories
{
    public class MongoDbMessageRepository : MongoDbRepository<MongoDbMessage>, IMessageRepository
    {
        public const string collectionName = "Messages";

        public MongoDbMessageRepository() : base(collectionName)
        {

        }

        public IEnumerable<MongoDbMessage> GetAllMessages()
        {
            return this.GetAllEntities();
        }

        public MongoDbMessage GetMessage(string MessageID)
        {
            return this.GetEntity(MessageID);
        }

        public MongoDbMessage AddMessage(MongoDbMessage item)
        {
            item.CreatedAt = DateTime.UtcNow;
            _entities.Insert(item);

            return item;
        }

        public bool RemoveMessage(string MessageID)
        {
            return this.RemoveEntity(MessageID);
        }

        public bool UpdateMessage(string MessageID, MongoDbMessage item)
        {
            IMongoQuery query = Query.EQ("_id", MessageID);
            item.UpdatedAt = DateTime.UtcNow;
            IMongoUpdate update = Update
                .Set("Text", item.Text);
            WriteConcernResult result = _entities.Update(query, update);

            return result.UpdatedExisting;
        }
    }
}
