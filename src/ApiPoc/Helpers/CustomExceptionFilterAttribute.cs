using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;
using ApiPoc.Representations;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.WebUtilities;

namespace ApiPoc.Helpers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // Another ugly patch
            var urlHelper = context.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();

            var exception = context.Exception;
            context.Result = new NegotiatedResult(new Error($"Unexpected exception: {exception.Message}")
            {
                Exception = exception,
                Links = new[] { urlHelper.LinkHome() },
                CustomStatusCode = StatusCodes.Status500InternalServerError
            });
        }
    }
}
