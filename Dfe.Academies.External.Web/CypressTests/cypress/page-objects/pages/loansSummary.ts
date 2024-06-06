class LoansSummary {
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
