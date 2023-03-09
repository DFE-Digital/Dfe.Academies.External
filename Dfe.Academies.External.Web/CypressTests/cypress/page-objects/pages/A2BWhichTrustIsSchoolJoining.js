import BasePage from "../BasePage"
export default class A2BWhichTrustIsSchoolJoining extends BasePage {
    
    static whichTrustIsSchoolJoiningElementsVisible()
    {
        cy.whichTrustIsSchoolJoiningElementsVisible()
    }

    static enterTrustNameSelectAndSubmit()
    {
        cy.enterTrustNameSelectAndSubmit()
    }

    static changeTrustName() {
        cy.changeTrustName()
    }
}