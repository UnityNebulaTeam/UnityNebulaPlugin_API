using System.Text.Json;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace NebulaPlugin.Api.Dtos.Mongo;

public record TableItemDto(JsonElement? Doc);
