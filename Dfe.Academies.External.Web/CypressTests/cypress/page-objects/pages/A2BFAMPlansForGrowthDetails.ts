import BasePage from ../basePage
export default class A2BFAMTrustPlansForGrowthDetails extends BasePage {

    static inputPlansForGrowthAndSubmit()
    {
        cy.get('#plansForGrowthOptionYes').click()
        cy.get('#GrowthPlanDescription').click()
        cy.get('#GrowthPlanDescription').type('What are your plans?')

        cy.get('input[type=submit]').should('be.visible').contains('Save and continue').click()

    }
}