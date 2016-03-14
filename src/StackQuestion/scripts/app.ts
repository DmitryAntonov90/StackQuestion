/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/jqueryui/jqueryui.d.ts" />

function displayAnser(id: number) {
    var element = $('#display-answer-' + id).toggleClass('visability');
}