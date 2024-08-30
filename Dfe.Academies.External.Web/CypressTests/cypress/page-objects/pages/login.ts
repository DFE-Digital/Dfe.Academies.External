class Login {
  public login(): this {
    const username = Cypress.env('loginUsername')
    const password = Cypress.env('loginPassword')
    const args = { username, password }
    const signinUrl = Cypress.env('signinUrl')
    cy.origin(signinUrl, { args }, (args) => {
      cy.get('#username').type(args.username)
      cy.get('#submit').click()
      cy.get('#password').type(args.password, { log: false })
      cy.contains('Sign in').click()
    })

    cy.get('h1').contains('Your applications')

    return this
  }
}

const login = new Login()

export default login
