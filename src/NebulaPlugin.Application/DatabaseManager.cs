using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace NebulaPlugin.Application
{
    public abstract class DatabaseManager
    {
        public abstract void CreateDatabase(string dbName);
        public abstract void CreateTable(string dbName, string tableName);
        public abstract void CreateItem(string dbName, string tableName, BsonDocument doc);
        public abstract void UpdateDatabase(string oldDbName, string newDbName);
        public abstract void UpdateTable(string dbName, string oldTableName, string newTableName);
        public abstract void UpdateItem(string dbName,string tableName,BsonDocument doc);
        public abstract void ReadDatabases();
        public abstract void ReadTables(string dbName);
        public abstract void ReadItems(string dbName, string tableName);
        public abstract void DeleteDatabase(string dbName);
        public abstract void DeleteTable(string dbName,string tableName);
        public abstract void DeleteItem(string dbName, string tableName, string id);
    }
}