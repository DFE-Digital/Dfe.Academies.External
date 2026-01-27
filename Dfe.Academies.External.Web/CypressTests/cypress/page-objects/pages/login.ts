class Login {
  public login(): this {
    const bypassOpenId = Cypress.env('BYPASS_OPENID') === true
    
    if (bypassOpenId) {
      // For bypass, authentication should have happened before visiting
      // Just verify we're authenticated and on the right page
      cy.url({ timeout: 10000 }).should('include', 'your-applications')
      cy.get('[data-cy="startNewApplicationButton"]', { timeout: 10000 }).should('be.visible').then(() => {
        cy.log('✓ Successfully authenticated via bypass and on your-applications page')
      })
    } else {
      // Standard OpenID authentication flow
      const username = Cypress.env('LOGIN_USERNAME')
      const password = Cypress.env('LOGIN_PASSWORD')
      const args = { username, password }
      const signinUrl = Cypress.env('SIGNIN_URL')
      cy.origin(signinUrl, { args }, (args) => {
        cy.get('#username').type(args.username)
        cy.get('button[type="submit"]').click()
        cy.get('#password').type(args.password, { log: false })
        cy.contains('Sign in').click()
      })

      // Verify login - "Your applications" h1 only appears if there are existing applications
      cy.get('body', { timeout: 10000 }).then(($body) => {
        if ($body.find('h1:contains("Your applications")').length > 0) {
          cy.get('h1').contains('Your applications')
        } else {
          // If no existing applications, verify we can see the start button
          cy.get('[data-cy="startNewApplicationButton"]', { timeout: 10000 }).should('be.visible')
        }
      })
    }

    return this
  }
}

const login = new Login()

export default login
