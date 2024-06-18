class FinancialInvestigations {
  public selectFinancialInvestigationsOptionNo(): this {
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')

    return this
  }

  public submitFinancialInvestigations(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const financialInvestigations = new FinancialInvestigations()

export default financialInvestigations
