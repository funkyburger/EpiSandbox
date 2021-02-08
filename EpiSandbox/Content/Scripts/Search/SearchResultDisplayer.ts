/// <reference path="../jquery/jquery.d.ts"/>
/// <reference path="../es6-promise/es6-promise.d.ts"/>
/// <reference path="./SearchResult.ts"/>

class SearchResultDisplayer {
    lastpage: number;
    listElement: JQuery;
    isEnabled: boolean;

    constructor(listElement: JQuery) {
        this.lastpage = -1;
        this.listElement = listElement;
        this.isEnabled = true;
    }

    public loadNextResults(): void {
        if (!this.isEnabled) {
            return;
        }

        this.fetchResults("aglagla");
    }

    public isTableSrolledToBottom(): boolean {
        return this.getLowerWindowY() >= this.getLowerListY();
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
            if (data.length < 1) {
                this.isEnabled = false;
            }

            data.forEach(sr => { this.displayResult(sr); });
        })
            .fail((error: any) => {
                // TODO : error handling
                console.error('fail');
            });
    }

    private getLowerWindowY(): number{
        return window.innerHeight + window.scrollY;
    }

    private getLowerListY(): number {
        return this.listElement.height() + this.listElement.offset().top;
    }
}