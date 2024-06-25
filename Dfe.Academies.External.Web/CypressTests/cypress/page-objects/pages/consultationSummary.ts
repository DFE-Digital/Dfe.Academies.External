class ConsultationSummary {
  // TODO get better selector for element
  public startConsultation(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  // TODO fix commented lines
  // TODO all of these elements require proper Cypress tags
  public checkConsultationSummaryCompleted(): this {
    // cy.get('p').eq(2).contains('No')

    return this
  }

  // TODO get better selector for element
  public saveAndReturnToApp(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const consultationSummary = new ConsultationSummary()

export default consultationSummary
