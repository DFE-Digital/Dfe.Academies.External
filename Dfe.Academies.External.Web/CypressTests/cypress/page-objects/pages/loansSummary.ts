class LoansSummary {
  public loansSummaryElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Finances (Step 4 of 6)')
    cy.get('.govuk-heading-l').contains('Loans')

    cy.get('legend').contains('Are there any existing loans?')

    cy.get('#anyLoansOptionYes').should('not.be.checked')
    cy.get('label[for=anyLoansOptionYes]').contains('Yes')

    cy.get('#anyLoansOptionNo').should('not.be.checked')
    cy.get('label[for=anyLoansOptionNo]').contains('No')

    cy.get('input[type=submit]').should('be.visible').contains('Continue')

    return this
  }

  public selectLoansOptionNo(): this {
    cy.get('#anyLoansOptionNo').click()
    cy.get('#anyLoansOptionNo').should('be.checked')

    return this
  }

  public submitLoansSummary(): this {
    cy.get('input[type=submit]').click()

    return this
  }
}

const loansSummary = new LoansSummary()

export default loansSummary
