using ApiPoc.Helpers;
using ApiPoc.PersistenceModel;
using ApiPoc.Representations;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.WebUtilities;
using System;
using System.Linq;

namespace ApiPoc.Controllers
{
    public class SubscribersController : BaseController
    {
        public SubscribersController(IDatabase database)
            : base(database)
        {
        }

        [HttpGet("/accounts/{accountId}/subscribers")]
        public IActionResult Index(int accountId)
        {
            var account = Database.GetAccountById(accountId);

            if (account == null)
            {
                var currentAccount = Database.GetCurrentAccount();
                return new NegotiatedResult(new ErrorRepresentation()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Account {accountId} not found.",
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Available accounts"),
                        Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountItem, "My account"),
                        Url.Link<SubscribersController>(x => x.Index(currentAccount.Id), Rel.SubscriberCollection, "My account subscribers")
                    }
                });
            }

            return new NegotiatedResult(new SubscriberCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberCollection),
                    Url.Link<AccountsController>(x => x.Item(account.Id), Rel.Parent | Rel.AccountCollection, "Account details"),
                    Url.Link<SubscribersController>(x => x.DetailedIndex(account.Id), Rel.SubscriberCollection, "Subscribers list (detailed)"),
                },
                Items = account.Subscribers.Select(subscriber => 
                    new SubscriberRepresentation() {
                        Id = subscriber.Id,
                        Links = new[] {
                            Url.Link<SubscribersController>(x => x.Item(account.Id, subscriber.Id), Rel.Self | Rel.SubscriberItem, "Subscriber details")
                        },
                        FirstName = subscriber.FirstName,
                        LastName = subscriber.LastName
                    }).ToArray()
            });
        }

        [HttpGet("/accounts/{accountId}/subscribers/detail")]
        public IActionResult DetailedIndex(int accountId)
        {
            var account = Database.GetAccountById(accountId);

            if (account == null)
            {
                var currentAccount = Database.GetCurrentAccount();
                return new NegotiatedResult(new ErrorRepresentation()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Account {accountId} not found.",
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Available accounts"),
                        Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountItem, "My account"),
                        Url.Link<SubscribersController>(x => x.DetailedIndex(currentAccount.Id), Rel.SubscriberCollection, "My account subscribers (detailed)")
                    }
                });
            }

            return new NegotiatedResult(new SubscriberCollectionRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberDetailCollection),
                    Url.Link<AccountsController>(x => x.Item(account.Id), Rel.Parent | Rel.AccountCollection, "Account details"),
                    Url.Link<SubscribersController>(x => x.Index(account.Id), Rel.SubscriberCollection, "Subscribers list (simple)"),
                },
                Items = account.Subscribers.Select(subscriber =>
                    new SubscriberRepresentation()
                    {
                        Id = subscriber.Id,
                        Links = new[] {
                            Url.Link<SubscribersController>(x => x.Item(accountId, subscriber.Id), Rel.Self | Rel.SubscriberItem, "Subscriber details")
                        },
                        FirstName = subscriber.FirstName,
                        LastName = subscriber.LastName,
                        Birthday = subscriber.Birthday,
                        Email = subscriber.Email
                    }).ToArray()
            });
        }

        [HttpGet("/accounts/{accountId}/subscribers/{subscriberId}")]
        public IActionResult Item(int accountId, int subscriberId)
        {
            var account = Database.GetAccountById(accountId);
            if (account == null)
            {
                var currentAccount = Database.GetCurrentAccount();
                return new NegotiatedResult(new ErrorRepresentation()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Account {accountId} not found.",
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<AccountsController>(x => x.Index(), Rel.AccountCollection, "Available accounts"),
                        Url.Link<AccountsController>(x => x.Item(currentAccount.Id), Rel.AccountItem, "My account"),
                        Url.Link<SubscribersController>(x => x.Index(currentAccount.Id), Rel.SubscriberCollection, "My account subscribers")
                    }
                });
            }

            var subscriber = account.Subscribers.FirstOrDefault(x => x.Id == subscriberId);
            if (subscriber == null)
            {
                return new NegotiatedResult(new ErrorRepresentation()
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = $"Subscriber {subscriberId} does not exist for account {accountId}.",
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<SubscribersController>(x => x.Index(accountId), Rel.Parent | Rel.SubscriberCollection, "Subscribers list"),
                    }
                });
            }

            return new NegotiatedResult(new SubscriberRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberItem),
                    Url.Link<SubscribersController>(x => x.Index(account.Id), Rel.Parent | Rel.SubscriberCollection, "Subscribers list"),
                },
                Id = subscriber.Id,
                FirstName = subscriber.FirstName,
                LastName = subscriber.LastName,
                Email = subscriber.Email,
                Birthday = subscriber.Birthday
            });
        }
    }
}
