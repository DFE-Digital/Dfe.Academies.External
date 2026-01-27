import { TestUser } from './TestUser'

// User roles enum values matching backend SchoolRoles
export enum UserRoles {
  ChairOfGovernors = 'ChairOfGovernors',
  Other = 'Other'
}

// Cypress user type identifier
export const userType = 'cypress'

// Environment variable keys
export const EnvUrl = 'URL'
export const EnvAuthKey = 'AUTH_KEY'

// Function to get the default Cypress test user
// The handler uses x-user-context-name for the name/email
export function getCypressUser(): TestUser {
  return new TestUser(
    'C29AF147-F2F5-4D30-B8A5-C68BF83A148A',
    Cypress.env('LOGIN_USERNAME') || 'test.cypress@education.gov.uk',
    'TEST-AD-ID', // This matches TestFallbackObjectId in CypressAuthenticationHandler
    UserRoles.ChairOfGovernors,
    Cypress.env('LOGIN_USERNAME') || 'test.cypress@education.gov.uk'
  )
}

// Default Cypress test user (lazy initialization)
export const cypressUser = getCypressUser()
