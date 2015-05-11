using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using Microsoft.Framework.OptionsModel;
using System;
using System.Linq;

namespace ApiPoc.Controllers
{
    public class AccountsController: BaseController
    {
        public AccountsController(IDatabase database, IOptions<AppSettings> settings)
            : base(database, settings)
        {
        }

        [HttpGet("/accounts")]
        [LinkDescription(Rel.AccountCollection, "Available accounts")]
        public NegotiatedResult Index()
        {
            var accounts = Database.GetAccounts();
            var currentAccount = Database.GetCurrentAccount();

            return NegotiatedResult(new AccountCollection()
            {
                Links = new[] {
                    Url.LinkHome(Rel.Parent),
                    Url.LinkSelf<AccountsController>(x => x.Index()),
                    Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), description: "My account details")
                },
                Items = new[]
                {
                    new AccountCollectionItem() {
                        Links = new[] {
                            Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), Rel.Alternate)
                        },
                        Id = currentAccount.Id,
                        FirstName = currentAccount.FirstName,
                        LastName = currentAccount.LastName
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}")]
        [LinkDescription(Rel.AccountDetail, "Account detail")]
        public NegotiatedResult Detail(int accountId)
        {
            var account = Database.GetAccountById(accountId);

            if (account == null)
            {
                var currentAccount = Database.GetCurrentAccount();
                return NegotiatedResult(new Error($"Account {accountId} not found.", StatusCodes.Status404NotFound)
                {
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<AccountsController>(x => x.Index()),
                        Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), description: "My account details")
                    }
                });
            }

            return NegotiatedResult(new AccountDetail()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf<AccountsController>(x => x.Detail(accountId)),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null)),

                    Url.Link<SubscribersController>(x => x.Detail(account.Id, TemplateParameter.Create<int>())),
                    Url.Link<SubscribersController>(x => x.Unsubscribe(account.Id, TemplateParameter.Create<int>())),
                    Url.Link<SubscribersController>(x => x.Modify(account.Id, TemplateParameter.Create<int>(), null)),
                    
                    // Hide because standard user does not need this list
                    // Url.Link<AccountsController>(x => x.Index(), Rel.Parent | Rel.AccountItem, "Accounts list"),
                },
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Birthday = account.Birthday
            });
        }
    }
}
