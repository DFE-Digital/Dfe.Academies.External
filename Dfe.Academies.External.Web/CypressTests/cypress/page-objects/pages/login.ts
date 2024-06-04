class Login {
  public login(username, password): this {
    cy.login(username, password)
    // eslint-disable-next-line cypress/no-unnecessary-waiting
    cy.wait(7000)

    return this
  }

  public loginToUnauthApplication(username, password): this {
    cy.loginToUnauthApplication(username, password)

    return this
  }

  public createAccount(): this {
    cy.createAccount()

    return this
  }

  public forgotPassword(): this {
    cy.forgotPassword()

    return this
  }
}

const login = new Login()

export default login
