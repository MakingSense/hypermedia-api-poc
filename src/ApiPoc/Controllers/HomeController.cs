using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Framework.OptionsModel;

namespace ApiPoc.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IDatabase database, IOptions<AppSettings> settings)
            : base(database, settings)
        {
        }

        public NegotiatedResult NotFound()
        {
            return NegotiatedResult(new Error($"Resource not found.", StatusCodes.Status404NotFound)
            {
                Links = new[]
                {
                    Url.LinkHome()
                }
            });
        }

        [HttpGet("/")]
        [LinkDescription(Rel.Home, "Home")]
        public NegotiatedResult Index()
        {
            var currentAccount = Database.GetCurrentAccount();

            return NegotiatedResult(new Home()
            {
                Links = new[] {
                    Url.LinkHome(Rel.Self),
                    Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), description: "My account details"),
                    Url.Link<SubscribersController>(x => x.Index(currentAccount.Id, null), description: "My Subscribers"),
                    Url.Link<SubscribersController>(x => x.Detail(TemplateParameter.Create<int>(), TemplateParameter.Create<int>())),
                    Url.Link<SubscribersController>(x => x.Unsubscribe(TemplateParameter.Create<int>(), TemplateParameter.Create<int>())),
                    Url.Link<SubscribersController>(x => x.Modify(TemplateParameter.Create<int>(), TemplateParameter.Create<int>(), null)),

                    // Hide because standard user does not need this list
                    // Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Account List"),
                }
            });
        }
    }
}
