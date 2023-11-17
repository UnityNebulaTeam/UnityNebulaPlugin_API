using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NebulaPlugin.Application;

namespace NebulaPlugin.Application.Mongo
{
    public class MongoManagement : DatabaseManager
    {
        private string connectionURL;
        private MongoClient client;
        public MongoManagement(string _Url)
        {
            connectionURL = _Url;
            client = new MongoClient(connectionURL);
        }
        #region Create

        /// <summary>
        /// Yeni bir veritabanı oluşturmak için
        /// </summary>
        /// <param name="dbName">Veritabanı adı</param>
        public override void CreateDatabase(string dbName)
        {
            client.GetDatabase(dbName).CreateCollection("Deneme");
            Console.WriteLine($" Veritabanı Oluşturuldu");
        }
        /// <summary>
        /// Yeni koleksiyon oluşturmak için
        /// </summary>
        /// <param name="dbName">Hangi veritabanı altında oluşacağı</param>
        /// <param name="tableName"></param>
        public override void CreateTable(string dbName, string tableName)
        {
            client.GetDatabase(dbName).CreateCollection(tableName);
            Console.WriteLine("Table Oluşturuldu");
        }

        /// <summary>
        /// Yeni bir veri oluşturmak için
        /// </summary>
        /// <param name="dbName">Hangi veritabanı altında</param>
        /// <param name="tableName">Hangi collection altında</param>
        /// <param name="doc">Veri seti</param>
        public override void CreateItem(string dbName, string tableName, BsonDocument doc)
        {
            client.GetDatabase(dbName).GetCollection<BsonDocument>(tableName).InsertOne(doc);
            Console.WriteLine("Veri Seti Eklendi");
        }

        #endregion
        #region Delete

        /// <summary>
        /// Veritabanını silmek için
        /// </summary>
        /// <param name="dbName">Silinecek veritabanı adı</param>
        public override void DeleteDatabase(string dbName)
        {
            client.DropDatabase(dbName);
        }

        /// <summary>
        /// Koleksiyon silmek için
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı</param>
        /// <param name="tableName">Koleksiyonun adı</param>
        public override void DeleteTable(string dbName, string tableName)
        {
            client.GetDatabase(dbName).DropCollection(tableName);
        }


        /// <summary>
        /// Veri silmek için
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
        /// <param name="tableName">Verinin bağlı olduğu koleksiyon adı</param>
        /// <param name="id">Bsondocument id'si</param>
        public override void DeleteItem(string dbName, string tableName, string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var result = client.GetDatabase(dbName).GetCollection<BsonDocument>(tableName).DeleteOne(filter);
            if (result.DeletedCount > 0)
                Console.WriteLine($"{id} numaralı döküman başarıyla silindi");
            else
                Console.WriteLine($"{id} numaralı döküman silinemedi");
        }



        #endregion
        #region Read

        /// <summary>
        /// Mongoclient'a bağlı tüm veritabanlarını okur
        /// </summary>
        public override void ReadDatabases()
        {
            var databases = client.ListDatabases().ToList();
            foreach (var db in databases)
            {
                Console.WriteLine(db["name"].AsString);
            }
        }

        /// <summary>
        /// Veritabanına bağlı tüm koleksiyonları okur
        /// </summary>
        /// <param name="dbName">Koleksiyonlarını görmek istediğiniz veritabanı</param>
        public override void ReadTables(string dbName)
        {
            var db = client.GetDatabase(dbName);
            foreach (var collection in db.ListCollections().ToList())
            {
                Console.WriteLine(collection["name"].AsString);
            }
        }

        /// <summary>
        /// Koleksiyona bağlı verileri listeler
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
        /// <param name="tableName">Verilerin bağlı olduğu koleksiyon adı</param>
        public override void ReadItems(string dbName, string tableName)
        {
            var db = client.GetDatabase(dbName);
            var table = db.GetCollection<BsonDocument>(tableName);
            var documents = table.Find(new BsonDocument()).ToList();
            foreach (var doc in documents)
            {
                Console.WriteLine(doc);
            }
        }
        #endregion
        #region Update

        /// <summary>
        /// Veritabanının adını günceller ve tüm koleksiyonları, koleksiyona bağlı verileri yeni oluşturulan veritabanına taşır.
        /// </summary>
        /// <param name="oldDbName">Değiştirilmek istenilen veritabanı adı</param>
        /// <param name="newDbName">Yeni veritabanı adı</param>
        public override void UpdateDatabase(string oldDbName, string newDbName)
        {
            var oldDatabase = client.GetDatabase(oldDbName);

            var newDatabase = client.GetDatabase(newDbName);

            foreach (var collectionName in oldDatabase.ListCollectionNames().ToList())
            {

                var oldCollection = oldDatabase.GetCollection<BsonDocument>(collectionName);
                var newCollection = newDatabase.GetCollection<BsonDocument>(collectionName);

                var documents = oldCollection.Find(new BsonDocument()).ToList();
                newCollection.InsertMany(documents);
            }
            client.DropDatabase(oldDbName);
        }
        /// <summary>
        /// Koleksiyon adını günceller ve tüm koleksiyon verilerini yeni oluşturulan koleksiyona aktarır. Eski koleksiyonu siler.
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
        /// <param name="oldTableName">Değiştirilmek istenilen koleksiyon adı</param>
        /// <param name="newTableName">Yeni koleksiyon adı</param>
        public override void UpdateTable(string dbName, string oldTableName, string newTableName)
        {
            var db = client.GetDatabase(dbName);
            var table = db.GetCollection<BsonDocument>(oldTableName);
            var documents = table.Find(new BsonDocument()).ToList();
            client.GetDatabase(dbName).CreateCollection(newTableName);
            var collection = client.GetDatabase(dbName).GetCollection<BsonDocument>(newTableName);
            collection.InsertMany(documents);
            db.DropCollection(oldTableName);
        }
        /// <summary>
        /// Veri güncellemek için. Veritabanı ve koleksiyon parametresi alır. Güncellemek istenilen veri BsonDocument tipinde gönderilir.
        /// BsonDocument parametresi içerisinde bulunan ObjectId referans alınarak güncelleme yapılır.
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı</param>
        /// <param name="tableName">Verinin bağlı olduğu koleksiyon</param>
        /// <param name="doc">Güncellenecek veri</param>
        public override void UpdateItem(string dbName, string tableName, BsonDocument doc)
        {
            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<BsonDocument>(tableName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc["_id"]);
            db.GetCollection<BsonDocument>(tableName).ReplaceOne(filter, doc);
        }
        #endregion
    }
}