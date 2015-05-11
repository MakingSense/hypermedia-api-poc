# subscriber-detailed-collection

_(Outbound Domain Link)_

A reference to an [SubscriberDetailedCollection](../models/SubscriberDetailedCollection.md) resource related to [subscribers](../concepts/Subscriber.md).

* HTML: `a.rel~="subscriber-detailed-collection"`
* JSON: `$.links[?(/^(.*\s)*subscriber-detailed-collection(\s.*)*$/.test(@.rel))]`