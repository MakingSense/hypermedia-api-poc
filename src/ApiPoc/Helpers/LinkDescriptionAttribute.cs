using ApiPoc.Representations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiPoc.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LinkDescriptionAttribute : Attribute
    {
        public Rel Rel { get; private set; }
        public string Description { get; private set; }

        public LinkDescriptionAttribute(Rel rel, string description)
        {
            Rel = rel;
            Description = description;
        }
    }
}
