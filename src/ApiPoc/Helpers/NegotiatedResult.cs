using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPoc.Helpers
{
    public class NegotiatedResult : IActionResult
    {
        public object Value { get; private set; }
        public bool IsError { get; private set; }
        public int? StatusCode { get; set; }

        public NegotiatedResult(object value)
        {
            Value = value;
            var error = Value as ErrorRepresentation;
            if (error != null)
            {
                IsError = true;
                StatusCode = error.Code;
            }
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            IActionResult innerActionResult;
            var acceptHeader = context.HttpContext.Request.Headers["Accept"];
            if (acceptHeader != null && acceptHeader.Contains("text/html"))
            {
                innerActionResult = new ViewResult()
                {
                    StatusCode = StatusCode,
                    ViewName = IsError ? "Error" : null,
                    ViewData = new ViewDataDictionary(
                        new EmptyModelMetadataProvider(),
                        context.ModelState ?? new ModelStateDictionary())
                    {
                        Model = Value
                    }
                };
            }
            else
            {
                innerActionResult = new ObjectResult(Value)
                {
                    StatusCode = StatusCode
                };
            }
            return innerActionResult.ExecuteResultAsync(context);
        }
    }
}
