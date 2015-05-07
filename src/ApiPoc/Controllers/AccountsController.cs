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
        public NegotiatedResult Index()
        {
            var accounts = Database.GetAccounts();
            var currentAccount = Database.GetCurrentAccount();

            return NegotiatedResult(new AccountCollection()
            {
                Links = new[] {
                    Url.LinkHome(Rel.Parent),
                    Url.LinkSelf(Rel.AccountCollection),
                    Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountDetail, "My account details")
                },
                Items = new[]
                {
                    new AccountCollectionItem() {
                        Links = new[] {
                            Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.Alternate | Rel.AccountDetail, "Account details")
                        },
                        Id = currentAccount.Id,
                        FirstName = currentAccount.FirstName,
                        LastName = currentAccount.LastName
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}")]
        public NegotiatedResult Item(int accountId)
        {
            var account = Database.GetAccountById(accountId);

            if (account == null)
            {
                var currentAccount = Database.GetCurrentAccount();
                return ErrorResult(new ErrorRepresentation()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = $"Account {accountId} not found.",
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Available accounts"),
                        Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountDetail, "My account")
                    }
                });
            }

            return NegotiatedResult(new AccountDetail()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.AccountDetail),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.SubscriberCollection, "Subscribers list"),

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
