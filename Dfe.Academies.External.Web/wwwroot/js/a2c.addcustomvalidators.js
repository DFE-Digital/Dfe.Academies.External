/**
 * Copyright (c) 2022
 *
 * For more information see:-
 * https://www.yogihosting.com/client-side-validation-asp-net-mvc/
 *
 * Dependencies:-
 * 1) JQuery
 * 2) JQuery unobtrusive validation
 */

$(function () {

	/* Date of Birth */
	jQuery.validator.addMethod('validbirthdate', function (value, element, params) {
		var minDate = new Date(params["min"]);
		var maxDate = new Date(params["max"]);
		var dateValue = new Date(value);
		if (dateValue > minDate && dateValue < maxDate) {
			return true;
		} else {
			return false;
		}
	});

	jQuery.validator.unobtrusive.adapters.add('validbirthdate', ['min', 'max'], function (options) {
		var params = {
			min: options.params.min,
			max: options.params.max
		};

		options.rules['validbirthdate'] = params;
		if (options.message) {
			options.messages['validbirthdate'] = options.message;
		}
	});
	/* End */


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


	//jQuery.validator.addMethod('termsvalidation', function (value, element, params) {
	//	if (value != "true")
	//		return false;
	//	else
	//		return true;
	//});

	//jQuery.validator.unobtrusive.adapters.add('termsvalidation', function (options) {
	//	options.rules['termsvalidation'] = {};
	//	options.messages['termsvalidation'] = options.message;
	//});

	/* Add Confirm checkbox Custom Validation config ! */
	$.validator.addMethod("confirmselection",
		function (value, element, parameters) {
			return value.toUpperCase() !== "FALSE";
		});

	$.validator.unobtrusive.adapters.add("confirmselection", [], function (options) {
		options.rules.cannotbered = {};
		options.messages["confirmselection"] = options.message;
	});

}(jQuery));