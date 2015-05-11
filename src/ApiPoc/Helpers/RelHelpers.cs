using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ApiPoc.Helpers
{
    public static class RelHelpers
    {
        public static bool Is(this Rel me, Rel other)
        {
            return (me & other) == other;
        }

        public static bool IsNot(this Rel me, Rel other)
        {
            return (me & other) == Rel._None;
        }

        public static bool IsAnyOf(this Rel me, params Rel[] others)
        {
            return others.Any(x => (me & x) == x);
        }
    }
}
