# unsubscribe

_(Operation Link)_

Indicates that the _DELETE_ method could be applied to the reference in order to mark an [Subscriber](../concepts/Subscriber.md) as _Unsubscribed_

* HTML: `a.rel~="unsubscribe"`
* JSON: `$.links[?(/^(.*\s)*unsubscribe(\s.*)*$/.test(@.rel))]`