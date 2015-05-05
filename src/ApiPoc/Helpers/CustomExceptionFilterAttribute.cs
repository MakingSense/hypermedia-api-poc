using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;
using ApiPoc.Representations;
using Microsoft.Framework.DependencyInjection;

namespace ApiPoc.Helpers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // Another ugly patch
            var urlHelper = context.HttpContext.RequestServices.GetRequiredService<IUrlHelper>();

            var exception = context.Exception;
            context.Result = new NegotiatedResult(new ErrorRepresentation()
            {
                Code = 500,
                Message = "Unexpected exception: " + exception.Message,
                Exception = exception,
                Links = new[] { urlHelper.LinkHome(Rel.Home) }
            });
        }
    }
}
