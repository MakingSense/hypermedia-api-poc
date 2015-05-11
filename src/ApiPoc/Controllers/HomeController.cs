using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.OptionsModel;

namespace ApiPoc.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IDatabase database, IOptions<AppSettings> settings)
            : base(database, settings)
        {
        }

        [HttpGet("/")]
        public NegotiatedResult Index()
        {
            var currentAccount = Database.GetCurrentAccount();

            return NegotiatedResult(new Home()
            {
                Links = new[] {
                    Url.LinkSelf(Rel.Home),
                    Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), Rel.AccountDetail, "My account details"),
                    Url.Link<SubscribersController>(x => x.Index(currentAccount.Id, null), Rel.SubscriberCollection, "My Subscribers"),
                    Url.Link<SubscribersController>(x => x.Detail(TemplateParameter.Create<int>(), TemplateParameter.Create<int>()), Rel.SubscriberDetail, "Subscriber detail"),
                    Url.Link<SubscribersController>(x => x.Unsubscribe(TemplateParameter.Create<int>(), TemplateParameter.Create<int>()), Rel.Unsubscribe, "Unsubcribe subscriber"),
                    Url.Link<SubscribersController>(x => x.Modify(TemplateParameter.Create<int>(), TemplateParameter.Create<int>(), null), Rel.EditSubscriber, "Modify subscriber"),

                    // Hide because standard user does not need this list
                    // Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Account List"),
                }
            });
        }
    }
}
