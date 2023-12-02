using System.Net;
using src.NebulaPlugin.Common.Exceptions;

namespace NebulaPlugin.Common.Exceptions.MongoExceptions;

public class MongoNotFoundException : AppException
{
    public MongoNotFoundException(string objName) : base($"'{objName}' not found")
    {
    }


    public MongoNotFoundException(string? message, string? objName) : base(message, objName)
    {
    }

    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;




}
