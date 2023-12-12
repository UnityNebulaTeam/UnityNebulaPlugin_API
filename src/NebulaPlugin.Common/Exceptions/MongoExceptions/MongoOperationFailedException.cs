using System.Net;
using src.NebulaPlugin.Common.Exceptions;

namespace NebulaPlugin.Common.Exceptions.MongoExceptions;

public class MongoOperationFailedException : AppException
{
    public MongoOperationFailedException()
    {
    }

    public MongoOperationFailedException(string? message) : base(message)
    {
    }

    public MongoOperationFailedException(string objName, string message) : base($"'{objName}' {message}")
    {
    }


    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
}
