# End-to-end tests

These are the end-to-end tests for the Apply to Become service. They have been written using [Cypress](https://cypress.io) in [TypeScript](https://www.typescriptlang.org/).

## Setup

### Installation

You will need to install all dependencies using `npm install`.

### Environment setup

There are a few variables you will need to set to be able to run the tests locally. These can be added to a `.env` file within the CypressTests folder, or in your terminal session:

```
URL
LOGIN_USERNAME
LOGIN_PASSWORD
```

## Running the tests

There are two ways Cypress provides to run tests - through the Cypress runner and headless in command line.

To run through the Cypress runner, use `npm run cy:open`

To run through the command line, use `npm run cy:run`