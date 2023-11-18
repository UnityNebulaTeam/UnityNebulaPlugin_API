using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace NebulaPlugin.Application.Mongo
{
    public class MongoItems
    {
        public BsonDocument item { get; set; }
    }
}