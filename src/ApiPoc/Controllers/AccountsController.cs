using ApiPoc.Helpers;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using System;

namespace ApiPoc.Controllers
{
    public class AccountsController : BaseController
    {
        private const int CURRENT_ACCOUNT_ID = 128;

        [HttpGet("/accounts")]
        public IActionResult Index()
        {
            return Negotiated(new AccountCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(Rel.Parent),
                    Url.LinkSelf(Rel.AccountCollection),
                    Url.Link<AccountsController>(x => x.Item(CURRENT_ACCOUNT_ID), Rel.AccountItem, "My account details")
                },
                Items = new[]
                {
                    new AccountRepresentation() {
                        Links = new[] {
                            Url.Link<AccountsController>(x => x.Item(CURRENT_ACCOUNT_ID), Rel.Self | Rel.AccountItem, "Account details")
                        },
                        Id = CURRENT_ACCOUNT_ID,
                        FirstName = "Andrés",
                        LastName = "Moschini"
                    }
                }
            });
        }

        [HttpGet("/accounts/{accountId}")]
        public IActionResult Item(int accountId)
        {
            return Negotiated(new AccountRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.AccountItem),
                    Url.Link<SubscribersController>(x => x.Index(accountId), Rel.SubscriberCollection, "Subscribers list"),

                    // Hide because standard user does not need this list
                    // Url.Link<AccountsController>(x => x.Index(), Rel.Parent | Rel.AccountItem, "Accounts list"),
                },
                Id = accountId,
                FirstName = "Andrés",
                LastName = "Moschini",
                Email = "private@andresmoschini.com",
                Birthday = DateTime.Parse("1978-12-02")
            });
        }
    }
}
