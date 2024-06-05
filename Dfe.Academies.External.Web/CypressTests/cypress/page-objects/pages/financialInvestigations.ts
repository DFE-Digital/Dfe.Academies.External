class FinancialInvestigations {
  public financialInvestigationsElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Finances (Step 6 of 6)')
    cy.get('.govuk-heading-l').contains('Financial investigations')

    cy.get('legend').contains('Are there any financial investigations ongoing at the school?')

    cy.get('#selectoptionYes').should('not.be.checked')
    cy.get('label[for=selectoptionYes]').contains('Yes')

    cy.get('#selectoptionNo').should('not.be.checked')
    cy.get('label[for=selectoptionNo]').contains('No')

    cy.get('input[type=submit]')

    return this
  }

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
