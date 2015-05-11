using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.AspNet.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiPoc.Helpers
{
    public class NegotiatedResult : IActionResult
    {
        public IRepresentation Value { get; private set; }

        public NegotiatedResult(IRepresentation value)
        {
            Value = value;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            IActionResult innerActionResult;

            var acceptHeader = context.HttpContext.Request.Headers["Accept"];
            var responseEncodding =
                acceptHeader != null && acceptHeader.Contains("text/html") ? "text/html"
                : "application/json";

            var noContent = Value == null;
            var viewName = noContent ? null : $"Models/{Value.GetType().Name}";
            var statusCode = noContent ? StatusCodes.Status204NoContent : Value.CustomStatusCode ?? StatusCodes.Status200OK;

            // Only for demo purposes, it should not be automatic, it should depend on the model to 
            // Generate it automatically, to generate based on model information, to not include etag, etc
            var currentEtag = CalculateHashByValues(new
            {
                Value = Value,
                ResponseEncodding = responseEncodding,
                CustomStatusCode = statusCode,
                CustomHtmlView = viewName
            }).ToString();

            string requestedEtag = context.HttpContext.Request.Headers["If-None-Match"];
            if (currentEtag != null)
            {
                context.HttpContext.Response.Headers["ETag"] = currentEtag;
            }

            if (noContent)
            {
                innerActionResult = new NoContentResult();
            }
            else if(requestedEtag != null && requestedEtag == currentEtag)
            {
                innerActionResult = new HttpStatusCodeResult(StatusCodes.Status304NotModified);
            }
            else 
            {
                if (responseEncodding == "text/html")
                {
                    innerActionResult = new ViewResult()
                    {
                        StatusCode = statusCode,
                        ViewName = viewName,
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
                        StatusCode = statusCode
                    };
                }
            }
            return innerActionResult.ExecuteResultAsync(context);
        }

        protected virtual ulong CalculateHashByValues(object obj)
        {
            //TODO: review this code, it could have holes
            unchecked
            {
                if (obj == null)
                {
                    return 23;
                }

                var type = obj.GetType();
                var typeInfo = type.GetTypeInfo();

                if (typeInfo.IsValueType || obj is string)
                {
                    return (uint)obj.GetHashCode();
                }

                ulong hash = 29;

                var collection = obj as IEnumerable<object>;
                if (collection != null)
                {
                    foreach (var o in collection)
                    {
                        hash = hash * 31 + CalculateHashByValues(o);
                    }
                    return hash;
                }

                foreach (var prop in type.GetProperties())
                {
                    var get = prop.GetGetMethod();
                    if (get != null && !get.IsStatic && get.GetParameters().Length == 0)
                    {
                        hash = hash * 37 + CalculateHashByValues(prop.GetValue(obj, null));
                    }
                }
                return hash;
            }
        }
    }
}
