using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;

namespace ApiPoc.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IDatabase database)
            : base(database)
        {
        }

        [HttpGet("/")]
        public NegotiatedResult Index()
        {
            var currentAccount = Database.GetCurrentAccount();

            return NegotiatedResult(new SimpleRepresentation()
            {
                Links = new[] {
                    Url.LinkSelf(Rel.Home),
                    Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountItem, "My account details"),
                    Url.Link<SubscribersController>(x => x.Index(currentAccount.Id), Rel.SubscriberCollection, "My Subscribers"),

                    // Hide because standard user does not need this list
                    // Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Account List"),
                }
            });
        }
    }
}
