using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class OkRepresentation : SimpleRepresentation
    {
        public string Message { get; set; }
    }
}
