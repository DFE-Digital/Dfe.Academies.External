import { AuthenticationInterceptor } from './AuthenticationInterceptor'

Cypress.Commands.add('executeAccessibilityTests', () => {
  const wcagStandards = ['wcag22aa', 'wcag21aa']
  const impactLevel = ['critical', 'minor', 'moderate', 'serious']
  const continueOnFail = false
  cy.injectAxe()
  cy.checkA11y(
    null,
    {
      retries: 3,
      runOnly: {
        type: 'tag',
        values: wcagStandards,
      },
      includedImpacts: impactLevel,
    },
    null,
    continueOnFail,
  )
})

Cypress.Commands.add('loginBypassOpenId', () => {
  // Use the authentication interceptor pattern
  // This registers an interceptor that adds auth headers to all requests
  // The CypressAuthenticationHandler (from AddCypressMultiAuthentication) will
  // read these headers and authenticate the user automatically
  const interceptor = new AuthenticationInterceptor()
  interceptor.register()
  
  cy.log('✓ Authentication interceptor registered - all requests will include auth headers')
  cy.log('The CypressAuthenticationHandler will authenticate requests with these headers automatically')
})
