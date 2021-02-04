/// <reference path="./jquery/jquery.d.ts"/>
/// <reference path="./Search/SearchResultDisplayer.ts"/>

//window.addEventListener("scroll", onScroll);

function onScroll() {
    console.log('scroll');
    //$('#test').html('saddsasadsda');
    console.log($('#test').html());
}

$(() => {
    let truc = new SearchResultDisplayer($('.result-table'));

    truc.loadNextResults();
});

//console.log($('.result-table').length);
//$('#test').html('pipi')

