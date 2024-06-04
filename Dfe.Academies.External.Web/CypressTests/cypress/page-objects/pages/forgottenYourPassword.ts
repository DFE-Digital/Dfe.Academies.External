class ForgottenYourPassword {
  public forgotPasswordElementsVisible(): this {
    cy.forgotPasswordElementsVisible()

    return this
  }

  public forgotPasswordEmptyEmailSubmitted(): this {
    cy.forgotPasswordEmptyEmailSubmitted()

    return this
  }

  public forgotPasswordInvalidEmailSubmitted(): this {
    cy.forgotPasswordInvalidEmailSubmitted()

    return this
  }

  public forgotPasswordA2BUserEmailSubmitted(username): this {
    cy.forgotPasswordA2BUserEmailSubmitted(username)

    return this
  }
}

const forgottenYourPassword = new ForgottenYourPassword()

export default forgottenYourPassword
