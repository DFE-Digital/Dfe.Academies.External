class LoansSummary {
  public enterLoansDetails(): this {
    cy.get('#anyLoansOptionNo').click()
    cy.get('#anyLoansOptionNo').should('be.checked')

    cy.get('input[type=submit]').click()

    return this
  }
}

const loansSummary = new LoansSummary()

export default loansSummary
