class GovernanceStructureSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public checkGovernanceStructureSummaryCompleted(): this {
    cy.get('[data-cy="response"]').contains('fifty-k.docx')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const governanceStructureSummary = new GovernanceStructureSummary()

export default governanceStructureSummary
