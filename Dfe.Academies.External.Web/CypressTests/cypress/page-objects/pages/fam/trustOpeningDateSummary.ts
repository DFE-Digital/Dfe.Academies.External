class TrustOpeningDateSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public FAMOpeningDateSummaryCompleteElementsVisibleAndSubmit(trustOpeningDateYear: string, approverName: string, approverEmail: string): this {
    cy.get('.govuk-back-link').contains('Back')
    cy.get('.govuk-caption-l').contains('Plymouth')
    cy.get('.govuk-heading-l').contains('Opening date')

    cy.get('.govuk-link').contains('Change your answers')
    cy.get('p').eq(1).contains('When do the schools plan to establish the new multi-academy trust?')
    cy.get('p').eq(2).contains(`01/09/${trustOpeningDateYear}`)
    cy.get('p').eq(4).contains(approverName)
    cy.get('p').eq(6).contains(approverEmail)

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const trustOpeningDateSummary = new TrustOpeningDateSummary()

export default trustOpeningDateSummary
