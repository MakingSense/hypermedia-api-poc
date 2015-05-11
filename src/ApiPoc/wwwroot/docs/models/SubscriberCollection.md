# SubscriberCollection

It is a [Collection](Collection.md) model that exposes a list of subscribers represented as _SubscriberCollectionItem_. 

* HTML: `div.subscribers > ul.items`
* JSON: `$.items`

## SubscriberCollectionItem

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

It should contain a link to get related [SubscriberDetail](SubscriberDetail.md).