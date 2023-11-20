using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using NebulaPlugin.Api.Services.Mongo;
namespace NebulaPlugin.Api.Controllers;

public class MongoController : BaseController
{
  IMongoService mongoService;
  public MongoController(IMongoService _service) => mongoService = _service;

  [HttpGet]
  public async Task Test2()
  {
    await mongoService.TestGetDatabases();
  }

  /// <summary>
  /// BsonDocument listesini json'a çevirir. Örneğin koleksiyona bağlı değerleri json olarak çıktı almak için kullanılır.
  /// </summary>
  /// <param name="items">Değerler</param>
  /// <returns></returns>
  private string ConvertBsonDocumentToJson(List<BsonDocument> items)
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
  private string ConvertBsonDocumentToJson(BsonDocument doc)
  {
    var bsonDocument = BsonDocument.Parse(doc.ToJson());
    var settings = new JsonWriterSettings { Indent = true };
    var jsonOutput = bsonDocument.ToJson(settings);
    return jsonOutput;
  }
}


