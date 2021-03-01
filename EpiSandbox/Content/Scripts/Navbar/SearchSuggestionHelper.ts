class SearchSuggestionHelper {
    constructor() {
    }

    public onSearchBoxTyping() : void {
        var queryString = $('#searchField').val();

        if (queryString.length > 3) {
            $.get("api/search/?query=" + queryString, this.displaySearchSuggestions)
                .fail(function () {
                    console.log("An error occured during search.");
                    $('#searchSuggestion').hide();
                });
        }
        else {
            $('#searchSuggestion').hide();
        }
    }

    public appendSearchSuggestion(searchHit): void {
        var tr = $("<tr/>");
        var td = $("<td/>");
        var a = $("<a/>", { "href": searchHit.Link });
        var div = $("<div/>");
        div.html(searchHit.Headline);
        div.appendTo(a);
        a.appendTo(td);
        td.appendTo(tr);
        tr.appendTo($('#searchSuggestion > tbody'));
    }

    public displaySearchSuggestions(results): void {
        $('#searchSuggestion > tbody > tr').remove();

        if (results.length > 0) {
            for (var i = 0; i < results.length; i++) {
                this.appendSearchSuggestion(results[i]);
            }

            $('#searchSuggestion').show();
        }
        else {
            $('#searchSuggestion').hide();
        }
    }
}