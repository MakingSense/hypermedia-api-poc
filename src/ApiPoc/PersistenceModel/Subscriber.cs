using System;

namespace ApiPoc.PersistenceModel
{
    public class Subscriber
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public bool Unsubscribed { get; set; }
    }
}
