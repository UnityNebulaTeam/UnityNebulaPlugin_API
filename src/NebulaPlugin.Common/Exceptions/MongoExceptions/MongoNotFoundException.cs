using System.Net;
using src.NebulaPlugin.Common.Exceptions;

namespace NebulaPlugin.Common.Exceptions.MongoExceptions;

public class MongoNotFoundException : AppException
{
    public MongoNotFoundException()
    {
    }

    public MongoNotFoundException(string? message) : base(message)
    {
    }

    public MongoNotFoundException(string? message, string? objName) : base(message, objName)
    {
    }

    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotFound;




}
