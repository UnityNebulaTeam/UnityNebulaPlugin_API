using System.Net;
using src.NebulaPlugin.Common.Exceptions;

namespace NebulaPlugin.Common.Exceptions.MongoExceptions;

public class MongoEmptyValueException : AppException
{
    public MongoEmptyValueException()
    {
    }

    public MongoEmptyValueException(string? message) : base(message)
    {
    }

    public MongoEmptyValueException(string? objName, string? message) : base(objName, message)
    {
    }

    public override HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
}
