using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace NebulaPlugin.Application
{
    public abstract class DatabaseManager
    {
        public abstract Task<bool> CreateDatabase(string dbName);
        public abstract Task<bool> CreateTable(string dbName, string tableName);
        public abstract Task<bool> CreateItem(string dbName, string tableName, BsonDocument doc);
        public abstract Task<bool> UpdateDatabase(string oldDbName, string newDbName);
        public abstract Task<bool> UpdateTable(string dbName, string oldTableName, string newTableName);
        public abstract Task<bool> UpdateItem(string dbName,string tableName,BsonDocument doc);
        public abstract Task<List<string>> ReadDatabases();
        public abstract Task<bool> ReadTables(string dbName);
        public abstract Task<bool> ReadItems(string dbName, string tableName);
        public abstract Task<bool> DeleteDatabase(string dbName);
        public abstract Task<bool> DeleteTable(string dbName,string tableName);
        public abstract Task<bool> DeleteItem(string dbName, string tableName, string id);
    }
}