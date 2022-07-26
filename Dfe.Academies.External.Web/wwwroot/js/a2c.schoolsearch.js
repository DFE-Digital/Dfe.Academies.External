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
* 2) JQuery - for  $("#SearchQueryInput") && $.ajax() && $('#schoolSelectedDetails').html && $("#SearchQueryInput").hide
*                  && $("#schoolSelectedDetails").empty()
*                  && $.validator() && $(this).valid() - for clientside validation
*/

var A2C = window.A2C || {};

let debounceTimeout;

$(function () {
    A2C.searchSchools();

    $('.js-hide').addClass("js-invisible");
    $('.js-show').show();

    document.getElementById("ConfirmSelection").checked = false;
    document.getElementById("SearchQueryInput").value = "";

    A2C.addCustomerClientSideValidators();

    $("#SearchQueryInput").focusin(function () {
	    A2C.clearResults();
    });

    // MR:- accessibleAutocomplete clones original control - so hide original
    $("#SearchQueryInput").hide("fast");
});

// MR:- similar to concerns casework clearing down controls
A2C.clearResults = function () {
    $("#schoolSelectedDetails").empty();
    $("#autocomplete-container").empty();
    // MR:- from concerns casework - not sure what this is for !
	//// $(".autocomplete__menu").addClass("autocomplete__menu--hidden");
};

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
	let autocompleteContainer = document.getElementById("autocomplete-container"); // MR:- this is just a plain old DIV
    const input = $("#SearchQueryInput"); // MR:- this is input type=text, which gets cloned

    accessibleAutocomplete({
        element: autocompleteContainer,
	    id: input.attr("id"),
	    name: input.attr("name"),
        source: debounceSuggest,
        confirmOnBlur: false,
		displayMenu: 'overlay',
        minLength: 4,
        onConfirm: (function (selectedValue) {
	        //// input.attr("aria-valuetext", trustUkprn);
            input.val(selectedValue);

            A2C.renderSchoolSearchOption(selectedValue);

            let originalSearchInput = $("#autocomplete-container #SearchQueryInput");
            originalSearchInput.val(selectedValue);

            // MR:- from concerns casework - not sure what this is for !
            //$(".autocomplete__menu").removeClass("autocomplete__menu--hidden");
		})
    });
};

A2C.renderSchoolSearchOption = function (selectedValue) {
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
            A2C.clearErrorBars(); // clear any existing name not input err
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
		url: 'school/Search',
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
    $.validator.addMethod("confirmselection", function (value, element) {
	    // MR:- value = useless for a checkbox!
        const checkboxCheckedValue = document.getElementById(element.id).checked; 

        if (checkboxCheckedValue !== true) {
            A2C.addConfirmValidationMessage();
            return false;
        } else {
            return true;
        }
    });

	$.validator.unobtrusive.adapters.add("confirmselection", function (options) {
            options.rules["confirmselection"] = options.params;
            options.messages["confirmselection"] = options.message;
	});

	/* Add Search Query Custom Validation config ! */
    $.validator.addMethod('searchqueryrequired', function (value, element) {
	    if (value.trim().length > 0) {
                if (value.trim().length > 4) {
	                //check selected school control
                    const selectedSchool = document.getElementById("SearchQueryInput").value;
                    if (selectedSchool.trim().length === 0) {
                        A2C.addSearchQueryValidationMessage("You must choose a school from the list");
	                    return false;
                    } else {
	                    return true;
                    }
                } else {
	                A2C.addSearchQueryValidationMessage("Search must be more than 4 characters");
		            return false;
	            }
            } else {
                A2C.addSearchQueryValidationMessage("You must give the name of the school");
	            return false;
            }
        });

    $.validator.unobtrusive.adapters.add('searchqueryrequired', function (options) {
            options.rules["searchqueryrequired"] = options.params;
            options.messages["searchqueryrequired"] = options.message;
	    });
};

A2C.clearErrorBars = function () {
    if (document.getElementById("SearchQueryContainer").classList.contains("govuk-form-group--error")) {
	    document.getElementById("SearchQueryContainer").classList.remove("govuk-form-group--error");
    }

    if (document.getElementById("ConfirmationErrorContainer").classList.contains("govuk-form-group--error")) {
        document.getElementById("ConfirmationErrorContainer").classList.remove("govuk-form-group--error");
    }

    if (document.getElementById("confirm-school-checkbox").classList.contains("govuk-form-group--error")) {
        document.getElementById("confirm-school-checkbox").classList.remove("govuk-form-group--error");
    }
};

A2C.addSearchQueryValidationMessage = function (errorMessage) {
	A2C.clearErrorBars();

    // TODO MR:- this always errors, but no idea why at this point !!!!!
    //id="SearchQueryInput-error" = span
    //const span = document.getElementById('SearchQueryInput-error');
    //span.textContent = errorMessage;

    // MR:- add left bar
    const elementToManipulate = document.getElementById("SearchQueryContainer");
    elementToManipulate.classList.add("govuk-form-group--error");
};

A2C.addConfirmValidationMessage = function () {
	A2C.clearErrorBars();

    A2C.unhideElement("ConfirmationErrorContainer");
    // MR:- add left bar
    const elementToManipulate = document.getElementById("confirm-school-checkbox");
    elementToManipulate.classList.add("govuk-form-group--error");
};

A2C.clientSideValidation = function () {
    A2C.addcomplexCustomerValidators();

	var form = $("#search-form");
	form.validate();

	if ($(this).valid()) {
	    // MR:- carry on - run server side code
	} else {
		event.preventDefault();
	}
};

A2C.clearCheckboxValidation = function (checked) {
	if (checked === true) {
		A2C.clearErrorBars();
	}
};
