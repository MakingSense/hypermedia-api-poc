# self

_(Outbound Application Link)_

A reference to the currently loaded resource representation.

* HTML: `a.rel~="self"`
* JSON: `$.links[?(/^(.*\s)*self(\s.*)*$/.test(@.rel))]`, for example: `links: [ { "href": "/", "rel": "self home", "description": "Self", "safe": true } ]` 