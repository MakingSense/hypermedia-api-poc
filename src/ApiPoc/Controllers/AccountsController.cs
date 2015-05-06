using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using System;
using System.Linq;

namespace ApiPoc.Controllers
{
    public class AccountsController: BaseController
    {
        public AccountsController(IDatabase database)
            : base(database)
        {
        }

        [HttpGet("/accounts")]
        public NegotiatedResult Index()
        {
            var accounts = Database.GetAccounts();
            var currentAccount = Database.GetCurrentAccount();

            return NegotiatedResult(new AccountCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(Rel.Parent),
                    Url.LinkSelf(Rel.AccountCollection),
                    Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountItem, "My account details")
                },
                Items = new[]
                {
                    new AccountRepresentation() {
                        Links = new[] {
                            Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.Self | Rel.AccountItem, "Account details")
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
                        Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountItem, "My account")
                    }
                });
            }

            return NegotiatedResult(new AccountRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.AccountItem),
                    Url.Link<SubscribersController>(x => x.Index(account.Id), Rel.SubscriberCollection, "Subscribers list"),

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
