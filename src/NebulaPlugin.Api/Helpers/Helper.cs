using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using NebulaPlugin.Api.Dtos.Mongo;
using Newtonsoft.Json;

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
            doc["_id"] = doc["_id"].ToString();
            var bsonDocument = BsonDocument.Parse(doc.ToJson());
            var settings = new JsonWriterSettings { Indent = true };
            jsonOutput += bsonDocument.ToJson(settings);
        }
        return jsonOutput;
    }

    public static JsonElement ConvertBsonDocumentToJsonElement(BsonDocument doc)
    {
        doc["_id"] = doc["_id"].ToString();

        var bsonDocument = BsonDocument.Parse(doc.ToJson());
        var json = bsonDocument.ToJson();

        JsonDocument jsonDocument = JsonDocument.Parse(json);
        JsonElement jsonElement = jsonDocument.RootElement;

        return jsonElement;
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

    public static string ConvertBsonDocumentToJson(List<TableItemDto> result)
    {

        var bsonArray = new BsonArray();

        foreach (var tableItemDto in result)
        {
            if (tableItemDto.Doc != null)
            {
                // bsonArray.Add(tableItemDto.Doc);
            }
        }

        var bsonDocument = new BsonDocument { { "items", bsonArray } };
        var settings = new JsonWriterSettings { Indent = true };
        var jsonOutput = bsonArray.ToJson(settings);

        return jsonOutput;
    }
}
