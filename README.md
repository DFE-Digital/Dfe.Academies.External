# Dfe Academies External Web

Web Application to contain any functionality accessed externally (i.e. outside DfE offices). Initially, for 'apply to become an academy'.

## Content

- [Dfe Academies External Web](#dfe-academies-external-web)
  - [Content](#content)
  - [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Shared Components](#shared-components)
    - [User Secrets](#user-secrets)
    - [Installing](#installing)
  - [Contributing](#contributing)
    - [Project Architecture](#project-architecture)
    - [Code structure](#code-structure)
    - [Caching](#caching)
    - [Error handling model state errors](#error-handling-model-state-errors)
  - [Running the Tests](#running-the-tests)
    - [Unit Tests](#unit-tests)
  - [Deployment](#deployment)
  - [Built With](#built-with)
    - [Dot Net Core Dependency Injection](#dot-net-core-dependency-injection)
    - [Logging / Serilog and Sentry](#logging--serilog-and-sentry)
    - [End to End Tests](#end-to-end-tests)

## Getting Started

### Prerequisites

This project requires you to have [Microsoft Dot Net Core 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed. 
To check if you have it installed on your machine, please run the following command in CMD:

```cmd
dotnet --version
```

This is all that is required to run this project.

### Shared Components
- [GOV.UK Frontend](https://github.com/alphagov/govuk-frontend)
- [GOV.UK Accessible autocomplete](https://github.com/alphagov/accessible-autocomplete)
- [dxw Mail Notify](https://github.com/dxw/mail-notify)

### User Secrets

You will need to configure user secrets to be able to run / contribute to the project. It will look similar to below:-
{
  "SignIn": {
    "OneLoginUrl": "",
    "OneLogoutUrl": "https://test-oidc.signin.education.gov.uk",
    "OneloginOpenIdConnectClientId": "",
    "OneloginOpenIdConnectClientSecret": ""
  },
  "academies_api": {
    "endpoint": "https://trams-external-api.azurewebsites.net/",
    "key": ""
  },
  "academisation_api": {
    "endpoint": "https://s184d01-aca-aca-app.nicedesert-a691fec6.westeurope.azurecontainerapps.io/",
    "key": ""
  },
  "emailnotifications": {
    "key": ""
  }
}

### Installing

Using Visual Studio, open the `Dfe.Academies.External.sln` file and let the IDE load the solution. 
Once the solution is fully loaded, you can right click on the solution file in the file explorer (inside Visual Studio) and then click on "Restore NuGet Packages". 
This will assert that the packages required for all projects under this solution are restored. 
Finally build the solution; the main project to set as the "Start Up Project" should be `Dfe.Academies.External.Web`.

## Contributing

Before contributing please read the following to get a better understanding of the standards and structure followed with this project.

There is no pre-defined test code coverage %age to aim for currently.

### Project Architecture

The project structure is simple:

| Project | Description |
| - | - |
| `Dfe.Academies.External.Web` | This is the project that the end user interacts directly with. It is meant to be a container for any functionality accessed externally (i.e. outside DfE offices) |
| `Dfe.Academies.External.Web.UnitTest` | The unit tests for the web application. |

`Dfe.Academies.External.Web` is a controller less razor page ASP.net MVC project. There are 2 controllers that have been created for trust & school search, these maybe replaced / refactored in the future.

There are 2 API's configured / consumed by the application. These are:-
1) Existing trams / academies API. Code for this resides within GitHub here:- https://github.com/DFE-Digital/trams-data-api .Dev URL here:- https://trams-external-api.azurewebsites.net/
2) New academisation API. Code for this resides within GitHub here:- https://github.com/DFE-Digital/academies-academisation-api .Dev URL here:- https://academies-academisation-api-dev.azurewebsites.net/

### Code structure
- AcademiesAPIResponseModels - these are the trams / academies API models
- Models - these are the academisation API models
- Attributes - For custom required attributes
- Custom validators - For custom ValidationAttribute. These are consumed by the school & trust search pages mostly
- Controllers - these have been created for trust & school search to call via ajax
- Extensions - static code helpers
- Helpers - A date and time helper consumed by the gov uk date input tag helper
- Pages - Container for razor pages and partial views. Partial views of note are:-
 
| Partial name | purpose |
| - | - |
| `_ErrorMessages` | This is to display error messages using gov UK design standards (https://design-system.service.gov.uk/components/error-summary/). This is consumed pretty much on every page |
| `_ValidationSummary` | To display error messages using gov UK design standards (https://design-system.service.gov.uk/components/error-message/). This is consumed pretty much on every page |
| `_SchoolComponentsStatusPartial` | To display the status to the user of a section. Using gov UK design standards (https://design-system.service.gov.uk/patterns/task-list-pages/) |
| `_HiddenFields` | Just contains hidden input containing system Id's e.g. Application Id and urn (school id) |
| `_SchoolDetails` | Contains HTML to render school summary details |
| `_TrustDetails` | Contains HTML to render trust summary details |

There are currently 2 base abstract classes to inherit from in a razor page. These are:-
- BasePageModel - Contains ValidationErrorMessagesViewModel property which is consumed across all pages. Contains UserHasSubmitApplicationRole property which means current user has elevated privileges and contains a helper func called ConvertModelStateToDictionary()
- BasePageEditModel - Inherits off BasePageModel and extends with crud based data functionality common across all pages
- BaseSchoolPageEditModel - Inherits off BasePageEditModel and extends to give functionality required for all application school pages
- BaseApplicationPageEditModel - Inherits off BasePageEditModel and extends to give functionality required for all application trust pages

- Services - these are the middleware to interface to the API layers from within the pages. Injected using Dependency Injection. Currently these are:-

| Service | purpose |
| - | - |
| `BaseService` | Base abstract class to encapsulate common functionality |
| `ConversionApplicationCreationService` | To create an application through the academisation API (see above for details) |
| `ConversionApplicationRetrievalService` | To GET application data from the academisation API (see above for details) |
| `ReferenceDataRetrievalService` | To GET data from the trams / academies API (see above for details) |
| `ResilientRequestProvider` |  Generic API wrapper helper which actually accesses the API endpoints and does the serialization / de-serialization. It's called resilient because originally it had polly retry code within it which may get put back |

- Tag Helpers - Two helpers to render HTML e.g. date input in gov UK standards format - GovUkDateInputTagHelper (https://design-system.service.gov.uk/patterns/dates/)
- View Models - Helper classes to aid rendering data in the format the UI designs demand

### Caching
The application uses ViewData[] to store selected application id / application json and selected school urn as they progress through the wizard sections. 
It uses the helper ViewDataHelper.cs to push & pull the data into the ViewData[].

Application Id and urn (school id) are also passed around the application through URL parameters. This maybe changed / refactored in the future for security reasons.

### Error handling model state errors
These are pushed into ViewData["Errors"] in a method in each page (this could definately be refactored to be better !!!) to be read by the error messages partial

## Running the Tests

When working on the project, make sure you do not break any unit tests. You should also do manual system testing to assert your changes work as intended, and any area they may affect are not broken.

---

### Unit Tests

There is no pre-defined test code coverage %age to aim for currently.

The Moq library is used to spin up mock objects such as a HttpClient to test the API integration.

The AutoFixture library is used to create mock test data.

There is some sample json responses from the API contained within the unit test project to test serialization / de-serialization.

However, the following code sections of the web application have got unit test coverage:-
- Controllers - to test the dependency on the api layer (via mocking) and where a partial view is returned, that this has returned HTML
- Custom validators - to ensure they validate as expected
- Extensions - startup extensions
- Helpers 
- Pages - to test model state validation and OnGet && OnPost logic, to integration test the dependency on the api layer (via mocking)
- Services - to integration test the dependency on the api layer (via mocking)
- Tag Helpers - to test they return the correct HTML
- View Models - basic property get / set tests

There is also limited code coverage on the models to serialize / deserialize the API responses. These are at:-
- AcademiesAPIResponseModels - these are the trams / academies API models
- Models - these are the academisation API models


The naming convention for unit tests is as follows:-
OnGetAsync___Valid___NullErrors

TODO:- check with wystan on dictionary definition of each component of name - ***

Using NUnit - https://docs.nunit.org/

<small>^ [Back to Top](#dfe-academies-external-web)</small>

## Deployment

Deployed to Dev automatically when the master/main branch is modified. This is done via a Azure DevOps pipeline.

The location of dev is as follows:-
https://webapp-t1dv-sip-a2c.azurewebsites.net/

## Built With

- [DotNet](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet) - Free, cross-platform, open source developer platform for building many different types of applications.
- [AspNet](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) - A cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected apps.
- [NUnit](https://nunit.org/) - A unit-testing framework for all .NET languages.
- [GovUk.Frontend.AspNetCore](https://www.nuget.org/packages/GovUk.Frontend.AspNetCore) -
- [accessible-autocomplete](https://github.com/alphagov/accessible-autocomplete) - library used on the school & trust search pages
- [jQuery.Validation](https://jqueryvalidation.org/documentation/) - Added to have client side validation. Mainly used on the school & trust search pages
- [jQuery](https://api.jquery.com/) - Needed for school & trust search pages consumed by accessible-autocomplete library

### Dot Net Core Dependency Injection

http://www.jiodev.com/aspnet/core/fundamentals/startup

<small>^ [Back to Top](#dfe-academies-external-web)</small>

---

### Logging / Serilog and Sentry

All logging should be done with the standard `ILogger<T>` instance injected with DI. 
This comes from `microsoft.extensions.logging.abstractions` package which is included in ASP NET.

> **NOTE:** You can find out more about dotnet core logging fundamentals [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0).

The `ILogger<T>` instanced are actually done using `Serilog` which comes from `Serilog` package. 
This is then setup inside `HostBuilder` in `Program.cs`.

Here are some tutorials followed to set up the logging:
- [Best Logging Practices for your Dot Net Applications](https://coralogix.com/log-analytics-blog/net-logging-best-practices-for-your-net-application/)

<small>^ [Back to Top](#dfe-academies-external-web)</small>


### End to End Tests

The end to end tests are documented within their directory:

[End to end tests](Dfe.Academies.External.Web/CypressTests/README.md)

<small>^ [Back to Top](#dfe-academies-external-web)</small>