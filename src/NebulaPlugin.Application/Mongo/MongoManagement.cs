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
            throw new MongoEmptyValueException("connection string cannot be found ", "MongoDB");

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
            return true;
        }
        catch (Exception ex)
        {
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
        var db = await GetExistingDb(dbName);

        try
        {
            await db.CreateCollectionAsync(tableName);
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
        var dbCollection = await GetExistingDbCollection(dbName, tableName);

        try
        {
            doc["_id"] = ObjectId.GenerateNewId();
            await dbCollection.InsertOneAsync(doc);
            return true;

        }
        catch (Exception ex)
        {
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
        await GetExistingDb(dbName);

        try
        {
            await client.DropDatabaseAsync(dbName);
            return true;
        }
        catch (Exception ex)
        {
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
        await GetExistingDbCollection(dbName, tableName);

        try
        {
            var database = client.GetDatabase(dbName);
            await database.DropCollectionAsync(tableName);
            return true;
        }
        catch (Exception ex)
        {
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
        var dbCollection = await GetExistingDbCollection(dbName, tableName);
        try
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var result = await dbCollection.DeleteOneAsync(filter);

            return result.DeletedCount > 0 ? true : false;
        }
        catch (Exception ex)
        {
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

        try
        {
            var databases = await client.ListDatabaseNames().ToListAsync();

            return databases;
        }
        catch (Exception ex)
        {
            throw new MongoOperationFailedException($"database opearation failed: {ex.Message}");
        }
    }

    /// <summary>
    /// Veritabanına bağlı tüm koleksiyonları okur
    /// </summary>
    /// <param name="dbName">Koleksiyonlarını görmek istediğiniz veritabanı</param>
    public override async Task<List<string>> ReadTables(string dbName)
    {
        var db = await GetExistingDb(dbName);
        try
        {
            var collections = await db.ListCollectionNamesAsync();

            return collections.ToList();
        }
        catch (Exception ex)
        {
            throw new MongoOperationFailedException($"database opearation failed: {ex.Message}");

        }
    }

    /// <summary>
    /// Koleksiyona bağlı verileri listeler
    /// </summary>
    /// <param name="dbName">Koleksiyonun bağlı olduğu veritabanı adı</param>
    /// <param name="tableName">Verilerin bağlı olduğu koleksiyon adı</param>
    public override async Task<List<BsonDocument>> ReadItems(string dbName, string tableName)
    {
        var dbCollection = await GetExistingDbCollection(dbName, tableName);

        try
        {
            var documents = await dbCollection.FindAsync(new BsonDocument());
            return documents.ToList();
        }
        catch (Exception ex)
        {
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

        var oldDatabase = await GetExistingDb(oldDbName);

        try
        {
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
            return true;
        }
        catch (Exception ex)
        {
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

        var dbCollection = await GetExistingDbCollection(dbName, oldTableName);

        try
        {
            var db = client.GetDatabase(dbName); //!
            var documents = dbCollection.Find(new BsonDocument()).ToList();
            await client.GetDatabase(dbName).CreateCollectionAsync(newTableName);
            var collection = client.GetDatabase(dbName).GetCollection<BsonDocument>(newTableName);
            if (documents.Count > 0)
                await collection.InsertManyAsync(documents);
            await db.DropCollectionAsync(oldTableName);
            return true;
        }
        catch (Exception ex)
        {
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
        var dbCollection = await GetExistingDbCollection(dbName, tableName);

        try
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", doc["_id"]);
            var res = await dbCollection.ReplaceOneAsync(filter, doc);

            if (!(res.ModifiedCount > 0) || !res.IsAcknowledged)
                throw new MongoOperationFailedException($" '{dbName}' database opearation failed");

            return true;
        }
        catch (Exception ex)
        {
            throw new MongoOperationFailedException($" '{dbName}' database opearation failed: {ex.Message}");

        }
    }
    #endregion

    private async Task<IMongoDatabase> GetExistingDb(string dbName)
    {
        var databaseNames = await client.ListDatabaseNames().ToListAsync();

        if (!databaseNames.Contains(dbName))
            throw new MongoNotFoundException(dbName);

        return client.GetDatabase(dbName);
    }

    private async Task<IMongoCollection<BsonDocument>> GetExistingDbCollection(string dbName, string collectionName)
    {
        var databaseNames = await client.ListDatabaseNames().ToListAsync();

        if (!databaseNames.Contains(dbName))
            throw new MongoNotFoundException(dbName);

        var db = client.GetDatabase(dbName);

        var filter = Builders<BsonDocument>.Filter.Eq("name", collectionName);
        var collectionCursor = await db.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });

        if (!await collectionCursor.AnyAsync())
            throw new MongoNotFoundException(collectionName);

        return db.GetCollection<BsonDocument>(collectionName);

    }

}
