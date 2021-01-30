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

class SearchResult {
    headline: string;
    sampleText: string;
}

class SearchResultDisplayer {
    lastpage: number;

    constructor() {
        this.lastpage = -1;
    }

    loadNextResults() {
        console.log('loadNextResults');
        this.displayResult(new SearchResult());
    }

    displayResult(searchResult: SearchResult) {
        var div = $('div');
        div.html('prout');

        div.appendTo($('#test'));
    }
}

let truc = new SearchResultDisplayer();

truc.loadNextResults();