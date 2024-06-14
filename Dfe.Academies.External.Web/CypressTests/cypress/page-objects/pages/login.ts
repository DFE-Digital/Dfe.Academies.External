class Login {
  public login(): this {
    const signinUrl = Cypress.env('SIGNIN_URL')
    cy.origin(signinUrl, () => {
      const username = Cypress.env('LOGIN_USERNAME')
      const password = Cypress.env('LOGIN_PASSWORD')

      cy.get('#username').type(username)
      cy.get('#password').type(password, { log: false })
      cy.contains('Sign in').click()
    })
    // TODO remove reliance on this wait
    // eslint-disable-next-line cypress/no-unnecessary-waiting
    // cy.wait(15000)

    return this
  }
}

const login = new Login()

export default login
