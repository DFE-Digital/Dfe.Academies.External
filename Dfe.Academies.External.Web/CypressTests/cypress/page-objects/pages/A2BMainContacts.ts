import BasePage from "../BasePage"
export default class A2BMainContacts extends BasePage {
    
    static mainContactsNotStartedElementsVisible()
    {
        cy.mainContactsNotStartedElementsVisible()
    }
    
    static fillHeadTeacherDetails() 
    {
        cy.fillHeadTeacherDetails()
    }

    static fillChairDetails()
    {
        cy.fillChairDetails()
    }

    static selectMainContactAsChair()
    {
        cy.selectMainContactAsChair()
    }

    static fillApproverDetails()
    {
        cy.fillApproverDetails()
    }

    static submitMainContactsForm()
    {
        cy.submitMainContactsForm()
    }

}