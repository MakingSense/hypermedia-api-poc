# account-collection

_(Outbound Domain Link)_

A reference to an [AccountCollection](../models/AccountCollection.md) resource to represent a list of [Accounts](../concepts/Account.md)

* HTML: `a.rel~="account-collection"`
* JSON: `$.links[?(/^(.*\s)*account-collection(\s.*)*$/.test(@.rel))]`