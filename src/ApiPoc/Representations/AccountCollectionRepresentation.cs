namespace ApiPoc.Representations
{
    public class AccountCollectionRepresentation : SimpleRepresentation
    {
        public AccountRepresentation[] Items { get; set; } = new AccountRepresentation[] { };
    }
}
