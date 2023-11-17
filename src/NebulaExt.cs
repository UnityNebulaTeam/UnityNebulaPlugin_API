using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NebulaPlugin.Application
{
    public static class NebulaExt
    {
        public static List<string> GetAllCollectionNames(this MongoClient client, BsonDocument doc)
        {
            return client.GetDatabase(doc["name"].AsString).ListCollectionNames().ToList();
        }
    }
}