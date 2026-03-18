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

	function inputValueTemplate(result) {
		return result && result.name + (result.ukprn != null ? ' (' + result.ukprn + ')' : '');
	}

	function suggestionTemplate(result) {
		return result && result.name +
			(result.ukprn != null ? ' (<span>' + result.ukprn + '</span>)' : '');
	}

	accessibleAutocomplete({
		element: autocompleteContainer,
		id: input.attr("id"),
		name: input.attr("name"),
		source: debounceSuggest,
		confirmOnBlur: false,
		displayMenu: 'overlay',
		minLength: 4,
		onConfirm: (function (selectedValue) {
			let val = selectedValue.name +' (' + selectedValue.ukprn + ')';
			input.val(val);

			academies.renderTrustSearchOption(selectedValue);

			let originalSearchInput = $("#autocomplete-container #SearchQueryInput");
			originalSearchInput.val(val);
		}),
		templates: {
			inputValue: inputValueTemplate,
			suggestion: suggestionTemplate
		}
	});
};

academies.renderTrustSearchOption = function (selectedValue) {
	// get full trust record from an endpoint
	// render partial & set results DIV HTML
	// unhide selected trust section of screen
	$.ajax({
		url: '../trust/ReturnTrustDetailsPartialViewPopulated',
		type: 'POST',
		data: selectedValue, // selected value will be in the format 'trust name (UKprn)'
		success: function (response) {
			academies.renderSelectedTrust(response);
			academies.unhideSelectedTrustSectionAndConfirmCheckbox();
			academies.clearTrustSearchErrorBars(); // clear any existing name not input err
		}
	});
};

academies.renderSelectedTrust = function (responseHtml) {
	$('#trustSelectedDetails').html(responseHtml);
	// this is a little hacky but without a page rewrite this is the best i could come up with in a short space of time
	$('#trustReference').val($('#td-trustReference').val());

};

function debounceSuggest(query, syncResults) {
	clearTimeout(debounceTimeout);
	debounceTimeout = setTimeout(() => {
		academies.GetTrustSearchResults(query, syncResults);
	}, 500);
}

academies.GetTrustSearchResults = function (query, syncResults) {
	$.ajax({
		url: '../trust/Search',
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
					academies.addSearchQueryValidationMessage("You must give the name of the trust");
					return false;
				} else {
					return true;
				}
			} else {
				academies.addSearchQueryValidationMessage("You must give the name of the trust");
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

academies.clearClientErrorSummary = function () {
	const summary = document.getElementById("client-error-summary");
	const list = document.getElementById("client-error-summary-list");
	if (summary && list) {
		list.innerHTML = "";
		summary.classList.add("govuk-!-display-none");
	}
};

academies.showErrorSummaryItem = function (fieldSelector, errorMessage) {
	const summary = document.getElementById("client-error-summary");
	const list = document.getElementById("client-error-summary-list");
	if (!summary || !list) return;

	const link = document.createElement("a");
	link.href = fieldSelector;
	link.className = "govuk-error-summary__link";
	link.textContent = errorMessage;

	const li = document.createElement("li");
	li.appendChild(link);
	list.appendChild(li);

	summary.classList.remove("govuk-!-display-none");
	summary.focus();
};

academies.clearTrustSearchErrorBars = function () {
	document.getElementById("SearchQueryContainer")?.classList.remove("govuk-form-group--error");

	const confirmationErrorContainer = document.getElementById("ConfirmationErrorContainer");
	if (confirmationErrorContainer) {
		confirmationErrorContainer.classList.remove("govuk-form-group--error");
		academies.hideElement("ConfirmationErrorContainer");
	}

	document.getElementById("confirm-trust-checkbox")?.classList.remove("govuk-form-group--error");

	academies.clearClientErrorSummary();
};

academies.addSearchQueryValidationMessage = function (errorMessage) {
	academies.clearTrustSearchErrorBars();
	academies.showErrorSummaryItem("#SearchQueryInput", errorMessage);

	const elementToManipulate = document.getElementById("SearchQueryContainer");
	elementToManipulate.classList.add("govuk-form-group--error");
};

academies.addConfirmValidationMessage = function () {
	academies.clearTrustSearchErrorBars();
	academies.showErrorSummaryItem("#ConfirmSelection", "You must confirm that this is the correct trust");
	academies.unhideElement("ConfirmationErrorContainer");
	const elementToManipulate = document.getElementById("confirm-trust-checkbox");
	elementToManipulate.classList.add("govuk-form-group--error");
};

academies.clientSideValidation = function () {
	var form = $("#search-form");
	form.validate();
	// Use form.valid() so ConfirmSelection checkbox is validated when trust is selected
	if (!form.valid()) {
		event.preventDefault();
	}
};

academies.clearTrustConfirmCheckboxValidation = function (checked) {
	if (checked === true) {
		academies.clearTrustSearchErrorBars();
	}
};

