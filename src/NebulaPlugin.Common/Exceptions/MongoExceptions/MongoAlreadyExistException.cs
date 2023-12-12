using System.Net;
using src.NebulaPlugin.Common.Exceptions;

namespace NebulaPlugin.Common.Exceptions.MongoExceptions;

public class MongoAlreadyExistException : AppException
{
    public MongoAlreadyExistException()
    {
    }

    public MongoAlreadyExistException(string? exceptionMessage) : base(exceptionMessage)
    {
    }

    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
}
