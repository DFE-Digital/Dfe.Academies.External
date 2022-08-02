/**
* Copyright (c) 2022
*
* Trust search Javascript to wire up to a local GET endpoint which will then in turn call
* and academies API endpoint to perform a school search.
* Depends upon components JQuery and accessible-autocomplete which are injected in _layout file.
*
* Dependencies:-
* 1) https://www.npmjs.com/package/accessible-autocomplete
*      accessible-autocomplete is a JavaScript autocomplete built from the ground up to be accessible.
* 2) JQuery - for  $("#SearchQueryInput") && $.ajax() && $('#schoolSelectedDetails').html && $("#SearchQueryInput").hide
*                  && $("#schoolSelectedDetails").empty()
*                  && $.validator() && $(this).valid() - for clientside validation
*/

var academies = window.academies || {};

let debounceTimeout;