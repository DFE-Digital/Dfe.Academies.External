# Dfe-Academies-External-Web

Web Application to container any functionality accessed externally (i.e. outside DfE offices). Initially, for 'apply to become'.

## Content

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installing](#installing)
- [Contributing](#contributing)
  - [Project Architecture](#project-architecture)
- [Running the Tests](#running-the-tests)
  - [Unit Tests](#unit-tests)
- [Deployment](#deployment)
- [Built With](#built-with)
  - [Dot Net Core Dependency Injection](#dot-net-core-dependency-injection)
  - [Logging, Serilog and Sentry](#logging-serilog-sentry)

## Getting Started

### Prerequisites

This project requires you to have [Microsoft Dot Net Core 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed. 
To check if you have it installed on your machine, please run the following command in CMD:

```cmd
dotnet --version
```

This is all that is required to run this project.

### Installing

Using Visual Studio, open the `Dfe.Academies.External.sln` file and let the IDE load the solution. 
Once the solution is fully loaded, you can right click on the solution file in the file explorer (inside Visual Studio) and then click on "Restore NuGet Packages". 
This will assert that the packages required for all projects under this solution are restored. 
Finally build the solution; the main project to set as the "Start Up Project" should be `Dfe.Academies.External.Web`.

## Contributing

Before contributing please read the following to get a better understanding of the standards and structure followed with this project.

There is no pre-defined test code coverage %age to aim for.

### Project Architecture

The project structure is simple:

| Project | Description |
| - | - |
| `Dfe.Academies.External.Web` | This is the project that the end user interacts directly with. It is meant to be a container for any functionality accessed externally (i.e. outside DfE offices) |
| `Dfe.Academies.External.Web.UnitTest` | The unit tests for the web application. |

## Running the Tests

When working on the project, make sure you do not break any unit tests. You should also do manual system testing to assert your changes work as intended, and any area they may affect are not broken.

## User Secrets

You will need to configure user secrets to be able to run / contribute to the project. It will look similar to below:-
{
  "SignIn:OneLoginUrl": "",
  "SignIn:OneLogoutUrl": "",
  "SignIn:OneloginOpenIdConnectClientId": "",
  "OneloginOpeIdConnectClientSecret": "",
  "academies-api:endpoint": "",
  "academies-api:key": "",
  "academisation-api:endpoint": "",
  "academisation-api:key": ""
}

---

### Unit Tests

Using NUnit - https://docs.nunit.org/

TODO: Add this section...

<small>^ [Back to Top](#Dfe-Academies-External-Web)</small>

## Deployment

Deployed to (Dev):-
https://webapp-t1dv-sip-a2c.azurewebsites.net/

## Built With

- [DotNet](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet) - Free, cross-platform, open source developer platform for building many different types of applications.
- [AspNet](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-3.1) - A cross-platform, high-performance, open-source framework for building modern, cloud-enabled, Internet-connected apps.
- [NUnit](https://nunit.org/) - A unit-testing framework for all .NET languages.

### Dot Net Core Dependency Injection

http://www.jiodev.com/aspnet/core/fundamentals/startup

<small>^ [Back to Top](#Dfe-Academies-External-Web)</small>


---

### Logging / Serilog and Sentry

All logging should be done with the standard `ILogger<T>` instance injected with DI. 
This comes from `microsoft.extensions.logging.abstractions` package which is included in ASP NET.

> **NOTE:** You can find out more about dotnet core logging fundamentals [here](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1).

The `ILogger<T>` instanced are actually done using `Serilog` which comes from `Serilog` package. 
This is then setup inside `HostBuilder` in `Program.cs`.

Here are some tutorials followed to set up the logging:
- [Best Logging Practices for your Dot Net Applications](https://coralogix.com/log-analytics-blog/net-logging-best-practices-for-your-net-application/)

<small>^ [Back to Top](#Dfe-Academies-External-Web)</small>


