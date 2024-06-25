# End-to-end tests

These are the end-to-end tests for the Apply to Become service. They have been written using [Cypress](https://cypress.io) in [TypeScript](https://www.typescriptlang.org/).

## Setup

### Installation

You will need to install all dependencies by running `npm install` in the `Dfe.Acadenues.External.Web/CypressTests` directory.

### Environment setup

The Cypress tests rely on some secrets being set. These can be added to a `cypress.env.json` file within the CypressTests folder, or in your terminal session:

```
{
  "url": <value>,
  loginUsername: <value>,
  loginPassword: <value>,
  signinUrl: <value>
}
```

Replace the values as required based on the below:

| Key | Description | Example |
|--|--|--|
| `url` | The base url for the application | `https://localhost:5005` |
| `loginUsername` | The username for logging in via DfE Sign-In | `foobar` |
| `loginPassword` | The password for logging in via DfE Sign-In | `abc123` |
| `signinUrl` | The url for DfE Sign-In | `https://localhost:5005/signin` |

## Test Execution

The Cypress tests will run against the front-end of the application.

To execute the tests locally and view the output, run the following:

`npm run cy:open`

To execute the tests in headless mode, run the following (the output will log to the console):

`npm run cy:run`

## Cypress Linting
We make use of [ESLint](https://eslint.org/) for code formatting and styling. This is to help make code more consistent and avoid bugs.

Linting is performed on Pull Requests to ensure standards are upheld, but can also be ran locally using the following command:

`npm run lint`

By default, all recommended checks are set to `Error`.

## Security testing with ZAP

The Cypress tests can also be run proxied via the Zed Attack Proxy (ZAP) for passive security scanning of the application.

Setup for running the ZAP daemon (via docker) can be found in the ZAP [documentation](https://www.zaproxy.org/docs/docker/about/#zap-headless).

**Note**: You will need to mount a volume to your host system to be able to retrieve generated reports like `${PWD}/zapoutput/:/zap/wrk:rw`

In your environment variables, you will need to set the following:

| Key | Description | Example |
|--|--|--|
| `HTTP_PROXY` | The URL that the ZAP daemon is running at | `http://localhost:8080` |
| `NO_PROXY` | A comma-separated list of URLS to bypass the proxy | `*.google.com,example.com` |
| `ZAP` | Flag to identify security scanning enabled (default is `false`) | `true` |
| `ZAP_ADDRESS` | The address that the ZAP daemon is running at | `localhost` |
| `ZAP_PORT` | The port that the ZAP daemon is running at | `8080` |
| `ZAP_API_KEY` | The API key configured for the ZAP daemon | `some-api-key` |

You can then run the Cypress tests as normal using `npx cypress run`.

HTML reports will be generated at the end of the run which will contain any identified issues.