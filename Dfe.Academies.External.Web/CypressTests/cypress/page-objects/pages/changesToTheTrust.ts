class ChangesToTheTrust {
  public enterChangesToTheTrust(): this {
    cy.get('#revenueTypeYes').click()

    cy.get('#PFYRevenueStatusExplained').click()
    cy.get('#PFYRevenueStatusExplained').type('What are the changes?')

    cy.get('input[type=submit]').click()

    return this
  }
}

const changesToTheTrust = new ChangesToTheTrust()

export default changesToTheTrust
