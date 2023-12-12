using System.Net;
using System.Runtime.Serialization;

namespace src.NebulaPlugin.Common.Exceptions
{
    public abstract class AppException : Exception
    {
        protected AppException()
        {
        }

        protected AppException(string? message) : base(message)
        {
        }

        protected AppException(string? objName, string? message) : base(message + " " + objName)
        {
        }

        public abstract HttpStatusCode StatusCode { get; set; }



    }
}