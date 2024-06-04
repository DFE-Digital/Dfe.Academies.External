class CreateAnAccountConfirm {
  public createAccountConfirmElementsVisible(): this {
    cy.createAccountConfirmElementsVisible()

    return this
  }
}

const createAnAccountConfirm = new CreateAnAccountConfirm()

export default createAnAccountConfirm
