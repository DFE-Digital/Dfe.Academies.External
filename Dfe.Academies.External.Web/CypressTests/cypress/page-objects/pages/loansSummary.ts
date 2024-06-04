class LoansSummary {
  public loansSummaryElementsVisible(): this {
    cy.loansSummaryElementsVisible()

    return this
  }

  public selectLoansOptionNo(): this {
    cy.get('#anyLoansOptionNo').click()
    cy.get('#anyLoansOptionNo').should('be.checked')

    return this
  }

  public submitLoansSummary(): this {
    cy.submitLoansSummary()

    return this
  }
}

const loansSummary = new LoansSummary()

export default loansSummary
