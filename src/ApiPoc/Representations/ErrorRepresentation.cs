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

        public override string GetEtag()
        {
            var hash = GetLinkBag().GetHashCode();
            unchecked // Overflow is fine, just wrap
            {
                hash = hash * 23 + StatusCode.GetHashCode();
                hash = hash * 23 + (Message == null ? 587 : Message.GetHashCode());
                hash = hash * 23 + (Exception == null ? 587 : Exception.GetHashCode());
            }
            return $"W/\"{ hash.ToString() }\"";
        }
    }
}
