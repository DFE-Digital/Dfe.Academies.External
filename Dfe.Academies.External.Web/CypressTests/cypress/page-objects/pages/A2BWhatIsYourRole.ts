import BasePage from "../BasePage"
export default class A2BWhatIsYourRole extends BasePage {
    static whatIsYourRoleElementsVisible()
    {
        cy.whatIsYourRoleElementsVisible()
    }

    static selectChairOfGovernorsRadioButton()
    {
        cy.get('input[type="radio"]').eq(0).click()
    }

    static verifyChairOfGovernorsRadioButtonChecked()
    {
        cy.get('input[type="radio"]').eq(0).should('be.checked')
    }

    static selectSomethingElseRadioButton()
    {
        cy.selectSomethingElseRadioButton()
    }

    static verifySomethingElseRadioButtonChecked()
    {
        cy.verifySomethingElseRadioButtonChecked()
    }

    static selectWhatIsYourRoleSaveAndContinue()
    {
        cy.get('input[type="submit"]').click()
    }
}