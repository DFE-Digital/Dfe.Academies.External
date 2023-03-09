import BasePage from "../BasePage"
export default class A2BYourApplication extends BasePage {
    
    static yourApplicationNotStartedElementsVisible()
    {
        cy.yourApplicationNotStartedElementsVisible()
    }
    
    static yourApplicationNotStartedButSchoolAddedElementsVisible()
    {
        cy.yourApplicationNotStartedButSchoolAddedElementsVisible()
    }

    static selectAddATrust()
    {
        cy.selectAddATrust()
    }

    static selectAddASchool()
    {
        cy.SelectAddASchool()
    }

    static selectTrustDetails() {
        cy.selectTrustDetails()
    }

    static selectChangeSchool() {
        cy.selectChangeSchool()
    }

    static selectChangeTrust() {
        cy.selectChangeTrust()
    }




}