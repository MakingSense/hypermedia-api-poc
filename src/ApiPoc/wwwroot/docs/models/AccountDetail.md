# AccountDetail

Exposes all [account](../concepts/Account.md) data:

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

It may contain links to child models like [subscribers](../rels/subscriber-collection.md) and to account operations.