import BasePage from "../BasePage"
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