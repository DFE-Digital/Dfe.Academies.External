class PlansForGrowthSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public checkPlansForGrowthSummaryCompleted(): this {
    cy.get('[data-cy="response"]').contains('Yes')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const plansForGrowthSummary = new PlansForGrowthSummary()

export default plansForGrowthSummary
