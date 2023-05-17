import BasePage from "../BasePage"
export default class A2BWhatAreYouApplyingToDo extends BasePage {
    static whatAreYouApplyingToDoElementsVisible()
    {
        cy.whatAreYouApplyingToDoElementsVisible()
    }

    static selectJAMRadioButton()
    {
        cy.get('input[type="radio"]').eq(0).click()
    }

    static verifyJAMRadioButtonChecked()
    {
        cy.get('input[type="radio"]').eq(0).should('be.checked')
    }

    static selectFAMRadioButton()
    {
        cy.get('input[type="radio"]').eq(1).click()
    }

    static verifyFAMRadioButtonChecked()
    {
        cy.get('input[type="radio"]').eq(1).should('be.checked')
    }

    static selectApplyingToDoSaveAndContinue()
    {
        cy.get('input[type="submit"]').click()  
    }
}