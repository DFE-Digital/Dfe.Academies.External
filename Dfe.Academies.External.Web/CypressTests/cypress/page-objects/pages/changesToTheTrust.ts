class ChangesToTheTrust {
  public changesToTheTrustElementsVisible(): this {
    cy.changesToTheTrustElementsVisible()

    return this
  }

  public changesToTheTrustClickYesEnterChangesAndSubmit(): this {
    cy.get('#revenueTypeYes').click()
    cy.enterChangesToTheTrust()
    cy.get('input[type=submit]').click()

    return this
  }
}

const changesToTheTrust = new ChangesToTheTrust()

export default changesToTheTrust
