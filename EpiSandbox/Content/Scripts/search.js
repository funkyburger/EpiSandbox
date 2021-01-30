/// <reference path="./jquery/jquery.d.ts"/>
window.addEventListener("scroll", onScroll);
function onScroll() {
    console.log('scroll');
    //$('#test').html('saddsasadsda');
    console.log($('#test').html());
}
//function hookOnControl() {
//    //$('.result-table').on
//}
var SearchResult = /** @class */ (function () {
    function SearchResult() {
    }
    return SearchResult;
}());
var SearchResultDisplayer = /** @class */ (function () {
    function SearchResultDisplayer() {
        this.lastpage = -1;
    }
    SearchResultDisplayer.prototype.loadNextResults = function () {
        console.log('loadNextResults');
        this.displayResult(new SearchResult());
    };
    SearchResultDisplayer.prototype.displayResult = function (searchResult) {
        var div = $('div');
        div.html('prout');
        div.appendTo($('#test'));
    };
    return SearchResultDisplayer;
}());
var truc = new SearchResultDisplayer();
truc.loadNextResults();
//# sourceMappingURL=search.js.map