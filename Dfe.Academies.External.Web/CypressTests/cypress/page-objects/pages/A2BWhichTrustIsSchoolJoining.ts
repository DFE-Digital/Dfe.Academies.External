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

    static selectConfirmTrust()
    {
        cy.get('#ConfirmSelection').click()
    }

    static submitTrustName()
    {
        cy.get('#btnAdd').click()
    }

    static changeTrustName() {
        cy.changeTrustName()
    }
}