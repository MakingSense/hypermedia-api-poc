Hypermedia API POC
==================

Conventions
-----------

To indicate HTML attributes or properties we will use [CSS selectors](https://developer.mozilla.org/en-US/docs/Web/Guide/CSS/Getting_started/Selectors) notation, for example `.id` references to elements with `id` class.

To indicate JSON properties we will use [JSONPath expressions](http://goessner.net/articles/JsonPath/), for example `$.id` references to the property `id`. 

Application models
------------------

#### All models

All models contain a list of links (HTML: `ul.links`, JSON: `$.links`) with references to _Home_, _Self_, related resources and others to help clients to follow the application flow. **It is highly recommended to use that links to control client application flow in place of use hard-coded URLs ([more information](http://roy.gbiv.com/untangled/2008/rest-apis-must-be-hypertext-driven)).**

#### Collection

All collection models in our system share some common structure:

* Should contains a list of elements 
    * HTML: `ul.items`
    * JSON: `$.items`
* May contains pagination information:
    * _FirstPage_ reference
    * _LastPage_ reference
    * _PreviousPage_ reference
    * _NextPage_ reference
    * _SpecificPage_ reference
    * Current page
        * HTML: `span.current-page`
        * JSON: `$.currentPage`
    * Page size
        * HTML: `span.page-size`
        * JSON: `$.pageSize`
    * Total items count 
        * HTML: `span.items-count`
        * JSON: `$.itemsCount`
    * Total pages count 
        * HTML: `span.pages-count`
        * JSON: `$.pagesCount`

#### Home

Singleton resource exposed in base URL. It should expose a list of most commonly used resources based on current account privileges.

#### Error

When something does not goes fine, system returns an error representation with:

* _StatusCode_
    * HTML: `.error > span.error-code`
    * JSON: `$.statusCode`   
* _Message_
    * HTML: `.error > p.message`
    * JSON: `$.message`
* It may also contain more details about the problem. 

It should include links to guide application flow, one of them marked as _Suggested_.

#### Ok

When an operation has been applied successfully, system returns a representation with _Message_ (HTML: `.done p.message`, JSON: `$.message`) and a list of links to guide application flow, one of them marked as _Suggested_


Domain Models
-------------

### Account

An account is blah blah blah 

#### AccountDetail

Exposes all account data:

* _Id_
    * HTML: `div.account-detail dd.id`
    * JSON: `$.id`
* _FirstName_ 
    * HTML: `div.account-detail dd.first-name`
    * JSON: `$.firstName`
* _LastName_ 
    * HTML: `div.account-detail dd.last-name`
    * JSON: `$.lastName`
* _Email_
    * HTML: `div.account-detail dd.email`
    * JSON: `$.email`
* _Birthday_ 
    * HTML: `div.account-detail dd.birthday`
    * JSON: `$.birthday`

It may contain links to child models like _Subscribers_ and to account operations like _UpdateAccount_.

#### AccountCollection

It is a _Collection_ model that exposes a list of accounts represented as _AccountCollectionItem_. 

* HTML: `div.accounts > ul.items`
* JSON: `$.items`

#### AccountCollectionItem

Exposes the most common account data used in lists:

* _Id_ 
    * HTML `li.account-item > span.id`
    * JSON `$.id`
* _FirstName_
    * HTML `li.account-item > span.first-name`
    * JSON `$.firstName`
* _LastName_
    * HTML `li.account-item > span.last-name`
    * JSON `$.lastName`

It should contain a link to get related _AccountDetail_.

### Subscriber

A subscriber is blah blah blah  

#### SubscriberDetail

Exposes all subscriber data:

* _Id_
    * HTML: `div,li.subscriber-detail dd.id`
    * JSON: `$.id`
* _FirstName_ 
    * HTML: `div,li.subscriber-detail dd.first-name`
    * JSON: `$.firstName`
* _LastName_ 
    * HTML: `div,li.subscriber-detail dd.last-name`
    * JSON: `$.lastName`
* _Email_
    * HTML: `div,li.subscriber-detail dd.email`
    * JSON: `$.email`
* _Birthday_ 
    * HTML: `div,li.subscriber-detail dd.birthday`
    * JSON: `$.birthday`

It may contain links to account subscribers like _UpdateSubscriber_ or _Unsubscribe_.

#### SubscriberCollection

It is a _Collection_ model that exposes a list of subscribers represented as _SubscriberCollectionItem_. 

* HTML: `div.subscribers > ul.items`
* JSON: `$.items`

#### SubscriberItem

Exposes the most common subscriber data used in lists:

* _Id_ 
    * HTML `li.subscriber-item > span.id`
    * JSON `$.id`
* _FirstName_
    * HTML `li.subscriber-item > span.first-name`
    * JSON `$.firstName`
* _LastName_
    * HTML `li.subscriber-item > span.last-name`
    * JSON `$.lastName`

It should contain a link to get related _SubscriberDetail_.

#### SubscriberDetailedCollection

Exposes a list of subscribers represented as _SubscriberDetail_.

* HTML: `div.subscribers-detailed > ul.items`
* JSON: `$.items`


Hypermedia relations
--------------------

### Outbound Application Links

#### Self

A reference to the currently loaded resource representation.

* HTML: `a.rel~="self"`
* JSON: `$.links[?(/^(.*\s)*self(\s.*)*$/.test(@.rel))]`, for example: `links: [ { "href": "/", "rel": "self home", "description": "Self", "safe": true } ]` 

#### Alternate

A reference to an alternative representation of the current element or entry.

* HTML: `a.rel~="alternate"`
* JSON: `$.links[?(/^(.*\s)*alternate(\s.*)*$/.test(@.rel))]`

#### Parent

A reference to the document or collection that governs current subordinate concept.

* HTML: `a.rel~="parent"`
* JSON: `$.links[?(/^(.*\s)*parent(\s.*)*$/.test(@.rel))]`

#### Home

A reference to _Home_ resource (base URL).

* HTML: `a.rel~="home"`
* JSON: `$.links[?(/^(.*\s)*home(\s.*)*$/.test(@.rel))]`

#### Suggested

A reference to suggested resource after an error occurred or an operation has been done.

* HTML: `a.rel~="suggested"`
* JSON: `$.links[?(/^(.*\s)*suggested(\s.*)*$/.test(@.rel))]`

#### FirstPage

A reference to the first page of current collection.

* HTML: `a.rel~="first-page"`
* JSON: `$.links[?(/^(.*\s)*first-page(\s.*)*$/.test(@.rel))]`

#### LastPage

A reference to the last page of current collection.

* HTML: `a.rel~="last-page"`
* JSON: `$.links[?(/^(.*\s)*last-page(\s.*)*$/.test(@.rel))]`

#### PreviousPage

A reference to the previous page of current collection.

* HTML: `a.rel~="previous-page"`
* JSON: `$.links[?(/^(.*\s)*first-page(\s.*)*$/.test(@.rel))]`

#### NextPage

A reference to the next page of current collection.

* HTML: `a.rel~="next-page"`
* JSON: `$.links[?(/^(.*\s)*last-page(\s.*)*$/.test(@.rel))]`

#### SpecificPage

A URI Template to build links to an arbitrary page.

<!-- TODO: resolve it with a form * HTML: `a.rel~="specific-page"` -->
* JSON: `$.links[?(/^(.*\s)*specific-page(\s.*)*$/.test(@.rel))]`

Template Parameters:

* `{page}`

### Outbound Domain Links

#### AccountDetail  

A reference to an _AccountDetail_ resource.

* HTML: `a.rel~="account-detail"`
* JSON: `$.links[?(/^(.*\s)*account-detail(\s.*)*$/.test(@.rel))]`

#### AccountCollection

A reference to an _AccountCollection_ resource.

* HTML: `a.rel~="account-collection"`
* JSON: `$.links[?(/^(.*\s)*account-collection(\s.*)*$/.test(@.rel))]`

#### SubscriberDetail

A reference to an _SubscriberDetail_ resource.

* HTML: `a.rel~="subscriber-detail"`
* JSON: `$.links[?(/^(.*\s)*subscriber-detail(\s.*)*$/.test(@.rel))]`

#### SubscriberDetailedCollection

A reference to an _SubscriberDetailedCollection_ resource.

* HTML: `a.rel~="subscriber-detailed-collection"`
* JSON: `$.links[?(/^(.*\s)*subscriber-detailed-collection(\s.*)*$/.test(@.rel))]`

### Embedding Links

_There are not embedding link relations by the moment_

### Operation Links

#### Unsubscribe

Indicates that the _DELETE_ method could be applied to the reference in order to mark an _Subscriber_ as _Unsubscribe_

<!-- TODO: resolve it with a form * HTML: `a.rel~="unsubscribe"` -->
* JSON: `$.links[?(/^(.*\s)*unsubscribe(\s.*)*$/.test(@.rel))]`

#### EditSubscriber

Indicates that the _UPDATE_ method could be applied to the reference in order to update the follow data of a _Subscriber_:

<!-- TODO: resolve it with a form * HTML: `a.rel~="edit-subscriber"` -->
* JSON: `$.links[?(/^(.*\s)*edit-subscriber(\s.*)*$/.test(@.rel))]`

Accepts: JSON representation of a _SubscriberDetail_ resource.

Updates:

* _LastName_
* _Birthday_
* _FirstName_
