using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class ErrorRepresentation : BaseRepresentation
    {
        public int StatusCode { get; set; } = StatusCodes.Status500InternalServerError;

        public string Message { get; set; }

        //TODO: consider remove this field or skip in serialization to avoid to expose the exception
        public Exception Exception { get; set; }
    }
}
