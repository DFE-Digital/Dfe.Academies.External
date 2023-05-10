import BasePage from "../BasePage"
export default class A2BWhichTrustIsSchoolJoining extends BasePage {
    
    static whichTrustIsSchoolJoiningElementsVisible()
    {
        cy.whichTrustIsSchoolJoiningElementsVisible()
    }

    static selectTrustName() 
    {
        cy.selectTrustName()
    }

    static changeTrustName() {
        cy.changeTrustName()
    }
}