/**
 * Copyright (c) 2022
 *
 * School search Javascript to wire up to a local GET endpoint which will then in turn call
 * and academies API endpoint to perform a school search.
 * Depends upon components JQuery and accessible-autocomplete which are injected in _layout file.
 *
  * Dependencies:-
 * 1) https://www.npmjs.com/package/accessible-autocomplete
 *      accessible-autocomplete is a JavaScript autocomplete built from the ground up to be accessible.
 * 2) JQuery - for $.ajax && selectElement: $('#schoolSelect')[0] && $('#schoolSelectedDetails').html
  */

var A2C = window.A2C || {};

let debounceTimeout;

$(function () {
    A2C.searchSchools();
    //A2C.hideSelectedSchoolSectionAndConfirmCheckbox(); hidden in razor !

    // TODO MR:- similar to concerns casework do we need a control cleardown e.g.
    //$("#schoolSelectedDetails").empty();
    //$("#autocomplete-container").empty();

    $('.js-hide').addClass("js-invisible");
    $('.js-show').show();

    //$("#search-form").submit(function () {
	   // Validate();
    //});
});

A2C.unhideSelectedSchoolSectionAndConfirmCheckbox = function () {
    A2C.unhideElement("schoolSelectedDetails");
    A2C.unhideElement("confirm-school-label");

	const elementToManipulate = document.getElementById("CorrectSchoolConfirmation");
    elementToManipulate.classList.remove("hideElement");
};

A2C.unhideElement = function (elementName) {
    const elementToManipulate = document.getElementById(elementName);
    
    elementToManipulate.classList.remove("hideElement");
    elementToManipulate.classList.add("unHideElement");
};

A2C.searchSchools = function () {
	accessibleAutocomplete.enhanceSelectElement({
        selectElement: document.querySelector('#schoolSelect'),
        //id: - Not required if using enhanceSelectElement.
        source: debounceSuggest,
        confirmOnBlur: false,
        defaultValue: '', // MR:- must have !! otherwise accessibleAutocomplete code blows up !!!
		displayMenu: 'overlay',
        minLength: 4,
        onConfirm: (function (selectedValue) {
            A2C.renderSchoolSearchOption(selectedValue);

            // MR:- new cloned control id='schoolSelect'
            document.getElementById("schoolSelect").value = selectedValue; // value still not set, maybe you can't !!!!

            // MR:- accessibleAutocomplete clones <select> control and display:none
            // creates a new one, id='schoolSelect-select' within <div class='autocomplete__wrapper'>
            document.getElementById("schoolSelect-select").value = selectedValue; // value still not set, maybe you can't !!!!

            setTimeout(() => {
                    // MR:- btnAdd always visible now !!!
				},
				2000);
		})
	});
};

A2C.renderSchoolSearchOption = function (selectedValue) {
    document.getElementById("SearchQuery").value = selectedValue;

    // get full school record from an endpoint
    // render partial & set results DIV HTML
    // unhide selected school section of screen
    $.ajax({
        url: 'school/ReturnSchoolDetailsPartialViewPopulated',
        type: 'GET',
        data: { 'selectedSchool': selectedValue }, // selected value will be in the format 'Wise owl primary school (587634)'
        success: function (response) {
	        A2C.renderSelectedSchool(response);
            A2C.unhideSelectedSchoolSectionAndConfirmCheckbox();
        }
    });
};

A2C.renderSelectedSchool = function (responseHtml) {
    $('#schoolSelectedDetails').html(responseHtml);
};

function debounceSuggest(query, syncResults) {
    clearTimeout(debounceTimeout);
    debounceTimeout = setTimeout(() => {
	    A2C.GetSchoolSearchResults(query, syncResults);
    }, 500);
}

A2C.GetSchoolSearchResults = function(query, syncResults) {
	$.ajax({
		url: 'school/Search', // this calls a controller endpoint
		type: 'GET',
		data: { 'searchQuery': query },
		success: function(response) {
			syncResults(query
				? response.filter(function(result) {
					return true;
				})
				: []
			);
		}
	});
};

// MR:- below relies on formValidator declared within site.js - which in turns relies on MOJFrontend
//let searchForm = $("#search-form");
//const validator = formValidator(searchForm[0]);

//validator.addValidator('SearchQuery', [{
//	method: function (field) {
//		return field.value.trim().length > 0;
//	},
//	message: 'Search cannot be blank'
//}, {
//	method: function (field) {
//		return (field.value.length > 3);
//	},
//	message: 'Enter search criteria higher than four characters'
//}]);

//searchForm.submit(function(event) {
//	validator.onSubmit(event);
//    if (validator.validate())
//    {
//		// MR:- carry on - run server side code
//        alert(validator.validate());
//	} else {
//		event.preventDefault();
//		showGlobalError();
//		hideLoader();
//	}
//});

A2C.clientSideValidation = function () {
	A2C.validate();


	// TODO MR:- check document.getElementById("SearchQuery").value


	debugger;

	//if (validator.validate()) {
	//	// MR:- carry on - run server side code
	//	alert(validator.validate());
	//} else {
	//	event.preventDefault();
	//	showGlobalError();
	//	hideLoader();
	//}
};

//// This function is only meant to validate complex scenario's
//// in built unobtrusive validation should handle the model [required] properties
A2C.validate = function () {
	var errorElement = $('span[data-valmsg-for="skills"]');
    var errorMessage = "Select at least 3 skills";

    const queryValue = document.getElementById("SearchQuery").value;

    //message: 'Search cannot be blank'
};