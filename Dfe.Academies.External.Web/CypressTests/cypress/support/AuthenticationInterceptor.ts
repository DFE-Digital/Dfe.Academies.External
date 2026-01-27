import { getCypressUser, EnvUrl, userType } from '../constants/cypressConstants'
import { TestUser } from '../constants/TestUser'

export class AuthenticationInterceptor {
  register(user: TestUser = getCypressUser()) {
    const baseUrl = Cypress.env(EnvUrl)

    if (!baseUrl) {
      throw new Error('URL is required for Cypress authentication bypass. Please set it in cypress.env.json')
    }

    cy.log(`Registering authentication interceptor for user: ${user.email}`)
    cy.log(`Base URL: ${baseUrl}`)
    cy.log(`User ID: ${user.id}`)
    cy.log(`AD ID: ${user.adId}`)
    cy.log(`Role: ${user.role}`)

    // Extract the hostname from the base URL to match all requests to that domain
    const escapedBaseUrl = baseUrl.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')
    const urlPattern = `${escapedBaseUrl}/**`

    cy.intercept(
      {
        method: '*',
        url: urlPattern,
        middleware: true,
      },
      (req) => {
        // Set authentication headers on every request made by the browser
        // These headers are read by CypressRequestChecker to determine if it's a Cypress request
        // and by CypressAuthenticationHandler to authenticate the user
        // IMPORTANT: This callback must be synchronous and cannot use cy commands
        
        if (!req.headers) {
          req.headers = {}
        }
        
        // x-user-context-name: User's name/email (required by CypressRequestChecker)
        req.headers['x-user-context-name'] = user.email
        
        // x-user-context-id: User identifier (required, defaults to GUID if not provided)
        req.headers['x-user-context-id'] = user.id
        
        // x-user-ad-id: Azure AD ID (required, defaults to "TEST-AD-ID" if not provided)
        req.headers['x-user-ad-id'] = user.adId
        
        // x-user-context-role-0: First role (the handler reads roles from x-user-context-role-0, x-user-context-role-1, etc.)
        req.headers['x-user-context-role-0'] = user.role
        
        // x-cypress-user: Identifier that this is a Cypress request (optional but useful for logging)
        req.headers['x-cypress-user'] = userType
      },
    ).as('AuthInterceptor')

    cy.log('✓ Authentication interceptor registered - all requests will include auth headers')
  }
}
