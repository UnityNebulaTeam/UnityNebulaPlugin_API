using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
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
            if (!string.IsNullOrEmpty(_Url))
            {
                connectionURL = _Url;
                client = new MongoClient(connectionURL);
            }
            else
            {
                Console.WriteLine("Connection Url boş olamaz");
            }
        }

        #region Create

        /// <summary>
        /// Yeni bir veritabanı oluşturmak için
        /// </summary>
        /// <param name="dbName">Veritabanı adı</param>
        public override async Task<bool> CreateDatabase(string dbName)
        {
            try
            {
                await client.GetDatabase(dbName).CreateCollectionAsync("Deneme");
                Console.WriteLine($" Veritabanı Oluşturuldu");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Veritabanı oluşturulamadı. Hata kodu {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Yeni koleksiyon oluşturmak için
        /// </summary>
        /// <param name="dbName">Hangi veritabanı altında oluşacağı</param>
        /// <param name="tableName"></param>
        public override async Task<bool> CreateTable(string dbName, string tableName)
        {
            try
            {
                await client.GetDatabase(dbName).CreateCollectionAsync(tableName);
                Console.WriteLine("Table Oluşturuldu");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Tablo oluşturulamadı  {ex.Message}");
                return false;
            }

        }

        /// <summary>
        /// Yeni bir veri oluşturmak için
        /// </summary>
        /// <param name="dbName">Hangi veritabanı altında</param>
        /// <param name="tableName">Hangi collection altında</param>
        /// <param name="doc">Veri seti</param>
        public override async Task<bool> CreateItem(string dbName, string tableName, BsonDocument doc)
        {
            try
            {
                await client.GetDatabase(dbName).GetCollection<BsonDocument>(tableName).InsertOneAsync(doc);
                Console.WriteLine("Veri Seti Eklendi");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri oluşturulamadı. Hata Mesajı : {ex.Message}");
                return false;
            }
        }

        #endregion
        #region Delete

        /// <summary>
        /// Veritabanını silmek için
        /// </summary>
        /// <param name="dbName">Silinecek veritabanı adı</param>
        public override async Task<bool> DeleteDatabase(string dbName)
        {
            try
            {
                await client.DropDatabaseAsync(dbName);
                Console.WriteLine($"{dbName} veritabanı silindi");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{dbName} veritabanı silinemedi. {ex.Message}");
                return true;
            }
        }

        /// <summary>
        /// Koleksiyon silmek için
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı</param>
        /// <param name="tableName">Koleksiyonun adı</param>
        public override async Task<bool> DeleteTable(string dbName, string tableName)
        {
            try
            {
                await client.GetDatabase(dbName).DropCollectionAsync(tableName);
                Console.WriteLine($"{dbName} koleksiyon silindi");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{dbName} koleksiyon silinemedi. Hata kodu {ex.Message}");
                return false;
            }
        }


        /// <summary>
        /// Veri silmek için
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
        /// <param name="tableName">Verinin bağlı olduğu koleksiyon adı</param>
        /// <param name="id">Bsondocument id'si</param>
        public override async Task<bool> DeleteItem(string dbName, string tableName, string id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
                var result = await client.GetDatabase(dbName).GetCollection<BsonDocument>(tableName).DeleteOneAsync(filter);
                if (result.DeletedCount > 0)
                {
                    Console.WriteLine($"{id} numaralı döküman başarıyla silindi");
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{id} numaralı döküman silinemedi. Hata kodu {ex.Message}");
                return false;
            }
        }



        #endregion
        #region Read

        /// <summary>
        /// Mongoclient'a bağlı tüm veritabanlarını okur
        /// </summary>
        public override async Task<List<string>> ReadDatabases()
        {
            try
            {
                var databases = await client.ListDatabaseNamesAsync();
                foreach (var db in databases.ToList())
                {
                    Console.WriteLine(db);
                }
                return databases.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanları okunamadı. Hata kodu {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Veritabanına bağlı tüm koleksiyonları okur
        /// </summary>
        /// <param name="dbName">Koleksiyonlarını görmek istediğiniz veritabanı</param>
        public override async Task<List<string>> ReadTables(string dbName)
        {
            try
            {
                var db = client.GetDatabase(dbName);
                var collections = await db.ListCollectionNamesAsync();
                return collections.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Koleksiyonlar okunamadı {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Koleksiyona bağlı verileri listeler
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
        /// <param name="tableName">Verilerin bağlı olduğu koleksiyon adı</param>
        public override async Task<List<BsonDocument>> ReadItems(string dbName, string tableName)
        {
            try
            {
                var db = client.GetDatabase(dbName);
                var table = db.GetCollection<BsonDocument>(tableName);
                var documents = await table.FindAsync(new BsonDocument());
                return documents.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Itemler okunamadı {ex.Message}");
                return null;
            }
        }

        #endregion


        #region Update


        /// <summary>
        /// Veritabanının adını günceller ve tüm koleksiyonları, koleksiyona bağlı verileri yeni oluşturulan veritabanına taşır.
        /// </summary>
        /// <param name="oldDbName">Değiştirilmek istenilen veritabanı adı</param>
        /// <param name="newDbName">Yeni veritabanı adı</param>
        public override async Task<bool> UpdateDatabase(string oldDbName, string newDbName)
        {
            try
            {
                var oldDatabase = client.GetDatabase(oldDbName);
                var newDatabase = client.GetDatabase(newDbName);
                var collectionNames = await oldDatabase.ListCollectionNamesAsync();
                foreach (var collectionName in collectionNames.ToList())
                {
                    var oldCollection = oldDatabase.GetCollection<BsonDocument>(collectionName);
                    var newCollection = newDatabase.GetCollection<BsonDocument>(collectionName);
                    var documents = oldCollection.Find(new BsonDocument()).ToList();
                    newCollection.InsertMany(documents);
                }
                await client.DropDatabaseAsync(oldDbName);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanı güncellenemedi {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Koleksiyon adını günceller ve tüm koleksiyon verilerini yeni oluşturulan koleksiyona aktarır. Eski koleksiyonu siler.
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
        /// <param name="oldTableName">Değiştirilmek istenilen koleksiyon adı</param>
        /// <param name="newTableName">Yeni koleksiyon adı</param>
        public override async Task<bool> UpdateTable(string dbName, string oldTableName, string newTableName)
        {
            try
            {
                var db = client.GetDatabase(dbName);
                var table = db.GetCollection<BsonDocument>(oldTableName);
                var documents = table.Find(new BsonDocument()).ToList();
                await client.GetDatabase(dbName).CreateCollectionAsync(newTableName);
                var collection = client.GetDatabase(dbName).GetCollection<BsonDocument>(newTableName);
                await collection.InsertManyAsync(documents);
                await db.DropCollectionAsync(oldTableName);
                Console.WriteLine("Veritabanı Güncellendi");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veritabanı Güncellendi {ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// Veri güncellemek için. Veritabanı ve koleksiyon parametresi alır. Güncellemek istenilen veri BsonDocument tipinde gönderilir.
        /// BsonDocument parametresi içerisinde bulunan ObjectId referans alınarak güncelleme yapılır.
        /// </summary>
        /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı</param>
        /// <param name="tableName">Verinin bağlı olduğu koleksiyon</param>
        /// <param name="doc">Güncellenecek veri</param>
        public override async Task<bool> UpdateItem(string dbName, string tableName, BsonDocument doc)
        {
            try
            {
                var db = client.GetDatabase(dbName);
                var collection = db.GetCollection<BsonDocument>(tableName);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", doc["_id"]);
                await db.GetCollection<BsonDocument>(tableName).ReplaceOneAsync(filter, doc);
                Console.WriteLine($"Item güncellendi.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri güncellenemedi. Hata kodu {ex.Message}");
                return false;
            }
        }
        #endregion
    }
}