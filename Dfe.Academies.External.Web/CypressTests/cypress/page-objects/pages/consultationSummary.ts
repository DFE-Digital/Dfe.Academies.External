class ConsultationSummary {
  public selectConsultationStartSection(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  public consultationSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Consultation')

    cy.get('.govuk-link').contains('Change your answers')

    cy.get('b').eq(0).contains('Has the governing body consulted the relevant stakeholders?')
    cy.get('p').eq(2).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  public submitConsultationSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const consultationSummary = new ConsultationSummary()

export default consultationSummary
