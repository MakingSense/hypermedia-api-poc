using System;

namespace ApiPoc.Representations
{
    public class SubscriberCollectionItem : BaseRepresentation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17; // Suitable nullity checks etc, of course :)
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (FirstName == null ? 587 : FirstName.GetHashCode());
                hash = hash * 23 + (LastName == null ? 587 : LastName.GetHashCode());
                hash = hash * 23 + (Email == null ? 587 : Email.GetHashCode());
                return hash;
            }
        }

        public override string GetEtag()
        {
            return $"W/\"{ GetHashCode().ToString() }\"";
        }
    }
}
