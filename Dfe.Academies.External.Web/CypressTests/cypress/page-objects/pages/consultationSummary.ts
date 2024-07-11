class ConsultationSummary {
  public startConsultation(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public checkConsultationSummaryCompleted(): this {
    cy.get('p').eq(2).contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const consultationSummary = new ConsultationSummary()

export default consultationSummary
