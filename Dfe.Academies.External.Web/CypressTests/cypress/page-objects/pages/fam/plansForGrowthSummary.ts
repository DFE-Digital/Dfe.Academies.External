class PlansForGrowthSummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public FAMPlansForGrowthSummaryCompleteElementsVisibleAndSubmit(): this {
    cy.get('.govuk-link').contains('Change your answers')
    cy.get('b').eq(0).contains('Do you plan to grow the trust over the next 5 years?')
    // cy.get('p').eq(2).contains('Yes')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const plansForGrowthSummary = new PlansForGrowthSummary()

export default plansForGrowthSummary
