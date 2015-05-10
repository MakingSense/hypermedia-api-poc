using Microsoft.AspNet.WebUtilities;
using System;

namespace ApiPoc.Representations
{
    public class OkRepresentation : BaseRepresentation
    {
        public string Message { get; set; }
    }
}
