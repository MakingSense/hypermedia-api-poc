var ApiPoc;
(function (ApiPoc) {
    var LinkRepresentation = (function () {
        function LinkRepresentation() {
        }
        return LinkRepresentation;
    })();
    var Request = (function () {
        function Request() {
            this.host = window.location.host;
            this.headers = {};
        }
        Request.prototype.toString = function () {
            var result = this.method + " " + this.url + " HTTP/1.1" + "\n" +
                "Host: " + this.host + "\n";
            for (var k in this.headers) {
                result += k + ": " + this.headers[k] + "\n";
            }
            if (this.body) {
                result += "\n" + this.body + "\n";
            }
            return result;
        };
        return Request;
    })();
    function hide(element) {
        if (element.style.display && element.style.display != 'none') {
            element["$$oldblock"] = element.style.display;
        }
        element.style.display = "none";
    }
    function show(element) {
        element.style.display = element["$$oldblock"] || 'block';
    }
    function toggle(element) {
        var display = element.style.display;
        if (display == "none" || display == "") {
            show(element);
        }
        else {
            hide(element);
        }
    }
    function readForm(form) {
        var obj = null;
        var length = form.elements.length;
        for (var i = 0; i < length; i++) {
            var element = form.elements[i];
            if (!element.classList.contains("ignore-on-send") && !element.classList.contains("uri-template")) {
                obj = obj || {};
                obj[element.name] = element.value;
            }
        }
        return obj;
    }
    function readUri(form) {
        var obj = null;
        var length = form.elements.length;
        for (var i = 0; i < length; i++) {
            var element = form.elements[i];
            if (element.classList.contains("uri-template")) {
                obj[element.name] = element.value;
            }
        }
        //TODO: Apply obj to override uri-template parameters
        return form.action;
    }
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
    function execute(request) {
        var xmlHttpRequest = new XMLHttpRequest();
        xmlHttpRequest.onload = function (a) {
            alert("Request has been executed. Content will be updated with request response");
            document.open();
            document.write(xmlHttpRequest.responseText);
            document.close();
        };
        xmlHttpRequest.open(request.method, request.url, true);
        for (var k in request.headers) {
            xmlHttpRequest.setRequestHeader(k, request.headers[k]);
        }
        xmlHttpRequest.send(request.body);
    }
    function foreachElement(selectors, action) {
        var elements = document.querySelectorAll(selectors);
        var length = elements.length;
        for (var i = 0; i < length; i++) {
            action(elements.item(i));
        }
    }
    function prepareForm(form) {
        var anchor = (form.previousElementSibling);
        var generateBtn = form.elements.namedItem("generate-request");
        var queryTextArea = form.elements.namedItem("generated-request");
        var submitBtn = form.elements.namedItem("submit-request");
        var request = new Request();
        hide(form);
        anchor.onclick = function (ev) {
            ev.preventDefault();
            toggle(form);
            hide(queryTextArea);
            hide(submitBtn);
        };
        generateBtn.onclick = function (ev) {
            var obj = readForm(form);
            request.method = form.dataset["method"];
            request.url = readUri(form);
            if (request.method == "GET") {
                submitBtn.value = "Go";
                queryTextArea.value = request.url;
            }
            else {
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
        submitBtn.onclick = function (ev) {
            if (request.method == "GET") {
                window.location.href = request.url;
            }
            else {
                execute(request);
            }
        };
    }
    function prepare() {
        foreachElement('form', function (form) {
            prepareForm(form);
        });
    }
    ApiPoc.prepare = prepare;
})(ApiPoc || (ApiPoc = {}));
ApiPoc.prepare();
