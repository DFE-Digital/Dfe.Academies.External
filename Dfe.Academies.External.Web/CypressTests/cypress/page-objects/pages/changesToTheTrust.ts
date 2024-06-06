class ChangesToTheTrust {
  public changesToTheTrustClickYesEnterChangesAndSubmit(): this {
    cy.get('#revenueTypeYes').click()

    const changesToTheTrust = 'What are the changes? Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ull'
    cy.get('#PFYRevenueStatusExplained').click()
    cy.get('#PFYRevenueStatusExplained').type(changesToTheTrust)

    cy.get('input[type=submit]').click()

    return this
  }
}

const changesToTheTrust = new ChangesToTheTrust()

export default changesToTheTrust
