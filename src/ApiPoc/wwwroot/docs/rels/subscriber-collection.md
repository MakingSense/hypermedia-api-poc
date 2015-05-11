# subscriber-collection

_(Outbound Domain Link)_

A reference to an [SubscriberCollection](../models/SubscriberCollection.md) representation of a [Subscriber](../concepts/Subscriber.md) set.

* HTML: `a.rel~="subscriber-collection"`
* JSON: `$.links[?(/^(.*\s)*subscriber-collection(\s.*)*$/.test(@.rel))]`