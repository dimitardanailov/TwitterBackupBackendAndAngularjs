using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConfigurations
{
    public class DatabaseSettings
    {
        public const string ConnectionString = "DefaultConnection";

        public readonly static string MongoDBServerLocation = AppSettings.Setting<string>("MongoDBServerLocation");
        public readonly static string MongoDBDatabase = AppSettings.Setting<string>("MongoDBDatabase");
    }
}
