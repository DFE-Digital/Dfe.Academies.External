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

    // TODO MR:- similar to concerns casework do we need a control cleardown e.g. ??
    //$("#schoolSelectedDetails").empty();
    //$("#autocomplete-container").empty();

    $('.js-hide').addClass("js-invisible");
    $('.js-show').show();

    document.getElementById("ConfirmSelection").checked = false;

    A2C.addCustomerClientSideValidators();
});

A2C.unhideSelectedSchoolSectionAndConfirmCheckbox = function () {
    A2C.unhideElement("schoolSelectedDetails");
    A2C.unhideElement("confirm-school-label");

    const elementToManipulate = document.getElementById("ConfirmSelection");
    elementToManipulate.classList.remove("hideElement");
};

A2C.hideElement = function (elementName) {
	const elementToManipulate = document.getElementById(elementName);
	elementToManipulate.classList.add("hideElement");
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
            //document.getElementById("schoolSelect").value = selectedValue; // value still not set, maybe you can't !!!!

            // MR:- accessibleAutocomplete clones <select> control and display:none
            // creates a new one, id='schoolSelect-select' within <div class='autocomplete__wrapper'>
            //document.getElementById("schoolSelect-select").value = selectedValue; // value still not set, maybe you can't !!!!

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

A2C.addCustomerClientSideValidators = function() {
    /* Add Confirm checkbox Custom Validation config ! */

    // MR:- value = useless for a checkbox!
	$.validator.addMethod("confirmselection",
        function (value, element, parameters) {
            const checkboxCheckedValue = document.getElementById(element.id).checked;

            if (checkboxCheckedValue !== true) {
	            A2C.addConfirmValidationMessage();
	            return false;
            } else {
	            return true;
            }
        });

	$.validator.unobtrusive.adapters.add("confirmselection",
		[],
		function(options) {
            options.rules.confirmselection = {};
			options.messages["confirmselection"] = options.message;
        });

	/* Add Search Query Custom Validation config ! */
    $.validator.addMethod("searchqueryrequired",
	    function (value, element, parameters) {
		    debugger;
            if (value.trim().length > 0) {
	            if (value.length > 4) {
		            //check selected school control
                    const selectedSchool = document.getElementById("SearchQuery").value;
                    if (selectedSchool.trim().length === 0) {
	                    return false;
                    } else {
	                    return true;
                    }
	            } else {
		            return true;
	            }
            } else {
	            return false;
            }
        });

    $.validator.unobtrusive.adapters.add("searchqueryrequired",
	    [],
	    function (options) {
		    options.rules.confirmselection = {};
            options.messages["searchqueryrequired"] = options.message;
	    });
};

A2C.addSearchQueryValidationMessage = function() {
    A2C.unhideElement("SearchQueryErrorContainer");
    document.getElementById("SearchQueryError").textContent = "Search cannot be blank";
    // MR:- add left bar
    const elementToManipulate = document.getElementById("SearchQueryContainer");
    elementToManipulate.classList.add("govuk-form-group--error");
};

A2C.addConfirmValidationMessage = function () {
    A2C.unhideElement("ConfirmationErrorContainer");
    // MR:- add left bar
    const elementToManipulate = document.getElementById("confirm-school-checkbox");
    elementToManipulate.classList.add("govuk-form-group--error");
};

A2C.clientSideValidation = function () {
    A2C.addcomplexCustomerValidators();

	var form = $("#search-form");
	form.validate();

	debugger;

	if ($(this).valid()) {
	    // MR:- carry on - run server side code
        debugger;
	} else {
		event.preventDefault();
	}
};

//// This function is only meant to validate complex scenario's
//// in built unobtrusive validation should handle the model [required] properties
A2C.addcomplexCustomerValidators = function () {
	var errorElement = $('span[data-valmsg-for="skills"]');
    var errorMessage = "Select at least 3 skills";

    const queryValue = document.getElementById("SearchQuery").value;

    //message: 'Search cannot be blank'
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
};