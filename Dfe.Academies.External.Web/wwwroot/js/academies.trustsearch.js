/**
* Copyright (c) 2022
*
* Trust search Javascript to wire up to a local GET endpoint which will then in turn call
* and academies API endpoint to perform a trust search.
* Depends upon components JQuery and accessible-autocomplete which are injected in _layout file.
*
* Dependencies:-
* 1) https://www.npmjs.com/package/accessible-autocomplete
*      accessible-autocomplete is a JavaScript autocomplete built from the ground up to be accessible.
* 2) JQuery - for  $("#SearchQueryInput") && $.ajax() && $('#trustSelectedDetails').html && $("#SearchQueryInput").hide
*                  && $("#trustSelectedDetails").empty()
*                  && $.validator() && $(this).valid() - for clientside validation
*/

var academies = window.academies || {};

let debounceTimeout;

$(function () {
	academies.searchTrusts();

	$('.js-hide').addClass("js-invisible");
	$('.js-show').show();

	document.getElementById("ConfirmSelection").checked = false;
	document.getElementById("SearchQueryInput").value = "";

	academies.addCustomClientSideValidators();

	$("#SearchQueryInput").focusin(function () {
		academies.clearTrustSearchResults();
	});

	// MR:- accessibleAutocomplete clones original control - so hide original
	$("#SearchQueryInput").hide("fast");
});

academies.clearTrustSearchResults = function () {
	$("#trustSelectedDetails").empty();
	$("#autocomplete-container").empty();
};

academies.unhideSelectedTrustSectionAndConfirmCheckbox = function () {
	academies.unhideElement("trustSelectedDetails");
	academies.unhideElement("confirm-trust-label");

	const elementToManipulate = document.getElementById("ConfirmSelection");
	elementToManipulate.classList.remove("hideElement");
};

academies.hideElement = function (elementName) {
	const elementToManipulate = document.getElementById(elementName);
	elementToManipulate.classList.add("hideElement");
};

academies.unhideElement = function (elementName) {
	const elementToManipulate = document.getElementById(elementName);
	elementToManipulate.classList.remove("hideElement");
	elementToManipulate.classList.add("unHideElement");
};

academies.searchTrusts = function () {
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
			input.val(selectedValue);

			academies.renderTrustSearchOption(selectedValue);

			let originalSearchInput = $("#autocomplete-container #SearchQueryInput");
			originalSearchInput.val(selectedValue);
		})
	});
};

academies.renderTrustSearchOption = function (selectedValue) {
	// get full trust record from an endpoint
	// render partial & set results DIV HTML
	// unhide selected trust section of screen
	$.ajax({
		url: 'trust/ReturnTrustDetailsPartialViewPopulated',
		type: 'GET',
		data: { 'selectedTrust': selectedValue }, // selected value will be in the format 'trust name (UKprn)'
		success: function (response) {
			academies.renderSelectedTrust(response);
			academies.unhideSelectedTrustSectionAndConfirmCheckbox();
			academies.clearTrustSearchErrorBars(); // clear any existing name not input err
		}
	});
};

academies.renderSelectedTrust = function (responseHtml) {
	$('#trustSelectedDetails').html(responseHtml);
};

function debounceSuggest(query, syncResults) {
	clearTimeout(debounceTimeout);
	debounceTimeout = setTimeout(() => {
		academies.GetTrustSearchResults(query, syncResults);
	}, 500);
}

academies.GetTrustSearchResults = function (query, syncResults) {
	$.ajax({
		url: 'trust/Search',
		type: 'GET',
		data: { 'searchQuery': query },
		success: function (response) {
			syncResults(query
				? response.filter(function (result) {
					return true;
				})
				: []
			);
		}
	});
};

academies.addCustomClientSideValidators = function () {
	/* Add Confirm checkbox Custom Validation config ! */
	$.validator.addMethod("confirmselection", function (value, element) {
		// MR:- value = useless for a checkbox!
		const checkboxCheckedValue = document.getElementById(element.id).checked;

		if (checkboxCheckedValue !== true) {
			academies.addConfirmValidationMessage();
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
				//check selected trust control
				const selectedSchool = document.getElementById("SearchQueryInput").value;
				if (selectedSchool.trim().length === 0) {
					academies.addSearchQueryValidationMessage("You must choose a trust from the list");
					return false;
				} else {
					return true;
				}
			} else {
				academies.addSearchQueryValidationMessage("Search must be more than 4 characters");
				return false;
			}
		} else {
			academies.addSearchQueryValidationMessage("You must give the name of the trust");
			return false;
		}
	});

	$.validator.unobtrusive.adapters.add('searchqueryrequired', function (options) {
		options.rules["searchqueryrequired"] = options.params;
		options.messages["searchqueryrequired"] = options.message;
	});
};

academies.clearTrustSearchErrorBars = function () {
	if (document.getElementById("SearchQueryContainer").classList.contains("govuk-form-group--error")) {
		document.getElementById("SearchQueryContainer").classList.remove("govuk-form-group--error");
	}

	if (document.getElementById("ConfirmationErrorContainer").classList.contains("govuk-form-group--error")) {
		document.getElementById("ConfirmationErrorContainer").classList.remove("govuk-form-group--error");
	}

	if (document.getElementById("confirm-trust-checkbox").classList.contains("govuk-form-group--error")) {
		document.getElementById("confirm-trust-checkbox").classList.remove("govuk-form-group--error");
	}
};

academies.addSearchQueryValidationMessage = function (errorMessage) {
	academies.clearTrustSearchErrorBars();

	//id="SearchQueryInput-error" = span
	//const span = document.getElementById('SearchQueryInput-error');
	//span.textContent = errorMessage;

	// MR:- add left bar
	const elementToManipulate = document.getElementById("SearchQueryContainer");
	elementToManipulate.classList.add("govuk-form-group--error");
};

academies.addConfirmValidationMessage = function () {
	academies.clearTrustSearchErrorBars();

	academies.unhideElement("ConfirmationErrorContainer");
	// MR:- add left bar
	const elementToManipulate = document.getElementById("confirm-trust-checkbox");
	elementToManipulate.classList.add("govuk-form-group--error");
};

academies.clientSideValidation = function () {
	//academies.addcomplexCustomValidators();

	var form = $("#search-form");
	form.validate();

	if ($(this).valid()) {
		// MR:- carry on - run server side code
	} else {
		event.preventDefault();
	}
};

academies.clearTrustConfirmCheckboxValidation = function (checked) {
	if (checked === true) {
		academies.clearTrustSearchErrorBars();
	}
};

