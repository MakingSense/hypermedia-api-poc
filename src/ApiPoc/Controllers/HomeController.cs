using ApiPoc.Helpers;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;

namespace ApiPoc.Controllers
{
    public class HomeController : BaseController
    {
        private const int CURRENT_ACCOUNT_ID = 128;

        [HttpGet("/")]
        public IActionResult Index()
        {
            return Negotiated(new HomeRepresentation()
            {
                Links = new[] {
                    Url.LinkSelf(Rel.Home),
                    Url.Link<AccountsController>(x => x.Item(CURRENT_ACCOUNT_ID), Rel.AccountItem, "My account details"),

                    // Hide because standard user does not need this list
                    // Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Account List"),
                }
            });
        }
    }
}
