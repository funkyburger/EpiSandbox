/// <reference path="./jquery/jquery.d.ts"/>
/// <reference path="./Search/SearchResultDisplayer.ts"/>

$(() => {
    var resultTable = $('.result-table');

    if (resultTable.length > 0) {
        let resultDisplayer = new SearchResultDisplayer($('.result-table'));

        resultDisplayer.loadNextResults();

        window.addEventListener("scroll", function () {
            console.log('scroll');

            if (resultDisplayer.isTableSrolledToBottom()) {
                resultDisplayer.loadNextResults();
            }
        });
    }
});