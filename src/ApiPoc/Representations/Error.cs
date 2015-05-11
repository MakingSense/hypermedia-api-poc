using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class Error : Message
    {
        public int ApplicationCode { get; set; }

        internal Exception Exception { get; set; }

        public Error(string messageText, int statusCode = StatusCodes.Status500InternalServerError, int applicationCode = 0)
            : base(messageText)
        {
            CustomStatusCode = statusCode;
            ApplicationCode = applicationCode;
        }

    }
}
