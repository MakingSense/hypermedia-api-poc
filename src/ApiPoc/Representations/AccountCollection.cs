namespace ApiPoc.Representations
{
    public class AccountCollection : CollectionRepresentation<AccountCollectionItem>
    {
        public override string GetEtag()
        {
            return $"W/\"{ GetCollectionHash().ToString() }\"";
        }
    }
}
