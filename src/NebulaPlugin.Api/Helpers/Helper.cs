using MongoDB.Bson;
using MongoDB.Bson.IO;

namespace NebulaPlugin.Api.Helpers;

public static class Helper
{
    /// <summary>
    /// BsonDocument listesini json'a çevirir. Örneğin koleksiyona bağlı değerleri json olarak çıktı almak için kullanılır.
    /// </summary>
    /// <param name="items">Değerler</param>
    /// <returns></returns>
    public static string ConvertBsonDocumentToJson(List<BsonDocument> items)
    {
        string jsonOutput = "";
        foreach (var doc in items)
        {
            var bsonDocument = BsonDocument.Parse(doc.ToJson());
            var settings = new JsonWriterSettings { Indent = true };
            jsonOutput += bsonDocument.ToJson(settings);
        }
        return jsonOutput;
    }

    /// <summary>
    /// BsonDocument tipini json çıktı olarak verir
    /// </summary>
    /// <param name="doc">Json'a çevirilmek istenen BsonDocument değişkeni</param>
    /// <returns></returns>
    public static string ConvertBsonDocumentToJson(BsonDocument doc)
    {
        var bsonDocument = BsonDocument.Parse(doc.ToJson());
        var settings = new JsonWriterSettings { Indent = true };
        var jsonOutput = bsonDocument.ToJson(settings);
        return jsonOutput;
    }
}
