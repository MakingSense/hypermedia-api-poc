Hypermedia API POC
==================

Conventions
-----------

To indicate HTML attributes or properties we will use [CSS selectors](https://developer.mozilla.org/en-US/docs/Web/Guide/CSS/Getting_started/Selectors) notation, for example `.id` references to elements with `id` class.

To indicate JSON properties we will use [JSONPath expressions](http://goessner.net/articles/JsonPath/), for example `$.id` references to the property `id`. 

Domain Concepts
---------------

* [Account](concepts/Account.md)
* [Subscriber](concepts/Subscriber.md)

Models
------

All models contain a list of links (HTML: `ul.links`, JSON: `$.links`) with references to _Home_, _Self_, related resources and others to help clients to follow the application flow. **It is highly recommended to use that links to control client application flow in place of use hard-coded URLs ([more information](http://roy.gbiv.com/untangled/2008/rest-apis-must-be-hypertext-driven)).**

* Application Models
    * [Collection](models/Collection.md)
    * [Error](models/Error.md)
    * [Message](models/Message.md)
    * [Home](models/Home.md)
* Domain Models
    * [AccountDetail](models/AccountDetail.md)
    * [AccountCollection](models/AccountCollection.md)
    * [SubscriberDetail](models/SubscriberDetail.md)
    * [SubscriberCollection](models/SubscriberCollection.md)
    * [SubscriberDetailedCollection](models/SubscriberDetailedCollection.md)


Hypermedia relations
--------------------

* Outbound Application Links
    * [self](rels/self.md)
    * [alternate](rels/alternate.md) 
    * [parent](rels/parent.md)
    * [home](rels/home.md)
    * [suggested](rels/suggested.md)
    * [first-page](rels/first-page.md)
    * [last-page](rels/last-page.md)
    * [previous-page](rels/previous-page.md)
    * [next-page](rels/next-page.md)
    * [specific-page](rels/specific-page.md) 
* Outbound Domain Links
    * [account-detail](rels/account-detail.md)
    * [account-collection](rels/account-collection.md)
    * [subscriber-detail](rels/subscriber-detail.md)
    * [subscriber-collection](rels/subscriber-collection.md)
    * [subscriber-detailed-collection](rels/subscriber-detailed-collection.md)
* Embedding Links
    * _There are not embedding link relations by the moment_
* Operation Links
    * [unsubscribe](rels/unsubscribe.md)
    * [edit-subscriber](rels/edit-subscriber.md)


