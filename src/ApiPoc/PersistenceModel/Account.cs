using System;
using System.Collections.Generic;

namespace ApiPoc.PersistenceModel
{
    public class Account
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public List<Subscriber> Subscribers { get; set; } = new List<Subscriber>();
    }
}
