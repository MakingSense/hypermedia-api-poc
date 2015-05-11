# specific-page

_(Outbound Application Link)_

A URI Template to build links to an arbitrary page.

<!-- TODO: resolve it with a form * HTML: `a.rel~="specific-page"` -->
* JSON: `$.links[?(/^(.*\s)*specific-page(\s.*)*$/.test(@.rel))]`

Template Parameters:

* `{page}`