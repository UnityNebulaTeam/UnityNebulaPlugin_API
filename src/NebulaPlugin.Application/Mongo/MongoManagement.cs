using MongoDB.Bson;
using MongoDB.Driver;
using NebulaPlugin.Common.Exceptions.MongoExceptions;

namespace NebulaPlugin.Application.Mongo;
public class MongoManagement : DatabaseManager
{
    private string connectionURL;
    private MongoClient client;
    public MongoManagement(string _Url)
    {
        if (string.IsNullOrWhiteSpace(_Url))
            throw new MongoEmptyValueException("connection string cannot be null ", nameof(MongoClient));

        connectionURL = _Url;
        client = new MongoClient(connectionURL);

    }

    #region Create

    /// <summary>
    /// Yeni bir veritabanı oluşturmak için
    /// </summary>
    /// <param name="dbName">Veritabanı adı</param>
    public override async Task<bool> CreateDatabase(string dbName, string tableName)
    {
        try
        {
            await client.GetDatabase(dbName).CreateCollectionAsync(tableName);
            Console.WriteLine($" Veritabanı Oluşturuldu");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Veritabanı oluşturulamadı. Hata kodu {ex.Message}");
            throw new MongoOperationFailedException($" '{dbName}' database opearation failed: {ex.Message}");

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
            throw new MongoOperationFailedException($" '{tableName}' table opearation failed: {ex.Message}");

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
            doc["_id"] = ObjectId.GenerateNewId();
            await client.GetDatabase(dbName).GetCollection<BsonDocument>(tableName).InsertOneAsync(doc);
            Console.WriteLine("Veri Seti Eklendi");

            return true;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veri oluşturulamadı. Hata Mesajı : {ex.Message}");
            throw new MongoOperationFailedException($" '{tableName}' 'item' opearation failed: {ex.Message}");

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
        await CheckIfDatabaseCollectionExists(dbName: dbName);
        try
        {
            await client.DropDatabaseAsync(dbName);
            Console.WriteLine($"{dbName} veritabanı silindi");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{dbName} veritabanı silinemedi. {ex.Message}");
            throw new MongoOperationFailedException($" '{dbName}' database opearation failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Koleksiyon silmek için
    /// </summary>
    /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı</param>
    /// <param name="tableName">Koleksiyonun adı</param>
    public override async Task<bool> DeleteTable(string dbName, string tableName)
    {
        await CheckIfDatabaseCollectionExists(dbName, tableName);

        try
        {
            var database = client.GetDatabase(dbName);
            await database.DropCollectionAsync(tableName);
            Console.WriteLine($"{tableName} koleksiyonu silindi");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{tableName} koleksiyonu silinemedi. Hata kodu: {ex.Message}");
            throw new MongoOperationFailedException($" '{tableName}' table opearation failed: {ex.Message}");

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
        await CheckIfDatabaseCollectionExists(dbName: dbName, collectionName: tableName);

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
            throw new MongoOperationFailedException($" '{tableName}' 'item' opearation failed: {ex.Message}");

        }
    }



    #endregion

    #region Read

    /// <summary>
    /// Mongoclient'a bağlı tüm veritabanlarını okur
    /// </summary>
    public override async Task<List<string>> ReadDatabases()
    {
        List<string> result = new();
        try
        {
            var databases = await client.ListDatabaseNames().ToListAsync();

            foreach (var db in databases.ToList())
            {
                result.Add(db);
            }

            return result;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veritabanları okunamadı. Hata kodu {ex.Message}");
            throw new MongoOperationFailedException($"database opearation failed: {ex.Message}");

        }
    }

    /// <summary>
    /// Veritabanına bağlı tüm koleksiyonları okur
    /// </summary>
    /// <param name="dbName">Koleksiyonlarını görmek istediğiniz veritabanı</param>
    public override async Task<List<string>> ReadTables(string dbName)
    {
        await CheckIfDatabaseCollectionExists(dbName: dbName);
        try
        {
            var db = client.GetDatabase(dbName);
            var collections = await db.ListCollectionNamesAsync();

            return collections.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Koleksiyonlar okunamadı {ex.Message}");
            throw new MongoOperationFailedException($"database opearation failed: {ex.Message}");

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
        await CheckIfDatabaseCollectionExists(dbName: dbName, collectionName: tableName);

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
            throw new MongoOperationFailedException($"database opearation failed: {ex.Message}");
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
        await CheckIfDatabaseCollectionExists(dbName: oldDbName);

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
                if (documents.Count > 0)
                    newCollection.InsertMany(documents);
                else
                    await newDatabase.CreateCollectionAsync("deneme");
            }
            await client.DropDatabaseAsync(oldDbName);
            Console.WriteLine("Veritabanı Güncellendi");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Veritabanı güncellenemedi {ex.Message} {ex.InnerException}");
            throw new MongoOperationFailedException($" '{oldDbName}' database opearation failed: {ex.Message}");
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

        await CheckIfDatabaseCollectionExists(dbName, oldTableName);

        try
        {
            var db = client.GetDatabase(dbName);
            var table = db.GetCollection<BsonDocument>(oldTableName);
            var documents = table.Find(new BsonDocument()).ToList();
            await client.GetDatabase(dbName).CreateCollectionAsync(newTableName);
            var collection = client.GetDatabase(dbName).GetCollection<BsonDocument>(newTableName);
            if (documents.Count > 0)
                await collection.InsertManyAsync(documents);
            await db.DropCollectionAsync(oldTableName);
            Console.WriteLine("Table  Güncellendi");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Table Guncellenmedi {ex.Message}");
            throw new MongoOperationFailedException($" '{dbName}' database opearation failed: {ex.Message}");



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
        await CheckIfDatabaseCollectionExists(dbName: dbName, collectionName: tableName);

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
            throw new MongoOperationFailedException($" '{dbName}' database opearation failed: {ex.Message}");

        }
    }
    #endregion

    private async Task CheckIfDatabaseCollectionExists(string dbName, string collectionName = null)
    {

        var databases = await client.ListDatabaseNames().ToListAsync();
        var databaseExist = databases.Exists(d => d == dbName);

        if (!databaseExist)
            throw new MongoNotFoundException(dbName);

        var db = client.GetDatabase(dbName);

        if (collectionName is not null)
        {
            var filter = new BsonDocument("name", collectionName);
            var collections = await db.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

            var collectionExist = await collections.AnyAsync();

            if (!collectionExist)
                throw new MongoNotFoundException(collectionName);
        }


    }

}
