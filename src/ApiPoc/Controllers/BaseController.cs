using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;


namespace ApiPoc.Controllers
{
    public class BaseController : Controller
    {
        public IDatabase Database { get; private set; }

        public BaseController(IDatabase database)
        {
            Database = database;
        }
    }
}
