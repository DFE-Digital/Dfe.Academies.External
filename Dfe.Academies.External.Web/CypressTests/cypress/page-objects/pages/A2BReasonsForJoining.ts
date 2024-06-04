import BasePage from ../basePage
export default class A2BReasonsForJoining extends BasePage {

    static reasonsForJoiningElementsVisible()
    {
        cy.reasonsForJoiningElementsVisible()
    }

    static reasonsForJoiningInputAndSubmit()
    {
        cy.reasonsForJoiningInput()
        cy.submitReasonsForJoining()
    }

}