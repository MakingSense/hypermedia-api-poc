# Collection

All collection models in our system share some common structure:

* Should contains a list of elements 
    * HTML: `ul.items`
    * JSON: `$.items`
* May contains pagination information:
    * _FirstPage_ reference
    * _LastPage_ reference
    * _PreviousPage_ reference
    * _NextPage_ reference
    * _SpecificPage_ reference
    * Current page
        * HTML: `span.current-page`
        * JSON: `$.currentPage`
    * Page size
        * HTML: `span.page-size`
        * JSON: `$.pageSize`
    * Total items count 
        * HTML: `span.items-count`
        * JSON: `$.itemsCount`
    * Total pages count 
        * HTML: `span.pages-count`
        * JSON: `$.pagesCount`