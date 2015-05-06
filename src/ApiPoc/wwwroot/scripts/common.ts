module ApiPoc {
    class LinkRepresentation {
        href: string
        rel: string
        description: string
        safe: string
    }

    function searchLinkByRel(links: LinkRepresentation[], rel: string): LinkRepresentation {
        for (var link of links) {
            var rels = link.rel.split(" ");
            if (rels.some(x => x == rel)) {
                return link;
            }
        }
        return null;
    }

    function ajax(method: string, url: string, onSuccess: (status: number, data: any, responseText: string) => void = null, onError: (status: number, data: any, responseText: string) => void = null) {
        var request = new XMLHttpRequest();
        request.onload = (a) => {
            try {
                var parsedResult = JSON.parse(request.responseText);
                if (onSuccess && request.status >= 200 && request.status < 400) {
                    onSuccess(request.status, parsedResult, request.responseText);
                } else if (onError) {
                    onError(request.status, parsedResult, request.responseText);
                }
            } catch (e) {
                onError(request.status, { message: "Parsing error", exception: e }, request.responseText);
            }
        }
        request.open(method, url, true);
        request.setRequestHeader("accept", "text/json");
        request.send();
    }

    function DELETE(url: string) {
        ajax(
            "DELETE",
            url,
            (status, data, responseText) => {
                alert("Success!");
                if (data && data.links) {
                    var redirectLink = searchLinkByRel(data.links, "suggested");
                    if (redirectLink) {
                        window.location.replace(redirectLink.href);
                    }
                }
            },
            (status, data, responseText) => {
                alert("Error: " + data.message);
            });
    }

    function foreachElement<T extends Node>(selectors: string, action: (element: T) => void) {
        var elements = document.querySelectorAll(selectors);
        var length = elements.length;
        for (var i = 0; i < length; i++) {
            action(<T>elements.item(i));
        }
    }

    export function prepare() {
        foreachElement('a[rel~="delete"]', (anchor: HTMLAnchorElement) => {
            anchor.onclick = (ev) => {
                ev.preventDefault();
                DELETE(anchor.href);
            };
        });
    }
}

ApiPoc.prepare();
