# account-detail

_(Outbound Domain Link)_

A reference to an [AccountDetail](../models/AccountDetail.md) resource to represent an [Account](../concepts/Account.md).

* HTML: `a.rel~="account-detail"`
* JSON: `$.links[?(/^(.*\s)*account-detail(\s.*)*$/.test(@.rel))]`