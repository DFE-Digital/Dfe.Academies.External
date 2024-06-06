import './commands'
import 'cypress-axe'

Cypress.on('uncaught:exception', (err) => {
  // returning false here prevents Cypress from
  // failing the test
  console.log('Cypress detected uncaught exception: ', err)
  return false
})

declare global {
  // eslint-disable-next-line @typescript-eslint/no-namespace
  namespace Cypress {
    interface Chainable {
      excuteAccessibilityTests(): Chainable<Element>
    }
  }
}

/**
* Cypress Grep module for filtering tests
*/
import registerCypressGrep from '@cypress/grep/src/support'
registerCypressGrep()
