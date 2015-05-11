# suggested

_(Outbound Application Link)_

A reference to suggested resource after an error occurred or an operation has been done.

* HTML: `a.rel~="suggested"`
* JSON: `$.links[?(/^(.*\s)*suggested(\s.*)*$/.test(@.rel))]`