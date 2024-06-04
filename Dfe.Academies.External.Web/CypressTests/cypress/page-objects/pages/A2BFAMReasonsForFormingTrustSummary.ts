import BasePage from ../basePage
export default class A2BFAMReasonsForFormingTrustSummary extends BasePage {
    static selectStartSection()
    {
        cy.contains('Start section').click()
    }

    static FAMReasonsForFormingTrustSummaryCompleteElementsVisibleAndSubmit()
    {
        cy.FAMReasonsForFormingTrustSummaryCompleteElementsVisibleAndSubmit()   
    }


}