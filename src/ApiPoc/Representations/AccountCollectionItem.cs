using System;

namespace ApiPoc.Representations
{
    public class AccountCollectionItem : SimpleRepresentation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
