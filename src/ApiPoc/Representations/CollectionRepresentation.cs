namespace ApiPoc.Representations
{
    public class CollectionRepresentation<T> : SimpleRepresentation
    {
        public T[] Items { get; set; } = new T[] { };
    }
}
