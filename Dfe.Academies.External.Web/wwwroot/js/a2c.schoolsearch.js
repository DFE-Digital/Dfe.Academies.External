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
 * 2) JQuery
  */

var A2C = window.A2C || {};

let debounceTimeout;

$(function () {
    A2C.searchSchools();
    //A2C.hideSelectedSchoolSectionAndConfirmCheckbox(); hidden in razor !

    // TODO MR:- similar to concerns casework do we need a control cleardown e.g.
    //$("#schoolSelectedDetails").empty();
    //$("#autocomplete-container").empty();
});

//A2C.hideSelectedSchoolSectionAndConfirmCheckbox = function () {
//    // hide school results div
//    const schoolSelectedDetails = document.getElementById("schoolSelectedDetails");
//    schoolSelectedDetails.classList.add("hideElement");
    
//    // hide confirm checkbox until school selected
//	//$('#confirm-school-checkbox').css('display', 'none')
// //       .css('margin-bottom', '1.3em');
    
//	const checkboxContainer = document.getElementById("confirm-school-checkbox");
//    checkboxContainer.classList.add("hideElement");
//};

A2C.unhideSelectedSchoolSectionAndConfirmCheckbox = function () {
    A2C.unhideElement("schoolSelectedDetails");
    A2C.unhideElement("confirm-school-cb");
    A2C.unhideElement("confirm-school-label");
};

A2C.unhideElement = function (elementName) {
    const elementToManipulate = document.getElementById(elementName);

    console.log(elementToManipulate);

    elementToManipulate.classList.remove("hideElement");
    elementToManipulate.classList.add("unHideElement");
};

A2C.searchSchools = function () {
	accessibleAutocomplete.enhanceSelectElement({
		source: debounceSuggest,
		defaultValue: '',
		displayMenu: 'overlay',
		confirmOnBlur: false,
		selectElement: $('#schoolSelect')[0],
		minLength: 4,
        onConfirm: (function (selectedValue) {
            A2C.renderSchoolSearchOption(selectedValue);

			setTimeout(() => {
					//$('#btnAdd').addClass('govuk-button--disabled')
					//	.attr('aria-disabled', 'true')
					//	.prop('disabled', 'true');

					//if ($('#confirm-school-checkbox').length === 0) {
					//	$('#btnAdd').removeClass('govuk-button--disabled')
					//		.attr('aria-disabled', 'false')
					//		.removeAttr('disabled');
					//}
				},
				2000);
		})
	});
};

A2C.renderSchoolSearchOption = function (selectedValue) {
	document.getElementById("searchQuery").value = selectedValue;

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
	    getSchools(query, syncResults);
    }, 500);
}

// call search schools endpoint
function getSchools(query, syncResults) {
    $.ajax({
        url: 'school/Search', // this calls a controller endpoint
        type: 'GET',
        data: { 'searchQuery': query },
        success: function (response) {
	        syncResults(query
		        ? response.filter(function(result) {
			        return true;
		        })
		        : []
	        );
        }
    });
}

function toggleConfirmationCheckbox() {
    if ($('#confirm-school-cb').prop('checked') === true)
    {
        // MR:- current app disables add button until validation = success / confirm checkbox = true
        $('#btnAdd').removeClass('govuk-button--disabled')
            .attr('aria-disabled', 'false')
            .removeAttr('disabled');
    }
    else
    {
        $('#btnAdd').addClass('govuk-button--disabled')
            .attr('aria-disabled', 'true')
            .prop('disabled', 'true');
    }
}