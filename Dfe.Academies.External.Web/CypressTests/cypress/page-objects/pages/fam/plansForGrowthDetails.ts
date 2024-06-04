class PlansForGrowthDetails {
  public inputPlansForGrowthAndSubmit(): this {
    cy.get('#plansForGrowthOptionYes').click()
    cy.get('#GrowthPlanDescription').click()
    cy.get('#GrowthPlanDescription').type('What are your plans?')
    cy.get('input[type=submit]').should('be.visible').contains('Save and continue').click()

    return this
  }
}

const plansForGrowthDetails = new PlansForGrowthDetails()

export default plansForGrowthDetails
