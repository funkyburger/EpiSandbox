/// <reference path="./jquery/jquery.d.ts"/>
/// <reference path="./Search/SearchResultDisplayer.ts"/>

$(() => {
    var searchButton = $('#btnSearch');

    searchButton.mouseup(eventObject => {
        var query = $('#searchField').val();

        if (query.length > 0) {
            window.location.href = "/search?q=" + query;
        }
    });

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