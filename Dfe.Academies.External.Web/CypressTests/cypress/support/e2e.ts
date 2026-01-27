import './commands'
import 'cypress-axe'

declare global {
  // eslint-disable-next-line @typescript-eslint/no-namespace
  namespace Cypress {
    interface Chainable {
      executeAccessibilityTests(): Chainable<Element>
      loginBypassOpenId(): Chainable<void>
    }
  }
}

Cypress.on('uncaught:exception', (err) => {
  // returning false here prevents Cypress from
  // failing the test
  console.log('Cypress detected uncaught exception: ', err)
  return false
})

/**
* Cypress Grep module for filtering tests
*/
import registerCypressGrep from '@cypress/grep/src/support'
registerCypressGrep()
