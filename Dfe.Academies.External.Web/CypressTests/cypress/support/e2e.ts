import './commands'
import 'cypress-axe'

declare global {
  // eslint-disable-next-line @typescript-eslint/no-namespace
  namespace Cypress {
    interface Chainable {
      executeAccessibilityTests(): Chainable<Element>
    }
  }
}

/**
* Cypress Grep module for filtering tests
*/
import registerCypressGrep from '@cypress/grep/src/support'
registerCypressGrep()
