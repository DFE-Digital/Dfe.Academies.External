import BasePage from ../basePage
export default class A2BMainContacts extends BasePage {
    
    static mainContactsNotStartedElementsVisible()
    {
        cy.mainContactsNotStartedElementsVisible()
    }
    
    static fillMainContactDetailsAndSubmit() 
    {
        cy.fillHeadTeacherDetails()
  
        cy.fillChairDetails()
 
        cy.get('#ContactTypeChairOfGoverningBody').click()
        cy.get('#ContactTypeChairOfGoverningBody').should('be.checked')

        cy.fillApproverDetails()
  
        cy.get('input[type=submit]').click()
    }

}