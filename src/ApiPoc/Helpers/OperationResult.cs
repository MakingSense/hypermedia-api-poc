using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPoc.Helpers
{
    public class OperationResult : NegotiatedResult
    {
        public OperationResult(OkRepresentation value)
            : base(value)
        {
            CustomHtmlView = "Done";
        }

        [Obsolete("Only for demo purposes, empty results should be avoided, see http://blog.ploeh.dk/2013/04/30/rest-lesson-learned-avoid-204-responses/")]
        public OperationResult()
            : base(new OkRepresentation())
        {
            CustomStatusCode = StatusCodes.Status204NoContent;
        }
    }
}
