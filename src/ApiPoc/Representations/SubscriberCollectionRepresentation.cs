namespace ApiPoc.Representations
{
    public class SubscriberCollectionRepresentation : SimpleRepresentation
    {
        public SubscriberRepresentation[] Items { get; set; } = new SubscriberRepresentation[] { };
    }
}
