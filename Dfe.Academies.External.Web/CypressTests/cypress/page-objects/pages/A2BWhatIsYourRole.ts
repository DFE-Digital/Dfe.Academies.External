import BasePage from ../basePage
export default class A2BWhatIsYourRole extends BasePage {
    static whatIsYourRoleElementsVisible()
    {
        cy.whatIsYourRoleElementsVisible()
    }

    static selectChairOfGovernorsRadioButtonVerifyAndSubmit()
    {
        cy.get('input[type=radio]').eq(0).click()
        cy.get('input[type=radio]').eq(0).should('be.checked')
        cy.get('input[type=submit]').click()
    }

    static selectSomethingElseRadioButton()
    {
        cy.selectSomethingElseRadioButton()
    }

    static verifySomethingElseRadioButtonChecked()
    {
        cy.verifySomethingElseRadioButtonChecked()
    }

}