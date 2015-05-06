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
        public IRepresentation Value { get; private set; }

        public int? CustomStatusCode { get; set; }

        public string CustomHtmlView { get; set; }

        public NegotiatedResult(IRepresentation value)
        {
            Value = value;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            IActionResult innerActionResult;
            var acceptHeader = context.HttpContext.Request.Headers["Accept"];
            if (acceptHeader != null && acceptHeader.Contains("text/html"))
            {
                innerActionResult = new ViewResult()
                {
                    StatusCode = CustomStatusCode,
                    ViewName = CustomHtmlView,
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
                    StatusCode = CustomStatusCode
                };
            }
            return innerActionResult.ExecuteResultAsync(context);
        }
    }
}
