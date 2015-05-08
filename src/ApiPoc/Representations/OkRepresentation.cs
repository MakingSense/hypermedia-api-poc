using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class OkRepresentation : BaseRepresentation
    {
        public string Message { get; set; }

        public override string GetEtag()
        {
            var hash = GetLinkBag().GetHashCode();
            unchecked // Overflow is fine, just wrap
            {
                hash = hash * 23 + (Message == null ? 587 : Message.GetHashCode());
            }
            return $"W/\"{ hash.ToString() }\"";
        }
    }
}
