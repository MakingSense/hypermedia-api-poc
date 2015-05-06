using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.PersistenceModel
{
    public class Account
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birthday { get; set; }

        public List<Subscriber> AllSubscribers { get; set; } = new List<Subscriber>();

        public IEnumerable<Subscriber> Subscribers
        {
            get { return AllSubscribers.Where(x => x.Unsubscribed == false); }
        } 
    }
}
