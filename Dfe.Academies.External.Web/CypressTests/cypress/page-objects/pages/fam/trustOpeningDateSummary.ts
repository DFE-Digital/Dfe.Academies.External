class TrustOpeningDateSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public checkOpeningDateSummaryCompleted(trustOpeningDateYear: string, approverName: string, approverEmail: string): this {
    // TODO need better selectors for these
    cy.get('p').eq(3).contains(`01/09/${trustOpeningDateYear}`)
    cy.get('p').eq(5).contains(approverName)
    cy.get('p').eq(7).contains(approverEmail)

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const trustOpeningDateSummary = new TrustOpeningDateSummary()

export default trustOpeningDateSummary
