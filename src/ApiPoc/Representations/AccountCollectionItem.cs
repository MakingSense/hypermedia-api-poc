using System;

namespace ApiPoc.Representations
{
    public class AccountCollectionItem : BaseRepresentation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17; // Suitable nullity checks etc, of course :)
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (FirstName == null ? 587 : FirstName.GetHashCode());
                hash = hash * 23 + (LastName == null ? 587 : LastName.GetHashCode());
                return hash;
            }
        }
    }
}
