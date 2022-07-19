var A2C = window.A2C || {};

let debounceTimeout;

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
                $('#btnAdd').addClass('govuk-button--disabled')
                .attr('aria-disabled', 'true')
                .prop('disabled', 'true');
                if ($('#confirm-school-checkbox').length == 0) {
                    $('#btnAdd').removeClass('govuk-button--disabled')
                        .attr('aria-disabled', 'false')
                        .removeAttr('disabled');
                } }, 2000);
        })
    })
};

A2C.renderSchoolSearchOption = function (selectedValue) {
    // get full school deets from an endpoint, then render
    $.ajax({
        url: 'DisplaySearchResult',
        type: 'GET',
        data: { 'SchoolId': selectedValue },
        success: function (response) {
            $('#searchResults').html(response);
            $('#confirm-school-checkbox').css('display', 'block')
            $('#confirm-school-checkbox').removeClass('hidden');
        }
    });
};

function debounceSuggest(query, syncResults) {
    clearTimeout(debounceTimeout);
    debounceTimeout = setTimeout(() => {
        getSchools(query, syncResults)
    }, 500);
}

// call search schools endpoint
function getSchools(query, syncResults) {
    $.ajax({
        url: 'Search',
        type: 'GET',
        data: { 'searchQuery': query },
        success: function (response) {
            syncResults(query
                ? response.filter(function (result) {
                    return true
                })
                : []
            )
        }
    });
}

$(document).ready(function () {
    $('#confirm-school-checkbox').css('display', 'none') // This alerts: <a href="#">Services</a>
        .css('margin-bottom', '1.3em');
});

$(function () {
    A2C.searchSchools();
});

function toggleConfirmationCheckbox() {
    if ($('#confirm-school-cb').prop('checked') == true) {
        $('#btnAdd').removeClass('govuk-button--disabled')
            .attr('aria-disabled', 'false')
            .removeAttr('disabled');
    }
    else {
        $('#btnAdd').addClass('govuk-button--disabled')
            .attr('aria-disabled', 'true')
            .prop('disabled', 'true');
    }
}