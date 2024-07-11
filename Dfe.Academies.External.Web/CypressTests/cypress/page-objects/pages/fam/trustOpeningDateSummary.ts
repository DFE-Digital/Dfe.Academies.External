class TrustOpeningDateSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public checkOpeningDateSummaryCompleted(trustOpeningDateYear: string, approverName: string, approverEmail: string): this {
    // TODO need better selectors for these
    cy.get('p').eq(2).contains(`01/09/${trustOpeningDateYear}`)
    cy.get('p').eq(4).contains(approverName)
    cy.get('p').eq(6).contains(approverEmail)

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const trustOpeningDateSummary = new TrustOpeningDateSummary()

export default trustOpeningDateSummary
