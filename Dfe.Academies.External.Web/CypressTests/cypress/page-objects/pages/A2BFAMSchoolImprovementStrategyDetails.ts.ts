import BasePage from "../BasePage"
export default class A2BFAMSchoolImprovementStrategySummary extends BasePage {
    
static fillSchoolImprovementStrategyAndSubmit()
{
    cy.get('#ImprovementSupport').click()
    cy.get('#ImprovementSupport').type('How will the trust support and improve the academies in the trust?')

    cy.get('#ImprovementStrategy').click()
    cy.get('#ImprovementStrategy').type('How will the trust make sure that the school improvement strategy is fit for purpose and will improve standards?')

    cy.get('#ImprovementApprovedSponsor').click()
    cy.get('#ImprovementApprovedSponsor').type('If the trust wants to become an approved sponsor, when would it plan to apply and begin sponsoring other schools?')

    cy.get('input[type="submit"]').should('be.visible').contains('Save and continue').click()
}

}