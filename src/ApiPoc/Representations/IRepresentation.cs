using ApiPoc.Helpers;

namespace ApiPoc.Representations
{
    public interface IRepresentation
    {
        LinkRepresentation[] Links { get; }

        LinkBag GetLinkBag();
    }
}
