using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class ErrorRepresentation : SimpleRepresentation
    {
        public int Code { get; set; }

        public string Message { get; set; }

        //TODO: consider remove this field or skip in serialization to avoid to expose the exception
        public Exception Exception { get; set; }
    }
}
