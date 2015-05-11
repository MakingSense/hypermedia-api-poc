# parent

_(Outbound Application Link)_

A reference to the document or collection that governs current subordinate concept.

* HTML: `a.rel~="parent"`
* JSON: `$.links[?(/^(.*\s)*parent(\s.*)*$/.test(@.rel))]`