/// <reference path="./jquery/jquery.d.ts"/>
/// <reference path="./Search/SearchResultDisplayer.ts"/>

$(() => {
    var searchButton = $('#btnSearch');

    searchButton.mouseup(eventObject => {
        console.log('searchButton.mouseup');

        var query = $('#searchField').val();

        if (query.length > 0) {
            window.location.href = "/search?q=" + query;
        }
    });

    var searchField = $('#searchField');
    searchField.keypress(eventObject => {
        console.log('searchField.keypress');

        if (eventObject.keyCode == 13) {
            eventObject.preventDefault();

            var query = $('#searchField').val();

            if (query.length > 0) {
                window.location.href = "/search?q=" + query;
            }
        }
    });

    var resultTable = $('.result-table');

    var query = window.location.GetUrlParameterValue("q");

    if (resultTable.length > 0) {
        let resultDisplayer = new SearchResultDisplayer($('.result-table'), query);

        resultDisplayer.loadNextResults();

        window.addEventListener("scroll", function () {
            if (resultDisplayer.isTableSrolledToBottom()) {
                resultDisplayer.loadNextResults();
            }
        });
    }
});