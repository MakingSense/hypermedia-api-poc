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
        [LinkDescription(Rel.SubscriberCollection, "Subscribers list")]
        public NegotiatedResult Index(int accountId, int? page = null)
        {
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
                        Url.Link<SubscribersController>(x => x.Detail(account.Id, subscriber.Id), Rel.Alternate)
                    },
                    FirstName = subscriber.FirstName,
                    LastName = subscriber.LastName
                });

            return NegotiatedResult(new SubscriberCollection(
                allItems, 
                Settings.Options.PageSize,
                page ?? 1, 
                (p, rel, description) => Url.Link<SubscribersController>(x => x.Index(accountId, p), rel, description),
                Url.LinkHome(),
                Url.LinkSelf<SubscribersController>(x => x.Index(accountId, page)),
                Url.Link<AccountsController>(x => x.Detail(account.Id), Rel.Parent, "Parent account details"),
                Url.Link<SubscribersController>(x => x.DetailedIndex(account.Id, null), Rel.Alternate)));
        }

        [HttpGet("/accounts/{accountId}/subscribers/detail")]
        [LinkDescription(Rel.SubscriberDetailedCollection, "Subscribers list (detailed)")]
        public NegotiatedResult DetailedIndex(int accountId, int? page = null)
        {
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
                            Url.LinkSelf<SubscribersController>(x => x.Detail(accountId, subscriber.Id)),
                            Url.Link<SubscribersController>(x => x.Unsubscribe(accountId, subscriber.Id)),
                            Url.Link<SubscribersController>(x => x.Modify(accountId, subscriber.Id, null)),
                        },
                        FirstName = subscriber.FirstName,
                        LastName = subscriber.LastName,
                        Birthday = subscriber.Birthday,
                        Email = subscriber.Email
                    });

            return NegotiatedResult(new SubscriberDetailedCollection(
                allItems,
                Settings.Options.PageSize,
                page ?? 1,
                (p, rel, description) => Url.Link<SubscribersController>(x => x.Index(accountId, p), rel, description),
                Url.LinkHome(),
                Url.LinkSelf<SubscribersController>(x => x.DetailedIndex(accountId, page)),
                Url.Link<AccountsController>(x => x.Detail(account.Id), Rel.Parent, "Parent account details"),
                Url.Link<SubscribersController>(x => x.Index(account.Id, null), description: "Subscribers list (simple)")));
        }

        [HttpGet("/accounts/{accountId}/subscribers/{subscriberId}")]
        [LinkDescription(Rel.SubscriberDetail, "Subscriber detail")]
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
                    Url.LinkSelf<SubscribersController>(x => x.Detail(accountId, subscriberId)),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent),
                    Url.Link<SubscribersController>(x => x.Unsubscribe(accountId, subscriberId)),
                    Url.Link<SubscribersController>(x => x.Modify(accountId, subscriber.Id, null)),
                },
                Id = subscriber.Id,
                FirstName = subscriber.FirstName,
                LastName = subscriber.LastName,
                Email = subscriber.Email,
                Birthday = subscriber.Birthday
            });
        }

        [HttpPut("/accounts/{accountId}/subscribers/{subscriberId}")]
        [LinkDescription(Rel.EditSubscriber, "Modify subscriber")]
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

            return NegotiatedResult(new Message("Subscriber modified!")
            {
                Links = new[] 
                {
                    Url.LinkHome(),
                    Url.Link<SubscribersController>(x => x.Detail(account.Id, subscriber.Id)),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent)
                }
            });
        }

        [HttpDelete("/accounts/{accountId}/subscribers/{subscriberId}")]
        [LinkDescription(Rel.Unsubscribe, "Unsubscribe")]
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

            var alreadyUnsubscribed = subscriber.Unsubscribed;
            subscriber.Unsubscribed = true;

            return NegotiatedResult(new Message(subscriber.Unsubscribed ? "Subscriber already unsubscribed" : "Subscriber unsubscribed successfully")
            {
                Links = new[]
                {
                    Url.LinkHome(),
                    Url.Link<SubscribersController>(x => x.Index(account.Id, null), Rel.Parent | Rel.Suggested),
                }
            });
        }

        private NegotiatedResult SubscriberNotFoundError(int accountId, int subscriberId)
        {
            return NegotiatedResult(new Error($"Subscriber {subscriberId} does not exist for account {accountId}.", StatusCodes.Status404NotFound)
            {
                Links = new[]
                {
                    Url.LinkHome(),
                    Url.Link<SubscribersController>(x => x.Index(accountId, null), Rel.Parent),
                }
            });
        }

        private NegotiatedResult AccountNotFoundError(int accountId)
        {
            var currentAccount = Database.GetCurrentAccount();
            return NegotiatedResult(new Error($"Account {accountId} not found.", StatusCodes.Status404NotFound)
            {
                Links = new[]
                {
                    Url.LinkHome(),
                    Url.Link<AccountsController>(x => x.Index()),
                    Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), description: "My account details"),
                    Url.Link<SubscribersController>(x => x.Index(currentAccount.Id, null), description: "My account subscribers")
                }
            });
        }
    }
}
