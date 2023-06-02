import BasePage from "../BasePage"
export default class A2BWhichTrustIsSchoolJoining extends BasePage {
    
    static whichTrustIsSchoolJoiningElementsVisible()
    {
        cy.whichTrustIsSchoolJoiningElementsVisible()
    }

    static selectConfirmAndSubmitTrust()
    {
        cy.selectTrustName()
        cy.get('#ConfirmSelection').click()
        cy.get('#btnAdd').click()
    }

    static changeTrustName() {
        cy.changeTrustName()
    }
}