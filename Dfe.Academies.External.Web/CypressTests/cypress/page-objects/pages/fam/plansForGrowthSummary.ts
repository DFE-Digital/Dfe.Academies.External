class PlansForGrowthSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  // TODO get better selector for element
  // TODO fix commented lines
  public checkPlansForGrowthSummaryCompleted(): this {
    // cy.get('p').eq(2).contains('Yes')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const plansForGrowthSummary = new PlansForGrowthSummary()

export default plansForGrowthSummary
