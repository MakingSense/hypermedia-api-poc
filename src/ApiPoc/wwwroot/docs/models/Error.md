# Error

When something does not goes fine, system returns an error representation with:

* _StatusCode_
    * HTML: `.error > span.error-code`
    * JSON: `$.statusCode`   
* _MessageText_
    * HTML: `.error > p.message-text`
    * JSON: `$.message-text`
* It may also contain more details about the problem. 

It should include links to guide application flow, one of them marked as _Suggested_.