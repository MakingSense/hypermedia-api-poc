# edit-subscriber

_(Operation Link)_

Indicates that the _UPDATE_ method could be applied to the reference in order to update the follow data of a [Subscriber](../concepts/Subscriber.md):

* HTML: `a.rel~="edit-subscriber"`
* JSON: `$.links[?(/^(.*\s)*edit-subscriber(\s.*)*$/.test(@.rel))]`

Accepts: JSON representation of a [SubscriberDetail](../models/SubscriberDetail.md) resource.

Requires and Updates the follow fields:

* _LastName_
* _Birthday_
* _FirstName_