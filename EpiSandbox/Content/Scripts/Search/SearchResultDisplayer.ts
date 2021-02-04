/// <reference path="../jquery/jquery.d.ts"/>
/// <reference path="../es6-promise/es6-promise.d.ts"/>
/// <reference path="./SearchResult.ts"/>

class SearchResultDisplayer {
    lastpage: number;
    listElement: JQuery;

    constructor(listElement: JQuery) {
        this.lastpage = -1;
        this.listElement = listElement;
    }

    public loadNextResults(): void {
        this.fetchResults("aglagla");
    }

    private displayResult(searchResult: SearchResult): void {
        var mainDiv = $("<div/>");
        mainDiv.addClass("row");
        
        var cell = $("<div/>");
        cell.addClass("col-md-10");
        cell.appendTo(mainDiv);

        var a = $("<a/>", { "href": "#" });
        a.appendTo(cell);

        var headline = $("<div/>");
        headline.html(searchResult.Headline);
        headline.appendTo(a);

        var sample = $("<div/>");
        sample.html(searchResult.Sample);
        sample.appendTo(cell);

        this.listElement.append(mainDiv);
    }

    private fetchResults(query: string): void {
        this.lastpage++;

        $.ajax({
            url: "/api/search?query=" + query + "&page=" + this.lastpage + "&pageSize=10",
            type: 'GET'
        }).then((data: SearchResult[], textStatus: string, jqXhr: JQueryXHR) => {
            data.forEach(sr => { this.displayResult(sr); });
        })
            .fail((error: any) => {
                // TODO : error handling
                console.error('fail');
            });
    }
}