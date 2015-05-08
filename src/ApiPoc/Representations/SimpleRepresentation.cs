using ApiPoc.Helpers;
using System.Collections;
using System.Collections.Generic;

namespace ApiPoc.Representations
{
    public class SimpleRepresentation : BaseRepresentation
    {
        public override string GetEtag()
        {
            return $"W/\"{ GetLinkBag().GetHashCode().ToString() }\"";
        }
    }
}
