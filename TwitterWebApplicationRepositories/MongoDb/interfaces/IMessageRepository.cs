using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterWebApplicationEntities;

namespace TwitterWebApplicationRepositories.MongoDb.interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<MongoDbMessage> GetAllMessages();

        MongoDbMessage GetMessage(string MessageID);

        MongoDbMessage AddMessage(MongoDbMessage item);

        bool RemoveMessage(string MessageID);

        bool UpdateMessage(string MessageID, MongoDbMessage item);
    }
}
