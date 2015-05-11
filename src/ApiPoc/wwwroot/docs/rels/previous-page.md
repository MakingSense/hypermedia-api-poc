# previous-page

_(Outbound Application Link)_

A reference to the previous page of current collection.

* HTML: `a.rel~="previous-page"`
* JSON: `$.links[?(/^(.*\s)*first-page(\s.*)*$/.test(@.rel))]`