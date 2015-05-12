Hypermedia API POC
==================

See also [API Reference](src/ApiPoc/wwwroot/docs/documentation.md).


How to run it
-------------

Download the [last release file](https://github.com/andresmoschini/hypermedia-api-poc/releases), decompress in any folder and run `web.cmd` (or `web` on linux or mac).

Open `http://localhost:5000` in your web browser.


Objective 
---------

The idea behind this proof of concept was to try to implement some of the concepts related to RESTful API (aka [Richardson Maturity Model Level 3](martinfowler.com/articles/richardsonMaturityModel.html#level3) or [Hypermedia API](http://blog.steveklabnik.com/posts/2012-02-23-rest-is-over)) in order to learn how difficult is to implement and maintain it and how useful could be this kind of API for a client.


Desired approach
----------------

Since in general I am working over Microsoft stack, and I like the way that they are taken, I choose _ASP.NET 5.0 MVC 6_ to implement it. Like other tools _ASP.NET 5.0 MVC 6_ to is very helpful and encourage to serialize existing programming objects into a common format (like JSON) and send the content to another party. 

> This method of marshaling internal types over HTTP can easily lead to brittle and inflexible
> implementations on the Web. Often, introduction of new arguments for requesting data, new
> addressees to which requests must be targeted, and/or new data
> elements in the response messages will cause a mismatch between parties that can only
> be resolved by reprogramming one or more participants on the network. This is the
> antithesis of how the Web was designed to work. 
> ([Building Hypermedia APIs with HTML5 and Node, Mike Amundsen, 2012](http://shop.oreilly.com/product/0636920020530.do))

For that reason my idea is not use it as an automatic mapping layer, instead to implement the mappings explicitly and expose a real hypermedia API.

To ensure that I am taking the right path, I will expose both JSON and HTML results, and allow user to fully use the API within a browser. It will be useful to identify what implicit thinks should be documented, for example expected data for unsafe methods. It cold be also useful in a definitive implementation, for example for didactic purposes and as a playground.

Research progress has been documented in a [Trello Board](https://trello.com/b/RxHiZuvz/hypermedia-api-research), and this document have the conclusions and current status of this POC.


Hypermedia
----------

All representations will include links to related resources, each of them with the follow properties:

* `href`: URI of the related resource, it could be a template.
* `description`: Human readable description of the role and meaning of the link.
* `rel`:  List of typified strings that represents the relations with current resource and also the role, behavior and constraints o the link. Documented in [hypermedia-relations](https://github.com/andresmoschini/hypermedia-api-poc/blob/master/src/ApiPoc/wwwroot/docs/documentation.md#hypermedia-relations)

Links could represent different kinds of hypermedia factors (_Embedding Links_, _Outbound Links_, _Templated Links_, _Idempotent Links_ and _Non-Idempotent Links_), this information is defined in [hypermedia-relations](https://github.com/andresmoschini/hypermedia-api-poc/blob/master/src/ApiPoc/wwwroot/docs/documentation.md#hypermedia-relations) documentation, in JSON representions all of them are rendered in the same way, but in HTML representation _Embedding Links_ and _Outbound Links_ are rendered as `a` elements, and _Templated Links_, _Idempotent Links_ and _Non-Idempotent Links_ are rendered as _form_ elements.


### Implementation

There is a base model class that exposes a list of links. This list is filled on each controller explicitly, allowing to create them smartly. 

    public class HomeController
    {
        [HttpGet("/")]
        [LinkDescription(Rel.Home, "Home")]
        public NegotiatedResult Index()
        {
            var links = new List<Link>() {
                Url.Link<Home>(x => x.Index(), Rel.Self),
                Url.Link<AccountsController>(x => x.Detail(currentAccount.Id), "Current account details"),
                Url.Link<SubscribersController>(x => x.Index(currentAccount.Id, null), "Current account Subscribers")
            };
            if (currentUser.HasManyAccounts)
            {
                links.Add(url.Link<AccountsController>(x => x.Index(), "All accounts"));
            }

            return NegotiatedResult(new Home()
            {
                Links = links.ToArray()
            }
        }
    }


#### Links generation

To create the links I am using a self made helpers to create URIs according to the application's route configuration in a type-safe manner. The idea was to use [Hyprlinkr](https://github.com/ploeh/Hyprlinkr) but it is not working on _ASP.NET 5.0 MVC 6_. On the other hand, use my own helpers allowed me to read controller annotations in order to generate base relations automatically, see `LinkDescription` annotation in the example. 

#### HTML representation

On link representation we have enough information to render _Embedding Links_ and _Outbound Links_ as `a` elements and _Templated Links_ as `form` elements extracting URI parameters parsig the URI, But some _Idempotent Links_ and _Non-Idempotent Links_ have some parameters only defined on [hypermedia-relations](https://github.com/andresmoschini/hypermedia-api-poc/blob/master/src/ApiPoc/wwwroot/docs/documentation.md#hypermedia-relations) documentation. In that cases, I am using a partial view for each of them.

By the moment template resolution is not so smart, and requires changes on adding new relations:

    <ul class="links">
    @foreach (var link in plainLinks)
    {
        <li class="link-item">@Html.Link(link)</li>
    }
    </ul>

    <ul class="template-links">
    @foreach (var link in templateLinks)
    {
        <li class="link-item">@Html.EmptyForm(link)</li>
    }
    </ul>

    <ul class="unsafe-links">
    @foreach (var link in unsafeLinks)
    {
        <li class="link-item">
            <!-- Changes required here on adding new relations -->
            @if (link.Rel.Is(Rel.EditSubscriber))
            {
                @Html.Partial("Forms/EditSubscriber", new LinkWithParent(link, Model))
            }
            else
            {
                @Html.EmptyForm(link)
            }
        </li>
    }
    </ul>


Errors
------

> All hypermedia designs should include the ability to render details about a particular error.
> While HTTP supports returning response codes to indicate possible error states (`4xx` and `5xx`
> codes), these are protocol-level indicators and are usually inadequate for expressing the details
> of the application level error itself or any possible suggestions for remediation of the error,
> retry options, etc. 
> ([Building Hypermedia APIs with HTML5 and Node, Mike Amundsen, 2012](http://shop.oreilly.com/product/0636920020530.do))

So, the idea is to have an own specific representation for errors.


### Implementation

My first idea was to have actions result typed as expected object and dealing with error responses as exceptions mapping them to output representation within a filter, for example:

    public class AccountController
    {
        public Account Detail(int accountId)
        {
            var account = DB.get(accountId);
            if (account == null) {
                throw new NotFound404Exception($"Account {accountId} not found.");
            }
            else
            {
                return account;
            }
        }
    }

But then I realized that sometimes errors are part of the normal flow, and also responses are not always representation of the same class of elements. So, I choose to loose result type in order to obtain flexibility:

    public class Account
    {
        public IActionResult Detail(int accountId)
        {
            var account = DB.get(accountId);
            if (account == null) {
                return new Error(StatusCodes.Status404NotFound, $"Account {accountId} not found.");
            }
            else
            {
                return new ObjectResult(account);
            }
        }
    }


Content type negotiation
------------------------

As I explained before, the idea is to expose both JSON and HTML content. JSON results are simple JSON serializations of the object that represents the response. HTML, instead, have a some HTML layout based on the kind of represented model, well formed links using `a` or `form` elements depending of the link type. <!-- WORK IN PROGRESS --> 




