class FinancialInvestigations {
  public financialInvestigationsElementsVisible(): this {
    cy.financialInvestigationsElementsVisible()

    return this
  }

  public selectFinancialInvestigationsOptionNo(): this {
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')

    return this
  }

  public submitFinancialInvestigations(): this {
    cy.submitFinancialInvestigations()

    return this
  }
}

const financialInvestigations = new FinancialInvestigations()

export default financialInvestigations
