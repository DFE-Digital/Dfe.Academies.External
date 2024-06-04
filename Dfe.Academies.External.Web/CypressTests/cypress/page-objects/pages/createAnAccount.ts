class CreateAnAccount {
  public createAccountElementsVisible(): this {
    cy.createAccountElementsVisible()

    return this
  }

  public createAccountFailsWithNoData(): this {
    cy.createAccountFailsWithNoData()

    return this
  }

  public createAccountSuccessful(): this {
    cy.createAccountSuccessful()

    return this
  }
}

const createAnAccount = new CreateAnAccount()

export default createAnAccount
