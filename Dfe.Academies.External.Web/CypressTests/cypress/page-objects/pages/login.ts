class Login {
  public login(): this {
    const username = Cypress.env('LOGIN_USERNAME')
    const password = Cypress.env('LOGIN_PASSWORD')
    const args = { username, password }
    const signinUrl = Cypress.env('SigninUrl')
    cy.origin(signinUrl, { args }, (args) => {
      cy.get('#username').type(args.username)
      cy.get('button[type="submit"]').click()
      cy.get('#password').type(args.password, { log: false })
      cy.contains('Sign in').click()
    })

    cy.get('h1').contains('Your applications')

    return this
  }
}

const login = new Login()

export default login
