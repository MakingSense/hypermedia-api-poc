using System;

namespace ApiPoc.Representations
{
    public class AccountDetail : BaseRepresentation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public override string GetEtag()
        {
            var hash = GetLinkBag().GetHashCode();
            unchecked // Overflow is fine, just wrap
            {
                hash = hash * 23 + Id.GetHashCode();
                hash = hash * 23 + (FirstName == null ? 587 : FirstName.GetHashCode());
                hash = hash * 23 + (LastName == null ? 587 : LastName.GetHashCode());
                hash = hash * 23 + (Email == null ? 587 : Email.GetHashCode());
                hash = hash * 23 + (Birthday == null ? 587 : Birthday.GetHashCode());
            }
            return $"W/\"{ hash.ToString() }\"";
        }
    }
}
