class ConsultationSummary {
  public startConsultation(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public checkConsultationSummaryCompleted(): this {
    cy.get('[data-cy="response"]').contains('No')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const consultationSummary = new ConsultationSummary()

export default consultationSummary
