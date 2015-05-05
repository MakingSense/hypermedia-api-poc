using Microsoft.AspNet.WebUtilities;

namespace ApiPoc.Representations
{
    public class ErrorRepresentation : BaseRepresentation
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
