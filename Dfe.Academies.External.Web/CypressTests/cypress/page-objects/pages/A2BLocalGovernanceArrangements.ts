import BasePage from ../basePage
export default class A2BLocalGovernanceArrangements extends BasePage {
    static localGovernanceArrangementsElementsVisible()
    {
        cy.localGovernanceArrangementsElementsVisible()
    }

    static localGovernanceArrangementsClickYes()
    {
        cy.get('#changesToLaGovernanceYes').click()
    }

    static enterlocalGovernanceArrangementsChanges()
    {
        cy.enterlocalGovernanceArrangementsChanges()
    }

    static localGovernanceArrangementsSubmit()
    {
        cy.get('input[type=submit]').click()
    }
}