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

## Automated Cypress Accessibility Testing Using Cypress-axe
```
i.e.,
Basic usage

it('Has no detectable a11y violations on load', () => {
  // Test the page at initial load
  cy.checkA11y()
})
//By default, it will scan the whole page but it also can be configured to run against a specific element, or to exclude some elements.
```
```
i.e.,
Applying a context and run parameters

it('Has no detectable a11y violations on load (with custom parameters)', () => {
  // Test the page at initial load (with context and options)
  cy.checkA11y('.example-class', {
    runOnly: {
      type: 'tag',
      values: ['wcag2a']
    }
  })
})
```
For more receipes: https://www.npmjs.com/package/cypress-axe

## Linting

We use [eslint](https://eslint.org/) for static code analysis. To run the linter, use the script `npm run lint`