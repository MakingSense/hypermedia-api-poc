using ApiPoc.Representations;

namespace ApiPoc.Controllers
{
    internal class HomeRepresentation
    {
        public HomeRepresentation()
        {
        }

        public LinkRepresentation[] Links { get; set; }
    }
}