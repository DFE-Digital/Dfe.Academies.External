class FinancialInvestigations {
  public enterFinancialInvestigationsDetails(): this {
    cy.get('#selectoptionNo').click()
    cy.get('#selectoptionNo').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const financialInvestigations = new FinancialInvestigations()

export default financialInvestigations
