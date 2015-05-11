# subscriber-detail

_(Outbound Domain Link)_

A reference to an [SubscriberDetail](../models/SubscriberDetail.md) resource related to a [Subscriber](../concepts/Subscriber.md).

* HTML: `a.rel~="subscriber-detail"`
* JSON: `$.links[?(/^(.*\s)*subscriber-detail(\s.*)*$/.test(@.rel))]`