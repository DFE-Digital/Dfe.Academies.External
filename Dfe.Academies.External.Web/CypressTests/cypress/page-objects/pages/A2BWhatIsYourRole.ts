import BasePage from "../BasePage"
export default class A2BWhatIsYourRole extends BasePage {
    static whatIsYourRoleElementsVisible()
    {
        cy.whatIsYourRoleElementsVisible()
    }

    static selectChairOfGovernorsRadioButton()
    {
        cy.selectChairOfGovernorsRadioButton()
    }

    static verifyChairOfGovernorsRadioButtonChecked()
    {
        cy.verifyChairOfGovernorsRadioButtonChecked()
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
        cy.selectWhatIsYourRoleSaveAndContinue()
    }
}