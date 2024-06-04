class ForgottenYourPasswordVerifyCode {
  public forgotPasswordVerifyCodeElementsVisible(): this {
    cy.forgotPasswordVerifyCodeElementsVisible()

    return this
  }
}

const forgottenYourPasswordVerifyCode = new ForgottenYourPasswordVerifyCode()

export default forgottenYourPasswordVerifyCode
