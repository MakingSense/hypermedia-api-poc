var ApiPoc;
(function (ApiPoc) {
    var LinkRepresentation = (function () {
        function LinkRepresentation() {
        }
        return LinkRepresentation;
    })();
    function searchLinkByRel(links, rel) {
        for (var _i = 0; _i < links.length; _i++) {
            var link = links[_i];
            var rels = link.rel.split(" ");
            if (rels.some(function (x) { return x == rel; })) {
                return link;
            }
        }
        return null;
    }
    function ajax(method, url, onSuccess, onError) {
        if (onSuccess === void 0) { onSuccess = null; }
        if (onError === void 0) { onError = null; }
        var request = new XMLHttpRequest();
        request.onload = function (a) {
            try {
                var parsedResult = JSON.parse(request.responseText);
                if (onSuccess && request.status >= 200 && request.status < 400) {
                    onSuccess(request.status, parsedResult, request.responseText);
                }
                else if (onError) {
                    onError(request.status, parsedResult, request.responseText);
                }
            }
            catch (e) {
                onError(request.status, { message: "Parsing error", exception: e }, request.responseText);
            }
        };
        request.open(method, url, true);
        request.setRequestHeader("accept", "text/json");
        request.send();
    }
    function DELETE(url) {
        ajax("DELETE", url, function (status, data, responseText) {
            alert("Success!");
            if (data && data.links) {
                var redirectLink = searchLinkByRel(data.links, "suggested");
                if (redirectLink) {
                    window.location.replace(redirectLink.href);
                }
            }
        }, function (status, data, responseText) {
            alert("Error: " + data.message);
        });
    }
    function foreachElement(selectors, action) {
        var elements = document.querySelectorAll(selectors);
        var length = elements.length;
        for (var i = 0; i < length; i++) {
            action(elements.item(i));
        }
    }
    function prepare() {
        foreachElement('a[rel~="unsubscribe"]', function (anchor) {
            anchor.onclick = function (ev) {
                ev.preventDefault();
                DELETE(anchor.href);
            };
        });
    }
    ApiPoc.prepare = prepare;
})(ApiPoc || (ApiPoc = {}));
ApiPoc.prepare();
