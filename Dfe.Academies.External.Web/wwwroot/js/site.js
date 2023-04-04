// MR:- below from concerns casework
//window.showGlobalError = function () {
//	$("#moj-banner-error").removeClass("govuk-!-display-none");
//};
//window.hideGlobalError = function () {
//	$("#moj-banner-error").addClass("govuk-!-display-none");
//};
//window.showLoader = function () {
//	$(".ccms-loader").removeClass("govuk-!-display-none");
//};
//window.hideLoader = function () {
//	$(".ccms-loader").addClass("govuk-!-display-none");
//};

// Form validator
//window.formValidator = function (form) {
//	return new MOJFrontend.FormValidator(form);
//}

var Global_TimePageLoaded = Date.now();
var Global_KeyInputEffortCounter = 0;
var idleSecondsMax = 0
var intervalIdleCheckSeconds = 60
var CookieIdleName = "CookieKeepAlive"



function idlechecks(GAnalyticsKey, idleSecondsMaxParam) {

	var idleSecondsMax = idleSecondsMaxParam;
	var intervalIdleCheckSeconds = idleSecondsMax / 10

	var myCookie = getCookie(CookieIdleName);

	if (myCookie == null) {
		return;
	}

	// Every intervalIdleCheckSeconds and on a KeyUp event it resets the Idle Counter 
	document.onkeyup = function (event) {
		Global_KeyInputEffortCounter++;
		var delta = (Date.now() - Global_TimePageLoaded) / 1000;
		if (delta > intervalIdleCheckSeconds) {
			Global_TimePageLoaded = Date.now();
			// send keepAlive
			$.ajax({
				url: "/Cookies/KeepAlive",
				type: 'GET',
				dataType: 'json', // added data type
				success: function (res) {
					alert(res);
				}
			});

		}
	};


	// calls this function every intervalIdleCheckSeconds  
	window.setInterval(function () {

		var delta = (Date.now() - Global_TimePageLoaded) / 1000;
		if (delta > idleSecondsMax) {
			// sent to GA

			(function (i, s, o, g, r, a, m) {
				i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
					(i[r].q = i[r].q || []).push(arguments)
				}, i[r].l = 1 * new Date(); a = s.createElement(o),
					m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
			})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

			ga('create', GAnalyticsKey, 'auto');
			ga('send', {
				'hitType': 'event',
				'eventCategory': 'IdleLogOut',
				'eventAction': 'Nothing',
				'eventLabel': 'TotalOfKeysPressedbyUser',
				'eventValue': Global_KeyInputEffortCounter
			});


			//redirect to LogOut   
			window.location.href = '/Cookies/LogOut';

		}
	}, (intervalIdleCheckSeconds * 1000));
}

// checks if Cookie exists
function getCookie(name) {
	var dc = document.cookie;
	var prefix = name + "=";
	var begin = dc.indexOf("; " + prefix);
	if (begin == -1) {
		begin = dc.indexOf(prefix);
		if (begin != 0) return null;
	}
	else {
		begin += 2;
		var end = document.cookie.indexOf(";", begin);
		if (end == -1) {
			end = dc.length;
		}
	}
	// because unescape has been deprecated, replaced with decodeURI
	//return unescape(dc.substring(begin + prefix.length, end));
	return decodeURI(dc.substring(begin + prefix.length, end));
}
