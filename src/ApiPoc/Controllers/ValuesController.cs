using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace ApiPoc.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value7" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 5)
            {
                return new ObjectResult("value");
            }
            else
            {
                // I prefeer to use HttpResponseException (see http://www.strathweb.com/2015/01/migrating-asp-net-web-api-mvc-6-exploring-web-api-compatibility-shim/#crayon-553fc01e6c93a647073714 )
                // but it requieres a compatibility layer (see https://www.nuget.org/packages/Microsoft.AspNet.Mvc.WebApiCompatShim/ )
                // so, probably, it is not a good practice
                // return new HttpNotFoundResult();
                // return HttpNotFound();
                // Or maybe this:
                var result = new ObjectResult(new { message = "We only have a value with id = 5" });
                result.StatusCode = 404;
                return result;
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
