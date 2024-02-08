import BasePage from "../BasePage"
export default class A2BWhatAreYouApplyingToDo extends BasePage {
    static whatAreYouApplyingToDoElementsVisible()
    {
        cy.whatAreYouApplyingToDoElementsVisible()
    }

    static selectJAMRadioButtonVerifyAndSubmit()
    {
        cy.get('input[type="radio"]').eq(0).click()
        cy.get('input[type="radio"]').eq(0).should('be.checked')
        cy.get('input[type="submit"]').click()  
    }

    static selectFAMRadioButtonVerifyAndSubmit()
    {
        cy.get('input[type="radio"]').eq(1).click()
        cy.get('input[type="radio"]').eq(1).should('be.checked')

        cy.get('input[type="submit"]').click()
    }

        

}