# AccountCollection

It is a [Collection](Collection.md) model that exposes a list of [accounts](../concepts/Account.md) represented as _AccountCollectionItem_. 

* HTML: `div.accounts > ul.items`
* JSON: `$.items`

# AccountCollectionItem

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

It should contain a link to get related [AccountDetail](AccountDetail.md).