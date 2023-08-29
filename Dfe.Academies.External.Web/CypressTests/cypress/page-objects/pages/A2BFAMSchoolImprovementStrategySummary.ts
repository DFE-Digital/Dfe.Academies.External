import BasePage from "../BasePage"
export default class A2BFAMSchoolImprovementStrategySummary extends BasePage {
    
static selectStartSection()
{
    cy.contains('Start section').click()
}

static schoolImprovementStrategyCompleteElementsVisibleAndSubmit()
{
    cy.get('.govuk-link').eq(1).contains('Change your answers')

    cy.get('p').eq(2).contains('How will the trust support and improve the academies in the trust?')
    cy.get('p').eq(4).contains('How will the trust make sure that the school improvement strategy is fit for purpose and will improve standards?')
    cy.get('p').eq(6).contains('If the trust wants to become an approved sponsor, when would it plan to apply and begin sponsoring other schools?')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application').click()
}

}