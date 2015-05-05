using System;

namespace ApiPoc.Representations
{
    public class SubscriberRepresentation : BaseRepresentation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
