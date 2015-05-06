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
    public class ErrorResult : NegotiatedResult
    {
        public ErrorResult(ErrorRepresentation value)
            : base(value)
        {
            CustomStatusCode = value.StatusCode;
            CustomHtmlView = "Error";
        }
    }
}
