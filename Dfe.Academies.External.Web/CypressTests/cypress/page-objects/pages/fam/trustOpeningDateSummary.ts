class TrustOpeningDateSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public checkOpeningDateSummaryCompleted(trustOpeningDateYear: string, approverName: string, approverEmail: string): this {
    cy.get('[data-cy="response"]').eq(0).contains(`01/09/${trustOpeningDateYear}`)
    cy.get('[data-cy="response"]').eq(1).contains(approverName)
    cy.get('[data-cy="response"]').eq(2).contains(approverEmail)

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const trustOpeningDateSummary = new TrustOpeningDateSummary()

export default trustOpeningDateSummary
