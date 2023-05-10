import BasePage from "../BasePage"
export default class A2BWhatAreYouApplyingToDo extends BasePage {
    static whatAreYouApplyingToDoElementsVisible()
    {
        cy.whatAreYouApplyingToDoElementsVisible()
    }

    static selectJAMRadioButton()
    {
        cy.selectJAMRadioButton()
    }

    static verifyJAMRadioButtonChecked()
    {
        cy.verifyJAMRadioButtonChecked()
    }

    static selectFAMRadioButton()
    {
        cy.selectFAMRadioButton()
    }

    static verifyFAMRadioButtonChecked()
    {
        cy.verifyFAMRadioButtonChecked()
    }

    static selectApplyingToDoSaveAndContinue()
    {
        cy.selectApplyingToDoSaveAndContinue()
    }
}