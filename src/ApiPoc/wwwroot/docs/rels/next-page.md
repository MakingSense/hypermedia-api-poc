# next-page

_(Outbound Application Link)_

A reference to the next page of current collection.

* HTML: `a.rel~="next-page"`
* JSON: `$.links[?(/^(.*\s)*last-page(\s.*)*$/.test(@.rel))]`