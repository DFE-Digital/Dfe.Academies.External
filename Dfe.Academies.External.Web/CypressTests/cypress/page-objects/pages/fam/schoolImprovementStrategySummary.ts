class SchoolImprovementStrategySummary {
  public selectStartSection(): this {
    cy.contains('Start section').click()

    return this
  }

  public schoolImprovementStrategyCompleteElementsVisibleAndSubmit(): this {

    cy.get('[data-cy="response"]').eq(0).contains('How will the trust support and improve the academies in the trust?')
    cy.get('[data-cy="response"]').eq(1).contains('How will the trust make sure that the school improvement strategy is fit for purpose and will improve standards?')
    cy.get('[data-cy="response"]').eq(2).contains('If the trust wants to become an approved sponsor, when would it plan to apply and begin sponsoring other schools?')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()

    return this
  }
}

const schoolImprovementStrategySummary = new SchoolImprovementStrategySummary()

export default schoolImprovementStrategySummary
