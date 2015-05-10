using ApiPoc.Helpers;

namespace ApiPoc.Representations
{
    public interface IRepresentation
    {
        Link[] Links { get; }

        LinkBag GetLinkBag();
    }
}
