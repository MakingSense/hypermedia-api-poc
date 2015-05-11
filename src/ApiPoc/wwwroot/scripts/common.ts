module ApiPoc {
    interface IDictionary<T> {
        [id: string]: T
    }

    class LinkRepresentation {
        href: string
        rel: string
        description: string
        safe: string
    }

    class Request {
        method: string
        url: string
        host: string = window.location.host
        headers: IDictionary<string> = { }
        body: string

        toString() {
            var result = this.method + " " + this.url + " HTTP/1.1" + "\n" +
                "Host: " + this.host + "\n";

            for (var k in this.headers) {
                result += k + ": " + this.headers[k] + "\n";
            }

            if (this.body) {
                result += "\n" + this.body + "\n";
            }

            return result;
        }
    }

    function hide(element: HTMLElement) {
        if (element.style.display && element.style.display != 'none') {
            element["$$oldblock"] = element.style.display;
        }
        element.style.display = "none";
    }

    function show(element: HTMLElement) {
        element.style.display = element["$$oldblock"] || 'block';
    }

    function toggle(element: HTMLElement) {
        var display = element.style.display;
        if (display == "none" || display == "") {
            show(element);
        } else {
            hide(element);
        }
    }

    function readForm(form: HTMLFormElement): IDictionary<string> {
        var obj = <IDictionary<string>> null;
        var length = form.elements.length;
        for (var i = 0; i < length; i++) {
            var element = <HTMLInputElement>form.elements[i];
            if (!element.classList.contains("ignore-on-send") && !element.classList.contains("uri-template")) {
                obj = obj || {};
                obj[element.name] = element.value;
            }
        }
        return obj;
    }

    function readUri(form: HTMLFormElement): string {
        var obj = <IDictionary<string>> {};
        var length = form.elements.length;
        for (var i = 0; i < length; i++) {
            var element = <HTMLInputElement>form.elements[i];
            if (element.classList.contains("uri-template")) {
                obj[element.name] = element.value;
            }
        }
        var uri = form.action;
        uri = uri.replace(/{([^{}]*)}/g,
            function (a, b) {
                return obj[b];
            });
        return uri;
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

    function execute(request: Request) {
        var xmlHttpRequest = new XMLHttpRequest();
        xmlHttpRequest.onload = (a) => {
            alert("Request has been executed. Content will be updated with request response");
            document.open();
            document.write(xmlHttpRequest.responseText);
            document.close();
        }
        xmlHttpRequest.open(request.method, request.url, true);
        for (var k in request.headers) {
            xmlHttpRequest.setRequestHeader(k, request.headers[k]);
        }
        xmlHttpRequest.send(request.body);
    }

    function foreachElement<T extends Node>(selectors: string, action: (element: T) => void) {
        var elements = document.querySelectorAll(selectors);
        var length = elements.length;
        for (var i = 0; i < length; i++) {
            action(<T>elements.item(i));
        }
    }

    function prepareForm(form: HTMLFormElement) {
        var anchor = <HTMLAnchorElement>(form.previousElementSibling);
        var generateBtn = <HTMLInputElement>form.elements.namedItem("generate-request");
        var queryTextArea = <HTMLInputElement>form.elements.namedItem("generated-request");
        var submitBtn = <HTMLInputElement>form.elements.namedItem("submit-request");
        var request = new Request();
        hide(form);
        anchor.onclick = (ev) => {
            ev.preventDefault();
            toggle(form);
            hide(queryTextArea);
            hide(submitBtn);
        };
        generateBtn.onclick = (ev) => {
            var obj = readForm(form);
            request.method = form.dataset["method"];
            request.url = readUri(form);
            if (request.method == "GET") {
                submitBtn.value = "Go"
                queryTextArea.value = request.url;
            } else {
                request.headers["Content-Type"] = "application/json; charset=UTF-8";
                request.headers["Accept"] = "text/html";
                if (obj) {
                    request.body = JSON.stringify(obj, null, 2);
                    request.headers["Content-Length"] = request.body.length.toString();
                }
                queryTextArea.value = request.toString();
            }
            show(queryTextArea);
            show(submitBtn);
        };
        submitBtn.onclick = (ev) => {
            if (request.method == "GET") {
                window.location.href = request.url;
            } else {
                execute(request);
            }
        };
    }

    export function prepare() {
        foreachElement<HTMLFormElement>('form', form => {
            prepareForm(form);
        });
    }
}

ApiPoc.prepare();
