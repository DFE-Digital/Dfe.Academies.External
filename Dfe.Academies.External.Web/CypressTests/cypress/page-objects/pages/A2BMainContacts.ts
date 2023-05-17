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
    cy.get('#ContactTypeChairOfGoverningBody').click()
    cy.get('#ContactTypeChairOfGoverningBody').should('be.checked')
    }

    static fillApproverDetails()
    {
        cy.fillApproverDetails()
    }

    static submitMainContactsForm()
    {
        cy.get('input[type="submit"]').click()
    }

}