﻿using System;

namespace ApiPoc.Representations
{
    public class SubscriptorRepresentation : BaseRepresentation
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime? Birday { get; set; }
    }
}
