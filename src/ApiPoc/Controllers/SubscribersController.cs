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
    public class SubscribersController : BaseController
    {
        public SubscribersController(IDatabase database, IOptions<AppSettings> settings)
            : base(database, settings)
        {
        }

        [HttpGet("/accounts/{accountId}/subscribers")]
        public NegotiatedResult Index(int accountId, int? page = null)
        {
            page = page ?? 1;
            var account = Database.GetAccountById(accountId);
            if (account == null)
            {
                return AccountNotFoundError(accountId);
            }

            var allItems = account.Subscribers.Select(subscriber =>
                new SubscriberCollectionItem()
                {
                    Id = subscriber.Id,
                    Links = new[] {
                        Url.Link<SubscribersController>(x => x.Detail(account.Id, subscriber.Id), Rel.Alternate | Rel.SubscriberDetail, "Subscriber details")
                    },
                    FirstName = subscriber.FirstName,
                    LastName = subscriber.LastName
                });

            return NegotiatedResult(new SubscriberCollection(
                allItems, 
                Settings.Options.PageSize, 
                page.Value, 
                (p, rel, description) => Url.Link<SubscribersController>(x => x.Index(accountId, p), rel | Rel.SubscriberCollection, description),
                Url.LinkHome(),
                Url.LinkSelf(Rel.SubscriberCollection),
                Url.Link<AccountsController>(x => x.Detail(account.Id), Rel.Parent | Rel.AccountCollection, "Account details"),
                Url.Link<SubscribersController>(x => x.DetailedIndex(account.Id, null), Rel.SubscriberCollection, "Subscribers list (detailed)")));
        }

        [HttpGet("/accounts/{accountId}/subscribers/detail")]
        public NegotiatedResult DetailedIndex(int accountId, int? page = null)
        {
            page = page ?? 1;
            var account = Database.GetAccountById(accountId);
            if (account == null)
            {
                return AccountNotFoundError(accountId);
            }

            var allItems = account.Subscribers.Select(subscriber =>
                    new SubscriberDetail()
                    {
                        Id = subscriber.Id,
                        Links = new[] {
                            Url.Link<SubscribersController>(x => x.Detail(accountId, subscriber.Id), Rel.Self | Rel.SubscriberDetail, "Subscriber details"),
                            Url.Link<SubscribersController>(x => x.Unsubscribe(accountId, subscriber.Id), Rel.Unsubscribe, "Unsubscribe"),
                            Url.Link<SubscribersController>(x => x.Modify(accountId, subscriber.Id, null), Rel.EditSubscriber, "Edit"),
                        },
                        FirstName = subscriber.FirstName,
                        LastName = subscriber.LastName,
                        Birthday = subscriber.Birthday,
                        Email = subscriber.Email
                    });

            return NegotiatedResult(new SubscriberDetailedCollection(
                allItems,
                Settings.Options.PageSize,
                page.Value,
                (p, rel, description) => Url.Link<SubscribersController>(x => x.Index(accountId, p), rel | Rel.SubscriberDetailedCollection, description),
                Url.LinkHome(),
                Url.LinkSelf(Rel.SubscriberDetailedCollection),
                Url.Link<AccountsController>(x => x.Detail(account.Id), Rel.Parent | Rel.AccountCollection, "Account details"),
                Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.SubscriberCollection, "Subscribers list (simple)")));
        }

        [HttpGet("/accounts/{accountId}/subscribers/{subscriberId}")]
        public NegotiatedResult Detail(int accountId, int subscriberId)
        {
            var account = Database.GetAccountById(accountId);
            if (account == null)
            {
                return AccountNotFoundError(accountId);
            }

            var subscriber = account.Subscribers.FirstOrDefault(x => x.Id == subscriberId);
            if (subscriber == null)
            {
                return SubscriberNotFoundError(accountId, subscriberId);
            }

            return NegotiatedResult(new SubscriberDetail()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.LinkSelf(Rel.SubscriberDetail),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent | Rel.SubscriberCollection, "Subscribers list"),
                    Url.Link<SubscribersController>(x => x.Unsubscribe(accountId, subscriberId), Rel.Unsubscribe, "Unsubscribe"),
                    Url.Link<SubscribersController>(x => x.Modify(accountId, subscriber.Id, null), Rel.EditSubscriber, "Edit"),
                },
                Id = subscriber.Id,
                FirstName = subscriber.FirstName,
                LastName = subscriber.LastName,
                Email = subscriber.Email,
                Birthday = subscriber.Birthday
            });
        }

        [HttpPut("/accounts/{accountId}/subscribers/{subscriberId}")]
        public NegotiatedResult Modify(int accountId, int subscriberId, [FromBody]SubscriberDetail updated)
        {
            //TODO: add optimistic concurrency check

            var account = Database.GetAccountById(accountId);
            if (account == null)
            {
                return AccountNotFoundError(accountId);
            }

            var subscriber = account.Subscribers.FirstOrDefault(x => x.Id == subscriberId);
            if (subscriber == null)
            {
                return SubscriberNotFoundError(accountId, subscriberId);
            }

            subscriber.Birthday = updated.Birthday;
            subscriber.FirstName = updated.FirstName;
            subscriber.LastName = updated.LastName;

            return DoneResult(new OkRepresentation()
            {
                Links = new[] {
                    Url.LinkHome(),
                    Url.Link<SubscribersController>(x => x.Detail(account.Id, subscriber.Id), Rel.SubscriberDetail | Rel.Suggested, "Subscriber"),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent | Rel.SubscriberCollection, "Subscribers list")
                }
            });
        }

        [HttpDelete("/accounts/{accountId}/subscribers/{subscriberId}")]
        public NegotiatedResult Unsubscribe(int accountId, int subscriberId)
        {
            //TODO: add optimistic concurrency check

            var account = Database.GetAccountById(accountId);
            if (account == null)
            {
                return AccountNotFoundError(accountId);
            }

            var subscriber = account.AllSubscribers.FirstOrDefault(x => x.Id == subscriberId);
            if (subscriber == null)
            {
                return SubscriberNotFoundError(accountId, subscriberId);
            }

            if (subscriber.Unsubscribed)
            {
                return NotModifiedResult(new OkRepresentation()
                {
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent | Rel.SubscriberCollection | Rel.Suggested, "Subscribers list")
                    }
                });
            }
            else
            {
                subscriber.Unsubscribed = true;

                return DoneResult(new OkRepresentation()
                {
                    Links = new[]
                    {
                        Url.LinkHome(),
                        Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent | Rel.SubscriberCollection | Rel.Suggested, "Subscribers list"),
                    }
                });
            }
        }

        private ErrorResult SubscriberNotFoundError(int accountId, int subscriberId)
        {
            return ErrorResult(new ErrorRepresentation()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"Subscriber {subscriberId} does not exist for account {accountId}.",
                Links = new[]
                {
                    Url.LinkHome(),
                    Url.Link<SubscribersController>(x => x.Index(accountId, null), Rel.Parent | Rel.SubscriberCollection, "Subscribers list"),
                }
            });
        }

        private ErrorResult AccountNotFoundError(int accountId)
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
                    Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), Rel.AccountDetail, "My account"),
                    Url.Link<SubscribersController>(x => x.Index(currentAccount.Id, null), Rel.SubscriberCollection, "My account subscribers")
                }
            });
        }
    }
}
